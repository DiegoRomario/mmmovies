using Ardalis.Result;
using MediatR;

namespace MMMovies.Users.UseCases.Cart.AddItem;

public record AddItemToCartCommand(Guid MovieId, int Quantity, string EmailAddress) : IRequest<Result>;
