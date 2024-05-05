namespace MMMovies.Users.Endpoints.Requests;

internal record AddAddressRequest(string Street1,
                                  string Street2,
                                  string City,
                                  string State,
                                  string PostalCode,
                                  string Country);
