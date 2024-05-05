using MMMovies.SharedKernel;

namespace MMMovies.Users.Contracts;

public record NewUserAddressAddedIntegrationEvent(UserAddressDetails Details) : IntegrationEventBase;
