using connectify.application.dtos.auth;
using connectify.application.interfaces;
using connectify.application.common.exceptions;
using connectify.domain.entities;
using connectify.domain.interfaces;
using connectify.infrastructure.identity;

namespace connectify.application.services;

public class authservice : iauthservice
{
    private readonly iunitofwork _unitofwork;
    private readonly jwttokengenerator _tokengenerator;

    public authservice(iunitofwork unitofwork, jwttokengenerator tokengenerator)
    {
        _unitofwork = unitofwork;
        _tokengenerator = tokengenerator;
    }

    public async task<authresponsedto> registerasync(registerrequestdto dto)
    {
        var existinguser = await _unitofwork.repository<user>()
            .findasync(u => u.email == dto.email || u.username == dto.username);

        if (existinguser.any())
            throw new validationexception("user with this email or username already exists.");

        var user = new user
        {
            username = dto.username,
            email = dto.email,
            password = dto.password, // BCrypt hashing should be applied here
            date_of_brith = dto.date_of_brith
        };

        await _unitofwork.repository<user>().addasync(user);
        await _unitofwork.completeasync();

        var token = _tokengenerator.generatetoken(user);
        return new authresponsedto(user.id, user.username, user.email, token);
    }

    public async task<authresponsedto> loginasync(loginrequestdto dto)
    {
        var users = await _unitofwork.repository<user>().findasync(u => u.email == dto.email);
        var user = users.firstordefault();

        if (user == null || user.password != dto.password)
            throw new validationexception("invalid email or password.");

        var token = _tokengenerator.generatetoken(user);
        return new authresponsedto(user.id, user.username, user.email, token);
    }
}
