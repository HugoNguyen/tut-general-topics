namespace Evently.Modules.Users.IntegrationTests.Abstractions;

[CollectionDefinition(nameof(IntegrationTestCollection))]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "<Pending>")]
public sealed class IntegrationTestCollection : ICollectionFixture<IntegrationTestWebAppFactory>;
