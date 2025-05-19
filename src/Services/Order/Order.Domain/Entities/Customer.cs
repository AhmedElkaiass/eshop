namespace Order.Domain.Entities;

public class Customer : Entity<CustomerId>
{
    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string? PhoneNumber { get; private set; }
    public static Customer Create(CustomerId id, string fullName, string email, string phoneNumber)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(fullName);
        ArgumentNullException.ThrowIfNullOrEmpty(email);
        return new Customer
        {
            Id = id,
            FullName = fullName,
            Email = email,
            PhoneNumber = phoneNumber
        };
    }
}
