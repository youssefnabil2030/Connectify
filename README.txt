================================================================================
                    CONNECTIFY ARCHITECTURE DOCUMENTATION (ARCH.TXT)
================================================================================

System: Connectify Enterprise Social Platform  
Architecture Style: Clean Architecture / Layered Monolith  
Version: 1.0.0  
Target Platform: .NET 8, EF Core, SQL Server, SignalR, Next.js  

--------------------------------------------------------------------------------
1. SYSTEM OVERVIEW & ARCHITECTURAL PATTERNS
--------------------------------------------------------------------------------

Connectify follows the principles of Clean Architecture (Onion/Hexagonal) to 
enforce strict separation of concerns, maintain high testability, and decouple 
domain models from frameworks or UI dependencies.

+-------------------------------------------------------+
|                   connectify.api                      | (Presentation Layer)
|        Controllers, Middlewares, SignalR Hubs         |
+---------------------------+---------------------------+
                            |
                            v
+-------------------------------------------------------+
|                connectify.application                 | (Application Layer)
|            Services, DTOs, Interfaces, Rules          |
+---------------------------+---------------------------+
                            |
                            v
+-------------------------------------------------------+
|                  connectify.domain                    | (Domain Layer)
|             Entities, Domain Contracts                |
+-------------------------------------------------------+
                            ^
                            |
+---------------------------+---------------------------+
|               connectify.infrastructure               | (Infrastructure Layer)
|         DbContext, Repositories, JWT, SignalR         |
+-------------------------------------------------------+

Architectural Guiding Principles:
1. Dependency Rule: Source code dependencies only point inward. The 
   connectify.domain layer has zero dependencies on external libraries or frameworks.
2. Naming Convention Standard: All namespaces, directories, and filename 
   conventions across backend modules adhere to strict lowercase naming.
3. Polymorphic Relations: Dynamic association of comments/reactions to 
   multiple targets (post, photo, video) via explicit Type Discriminators.
4. Asynchronous First: All database operations and I/O pipelines utilize 
   non-blocking async/await flows.

--------------------------------------------------------------------------------
2. DIRECTORY STRUCTURE BLUEPRINT
--------------------------------------------------------------------------------

connectify/
|-- connectify.domain/
|   |-- common/
|   |   `-- baseentity.cs
|   |-- entities/
|   |   |-- user.cs
|   |   |-- post.cs
|   |   |-- comment.cs
|   |   |-- message.cs
|   |   `-- tag.cs
|   `-- interfaces/
|       |-- igenericrepository.cs
|       `-- iunitofwork.cs
|
|-- connectify.application/
|   |-- common/
|   |   |-- exceptions/
|   |   |   |-- notfoundexception.cs
|   |   |   `-- validationexception.cs
|   |   `-- models/
|   |       `-- apiresponse.cs
|   |-- dtos/
|   |   |-- auth/
|   |   |-- posts/
|   |   |-- comments/
|   |   `-- chat/
|   |-- interfaces/
|   |   |-- iauthservice.cs
|   |   |-- icommentservice.cs
|   |   `-- ichatservice.cs
|   `-- services/
|       |-- authservice.cs
|       |-- commentservice.cs
|       `-- chatservice.cs
|
|-- connectify.infrastructure/
|   |-- persistence/
|   |   |-- connectifydbcontext.cs
|   |   `-- repositories/
|   |       |-- genericrepository.cs
|   |       `-- unitofwork.cs
|   |-- identity/
|   |   `-- jwttokengenerator.cs
|   `-- hubs/
|       |-- chathub.cs
|       `-- notificationhub.cs
|
`-- connectify.api/
    |-- controllers/
    |   |-- authcontroller.cs
    |   |-- postscontroller.cs
    |   |-- commentscontroller.cs
    |   |-- tagscontroller.cs
    |   `-- messagescontroller.cs
    |-- middlewares/
    |   `-- exceptionhandlingmiddleware.cs
    |-- appsettings.json
    `-- program.cs

--------------------------------------------------------------------------------
3. MODULE SPECIFICATIONS & LAYER RESPONSIBILITIES
--------------------------------------------------------------------------------

3.1 Domain Layer (connectify.domain)
- Role: Represents state, domain models, and core abstraction contracts.
- Key Contracts:
  * igenericrepository<t>: Defines generic CRUD and specification queries.
  * iunitofwork: Manages transaction boundaries across multiple repositories.

3.2 Application Layer (connectify.application)
- Role: Implements application-specific business logic, orchestrates data flows, 
  validates input data, and handles mapping.
- Core Abstractions:
  * iauthservice: Registration, authentication, and token minting logic.
  * icommentservice: Polymorphic comment creation and retrieval logic.
  * ichatservice: Chat message persistence and channel history handling.
- Error Handling: Standardized custom exceptions (notfoundexception, 
  validationexception) wrapped inside apiresponse<t>.

3.3 Infrastructure Layer (connectify.infrastructure)
- Role: External implementation details (EF Core ORM, SQL Server engine, JWT 
  security generation, SignalR real-time hubs).
- Key Components:
  * connectifydbcontext: Database relational configurations and entity mapping.
  * chathub & notificationhub: WebSockets endpoints for real-time messaging 
    and events.

3.4 API / Presentation Layer (connectify.api)
- Role: Exposes REST API endpoints, handles HTTP request/response pipelines, 
  CORS policies, and global middleware execution.
- Key Endpoints:

  Controller         | Route                                 | Method     | Description
  -------------------|---------------------------------------|------------|-----------------------------------
  authcontroller     | /api/v1/auth/register                 | POST       | User registration
                     | /api/v1/auth/login                    | POST       | User login & JWT issuance
  postscontroller    | /api/v1/posts                         | POST / GET | Create & list social posts
  commentscontroller | /api/v1/comments                      | POST       | Polymorphic comment creation
                     | /api/v1/comments/{targetType}/{id}    | GET        | Fetch comments by target
  messagescontroller | /api/v1/messages                      | POST / GET | Direct message actions

--------------------------------------------------------------------------------
4. CROSS-CUTTING CONCERNS
--------------------------------------------------------------------------------

Global Exception Pipeline:
Exceptions are intercepted globally by exceptionhandlingmiddleware and 
translated to standard HTTP status codes:
- validationexception -> 400 Bad Request
- notfoundexception   -> 404 Not Found
- exception           -> 500 Internal Server Error

Response Schema:
{
  "is_success": false,
  "message": "Error description details",
  "data": null
}

Security & Real-Time Setup:
- JWT Authentication: Symmetric Key Bearer validation active across protected routes.
- CORS: Configured for local Next.js client origin (http://localhost:3000) with 
  WebSockets/SignalR credentials support enabled.
=======================================================================================================
THIS PROJECT INCULED IN THE INTERNSHIP PROGRAM OF MISR INSURANCE IN DEPARTMENT OF SOFTWARE ENGINEERNIG 
THE PROJECT REPRESENT THE TECHINCAL TEQUINCES OF BUILDING SCALABLE SYSTEMS .
=======================================================================================================
