using MMMovies.OrderProcessing.Domain;

namespace MMMovies.OrderProcessing.Data.Cache;

// This is the materialized view's data model
internal record OrderAddress(Guid Id, Address Address);
