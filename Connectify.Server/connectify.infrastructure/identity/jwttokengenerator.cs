using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using connectify.domain.entities;
using microsoft.extensions.configuration;
using microsoft.identitymodel.tokens;

namespace connectify.infrastructure.identity;

public class jwttokengenerator
{
    private readonly iconfiguration _configuration;

    public jwttokengenerator(iconfiguration configuration)
    {
        _configuration = configuration;
    }

    public string generatetoken(user user)
    {
        var tokenhandler = new jwtsecuritytokenhandler();
        var key = encoding.ascii.getbytes(_configuration["jwt:secret"] ?? "super_secret_key_connectify_2026");

        var tokendescriptor = new securitytokendescriptor
        {
            subject = new claimsidentity(new[]
            {
                new claim(claimtypes.nameidentifier, user.id.tostring()),
                new claim(claimtypes.name, user.username),
                new claim(claimtypes.email, user.email)
            }),
            expires = datetime.utcnow.adddays(7),
            signingcredentials = new signingcredentials(new symmetricsecuritykey(key), securityalgorithms.hmacsha256signature)
        };

        var token = tokenhandler.createtoken(tokendescriptor);
        return tokenhandler.writetoken(token);
    }
}
