using bookify.application.Abstractions.Messaging;
using bookify.domain.Abstractions;
using bookify.infrastructure;
using System.Reflection;

namespace bookify.architecture.unitTests;

public class BaseTest
{
    protected static Assembly ApplicationAssembly => typeof(IBaseCommand).Assembly;

    protected static Assembly DomainAssembly => typeof(IEntity).Assembly;

    protected static Assembly InfrastructureAssembly => typeof(ApplicationDbContext).Assembly;
}