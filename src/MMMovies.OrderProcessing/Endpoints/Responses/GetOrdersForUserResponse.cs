namespace MMMovies.OrderProcessing.Endpoints.Responses;

public class GetOrdersForUserResponse
{
    public List<OrderSummaryResponse> Orders { get; set; } = [];
}
