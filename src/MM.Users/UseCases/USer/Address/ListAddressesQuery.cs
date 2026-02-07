using Ardalis.Result;
using MediatR;
using MM.Users.UserEndpoints.Address;

namespace MM.Users.UseCases.USer.Address;
internal record ListAddressesQuery(string EmailAddress) :
  IRequest<Result<List<UserAddressDto>>>;
