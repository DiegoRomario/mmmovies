using Ardalis.Result;
using MediatR;
using MMMovies.OrderProcessing.Endpoints.Responses;

namespace MMMovies.OrderProcessing.UseCases;

internal record GetOrdersForUserQuery(string EmailAddress) : IRequest<Result<List<OrderSummaryResponse>>>;


