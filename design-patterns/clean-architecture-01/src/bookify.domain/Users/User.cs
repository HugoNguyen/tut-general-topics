﻿using bookify.domain.Abstractions;
using bookify.domain.Users.Events;

namespace bookify.domain.Users;
public sealed class User : Entity<UserId>
{
    private User () { }

    private User(UserId id, FirstName firstName, LastName lastName, Email email)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;

    /// <summary>
    /// For encapsulation, hide constructor method
    /// </summary>
    /// <returns></returns>
    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        var user = new User(UserId.New(), firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
