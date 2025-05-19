namespace Order.Domain.ValueObjects;

public record class Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string Email { get; } = default!;
    public string AddressLine { get; } = default!;
    public string ZipCode { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    protected Address( )
    {
    }
    private Address(string firstName,
                    string lastName,
                    string email,
                    string addressLine,
                    string zipCode,
                    string country,
                    string state)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AddressLine = addressLine;
        ZipCode = zipCode;
        Country = country;
        State = state;
    }

    public static Address Of(string firstName,
                             string lastName,
                             string email,
                             string addressLine,
                             string zipCode,
                             string country,
                             string state)
    {
        return new Address(firstName,
                           lastName,
                           email,
                           addressLine,
                           zipCode,
                           country,
                           state);
    }
}
