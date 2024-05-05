using Ardalis.Result;
using MediatR;
using MMMovies.Users.DTOs;

namespace MMMovies.Users.UseCases.User.ListAddresses;
internal record GetAddressesQuery(string EmailAddress) : IRequest<Result<List<UserAddressDto>>>;
