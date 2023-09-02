using bookify.domain.Abstractions;
using MediatR;

namespace bookify.application.Abstractions.Messaging;

/// <summary>
/// Every Response will be wrap inside Result
///     Can determine the result is success of fail
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
