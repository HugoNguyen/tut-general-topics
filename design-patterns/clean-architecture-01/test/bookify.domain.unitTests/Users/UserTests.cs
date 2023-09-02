using bookify.domain.unitTests.Infrastructure;
using bookify.domain.Users;
using bookify.domain.Users.Events;
using FluentAssertions;

namespace bookify.domain.unitTests.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void Create_Should_Raise_UseCreatedDomainEvent()
    {
        // Arrange
        var firstName = new FirstName("first");
        var lastName = new LastName("last");
        var email = new Email("test@test.com");

        // Act
        var user = User.Create(firstName, lastName, email);

        // Assert
        var userCreatedDomainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

        userCreatedDomainEvent.UserId.Should().Be(user.Id);
    }
}