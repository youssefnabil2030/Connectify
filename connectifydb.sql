create database connectifydb;
go

use connectifydb;
go

-- =============================================
-- 1. USER MANAGEMENT & SETTINGS
-- =============================================

create table [user] (
    user_id int identity(1,1) primary key,
    username varchar(50) not null unique,
    email varchar(100) not null unique,
    password varchar(255) not null,
    shortbio nvarchar(500) null,
    profile_photo varchar(2083) null,
    cover_photo varchar(2083) null,
    date_of_brith date not null,
    creation_date datetime2 default getdate() not null
);

create table [setting] (
    setting_id int identity(1,1) primary key,
    user_id int not null unique,
    privacy_preference varchar(20) default 'public' check (privacy_preference in ('public', 'followers_only', 'private')),
    notification_preference varchar(50) default 'all',
    language_preference varchar(10) default 'en',
    foreign key (user_id) references [user](user_id) on delete cascade
);

create table [interest] (
    interest_id int identity(1,1) primary key,
    interest_name varchar(50) not null unique
);

create table [user_interest] (
    user_id int not null,
    interest_id int not null,
    primary key (user_id, interest_id),
    foreign key (user_id) references [user](user_id) on delete cascade,
    foreign key (interest_id) references [interest](interest_id) on delete cascade
);

create table [follow] (
    follower_id int not null,
    followed_id int not null,
    date_of_follow datetime2 default getdate() not null,
    primary key (follower_id, followed_id),
    foreign key (follower_id) references [user](user_id),
    foreign key (followed_id) references [user](user_id),
    constraint chk_cannot_follow_self check (follower_id <> followed_id)
);

create table [block] (
    blocker_id int not null,
    blocked_id int not null,
    blocking_date datetime2 default getdate() not null,
    primary key (blocker_id, blocked_id),
    foreign key (blocker_id) references [user](user_id),
    foreign key (blocked_id) references [user](user_id),
    constraint chk_cannot_block_self check (blocker_id <> blocked_id)
);

-- =============================================
-- 2. CONTENT CREATION & MEDIA
-- =============================================

create table [post] (
    post_id int identity(1,1) primary key,
    user_id int not null,
    text_caption nvarchar(max) null,
    visibility_setting varchar(20) default 'public' check (visibility_setting in ('public', 'followers_only', 'private')),
    publication_date datetime2 default getdate() not null,
    foreign key (user_id) references [user](user_id) on delete cascade
);

create table [album] (
    album_id int identity(1,1) primary key,
    user_id int not null,
    album_name nvarchar(100) not null,
    type_id nvarchar(50) null,
    foreign key (user_id) references [user](user_id) on delete cascade
);

create table [media_item] (
    item_id int identity(1,1) primary key,
    media_type varchar(10) not null check (media_type in ('photo', 'video')),
    file_url varchar(2083) not null,
    resolution varchar(20) null,
    deluration_ffor_videos int null,
    album_id int null,
    foreign key (album_id) references [album](album_id) on delete set null
);

create table [story] (
    story_id int identity(1,1) primary key,
    user_id int not null,
    item_id int not null unique,
    publication_date datetime2 default getdate() not null,
    foreign key (user_id) references [user](user_id),
    foreign key (item_id) references [media_item](item_id) on delete cascade
);

create table [poll] (
    poll_id int identity(1,1) primary key,
    post_id int not null unique,
    question nvarchar(255) not null,
    item_id int null,
    foreign key (post_id) references [post](post_id) on delete cascade
);

create table [poll_option] (
    option_id int identity(1,1) primary key,
    poll_id int not null,
    option_text nvarchar(150) not null,
    foreign key (poll_id) references [poll](poll_id) on delete cascade
);

create table [vote] (
    user_id int not null,
    option_id int not null,
    primary key (user_id, option_id),
    foreign key (user_id) references [user](user_id),
    foreign key (option_id) references [poll_option](option_id) on delete cascade
);

-- =============================================
-- 3. POLYMORPHIC INTERACTIONS & ENGAGEMENT
-- =============================================

create table [comment_(poly)] (
    comment_id int identity(1,1) primary key,
    user_id int not null,
    comment_text nvarchar(max) not null,
    created_at datetime2 default getdate() not null,
    commentable_type varchar(20) not null check (commentable_type in ('post', 'photo', 'video')),
    post_id int not null,
    foreign key (user_id) references [user](user_id)
);

create table [like] (
    like_id int identity(1,1) primary key,
    user_id int not null,
    post_id int not null,
    constraint uq_user_post_like unique (user_id, post_id),
    foreign key (user_id) references [user](user_id),
    foreign key (post_id) references [post](post_id) on delete cascade
);

create table [save] (
    save_id int identity(1,1) primary key,
    user_id int not null,
    post_id int not null,
    constraint uq_user_post_save unique (user_id, post_id),
    foreign key (user_id) references [user](user_id),
    foreign key (post_id) references [post](post_id) on delete cascade
);

create table [tag] (
    tag_id int identity(1,1) primary key,
    tag_description nvarchar(100) not null unique
);

create table [tag_post_(poly)] (
    tag_id int not null,
    taggable_type varchar(20) not null check (taggable_type in ('post', 'photo', 'video')),
    taggable_id int not null,
    primary key (tag_id, taggable_type, taggable_id),
    foreign key (tag_id) references [tag](tag_id) on delete cascade
);

-- =============================================
-- 4. GROUPS, MESSAGING, NOTIFICATIONS & REPORTS
-- =============================================

create table [group] (
    group_id int identity(1,1) primary key,
    group_name nvarchar(100) not null,
    description nvarchar(max) null,
    privacy_setting varchar(20) default 'public' check (privacy_setting in ('public', 'private')),
    creation_date datetime2 default getdate() not null
);

create table [role_type] (
    group_id int not null,
    user_id int not null,
    role varchar(20) default 'member' check (role in ('admin', 'moderator', 'member')),
    join_date datetime2 default getdate() not null,
    primary key (group_id, user_id),
    foreign key (group_id) references [group](group_id) on delete cascade,
    foreign key (user_id) references [user](user_id) on delete cascade
);

create table [conversation] (
    conversation_id int identity(1,1) primary key,
    [start_date_&_time] datetime2 default getdate() not null
);

create table [conversation_user] (
    conversation_id int not null,
    user_id int not null,
    primary key (conversation_id, user_id),
    foreign key (conversation_id) references [conversation](conversation_id) on delete cascade,
    foreign key (user_id) references [user](user_id) on delete cascade
);

create table [message] (
    message_id int identity(1,1) primary key,
    conversation_id int not null,
    user_id int not null,
    content nvarchar(max) not null,
    sent_time datetime2 default getdate() not null,
    foreign key (conversation_id) references [conversation](conversation_id) on delete cascade,
    foreign key (user_id) references [user](user_id)
);

create table [notification] (
    notification_id int identity(1,1) primary key,
    user_id_recieptent int not null,
    type varchar(50) not null,
    sent_time datetime2 default getdate() not null,
    foreign key (user_id_recieptent) references [user](user_id) on delete cascade
);

create table [report] (
    report_id int identity(1,1) primary key,
    user_id int not null,
    post_id int not null,
    reason nvarchar(255) not null,
    status varchar(20) default 'pending' check (status in ('pending', 'reviewed', 'resolved')),
    foreign key (user_id) references [user](user_id),
    foreign key (post_id) references [post](post_id) on delete cascade
);

-- INDEXES FOR POLYMORPHIC LOOKUPS
create index ix_comments_polymorphic on [comment_(poly)] (commentable_type, post_id);
create index ix_taggables_polymorphic on [tag_post_(poly)] (taggable_type, taggable_id);
