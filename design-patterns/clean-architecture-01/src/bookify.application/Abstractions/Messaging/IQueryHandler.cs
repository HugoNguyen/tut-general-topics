using bookify.domain.Abstractions;
using MediatR;

namespace bookify.application.Abstractions.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
