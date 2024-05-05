using Ardalis.Result;
using MediatR;

namespace MMMovies.Movies.Contracts;

public record MovieDetailsQuery(Guid movieId) : IRequest<Result<MovieDetailsResponse>>;
