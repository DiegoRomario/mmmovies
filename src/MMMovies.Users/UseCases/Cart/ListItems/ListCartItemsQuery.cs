using Ardalis.Result;
using MediatR;
using MMMovies.Users.DTOs;

namespace MMMovies.Users.UseCases.Cart.ListItems;

public record GetCartItemsQuery(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;
