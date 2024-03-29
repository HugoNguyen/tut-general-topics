﻿using bookify.domain.Abstractions;

namespace bookify.domain.Users.Events;
public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;