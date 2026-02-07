using Ardalis.Result;
using MediatR;

namespace MM.Users.UseCases.USer.Address;
public record AddAddressToUserCommand(string EmailAddress,
                      string Street1,
                      string Street2,
                      string City,
                      string State,
                      string PostalCode,
                      string Country) : IRequest<Result>;
