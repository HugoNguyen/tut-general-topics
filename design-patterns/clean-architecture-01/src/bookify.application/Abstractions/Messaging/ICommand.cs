using bookify.domain.Abstractions;
using MediatR;

namespace bookify.application.Abstractions.Messaging;
public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

/// <summary>
/// To apply generic constraints
/// </summary>
public interface IBaseCommand
{
}