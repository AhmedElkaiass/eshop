namespace Order.Application.Dtos;

public record AddressDto(
    string FirstName,
    string LastName,
    string Email,
    string AddressLine,
    string ZipCode,
    string Country,
    string State
);
