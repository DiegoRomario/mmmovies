using Ardalis.Result;
using MediatR;

namespace MMMovies.Movies.Contracts;

public record MovieDetailsQuery(Guid MovieId) : IRequest<Result<MovieDetailsResponse>>;
