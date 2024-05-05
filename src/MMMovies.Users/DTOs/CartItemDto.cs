namespace MMMovies.Users.DTOs
{
    public record CartItemDto(Guid Id, Guid MovieId, string Description, int Quantity, decimal UnitPrice);
}