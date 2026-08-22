using connectify.application.dtos.auth;

namespace connectify.application.interfaces;

public interface iauthservice
{
    task<authresponsedto> registerasync(registerrequestdto dto);
    task<authresponsedto> loginasync(loginrequestdto dto);
}
