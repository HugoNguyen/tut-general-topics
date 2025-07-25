﻿using Bogus;
using Evently.Common.Domain;

namespace Evently.Modules.Attendance.UnitTests.Abstractions;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "<Pending>")]
public abstract class BaseTest
{
    protected static readonly Faker Faker = new();

    public static T AssertDomainEventWasPublished<T>(Entity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.DomainEvents.OfType<T>().SingleOrDefault();

        if (domainEvent is null)
        {
            throw new Exception($"{typeof(T).Name} was not published");
        }

        return domainEvent;
    }
}
