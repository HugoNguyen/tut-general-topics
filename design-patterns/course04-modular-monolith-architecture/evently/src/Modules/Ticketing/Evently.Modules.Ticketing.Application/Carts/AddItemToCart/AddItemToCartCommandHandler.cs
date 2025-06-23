using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.PublicApi;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Events;

namespace Evently.Modules.Ticketing.Application.Carts.AddItemToCart;

internal sealed class AddItemToCartCommandHandler(
    CartService cartService,
    ICustomerRepository customerRepository,
    IEventsApi eventsApi)
    : ICommandHandler<AddItemToCartCommand>
{
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        TicketTypeResponse? ticketType = await eventsApi.GetTicketTypeAsync(request.TicketTypeId, cancellationToken);

        if (ticketType is null)
        {
            return Result.Failure(TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        var cartItem = new CartItem
        {
            TicketTypeId = ticketType.Id,
            Price = ticketType.Price,
            Quantity = request.Quantity,
            Currency = ticketType.Currency
        };

        await cartService.AddItemAsync(customer.Id, cartItem, cancellationToken);

        return Result.Success();
    }
}
