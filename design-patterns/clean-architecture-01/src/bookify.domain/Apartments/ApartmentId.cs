﻿namespace bookify.domain.Apartments;
public record ApartmentId(Guid Value)
{
    public static ApartmentId New() => new(Guid.NewGuid());
}

