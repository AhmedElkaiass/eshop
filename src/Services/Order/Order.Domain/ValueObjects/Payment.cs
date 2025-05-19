namespace Order.Domain.ValueObjects;

public record class Payment
{
    public string CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string Cvv { get; } = default!;
    public int PaymentMethodId { get; }
    private Payment(string cardNumber,
                    string cardName,
                    string expiration,
                    string cvv,
                    int paymentMethodId)
    {
        CardNumber = cardNumber;
        Expiration = expiration;
        CardName = cardName;
        Cvv = cvv;
        PaymentMethodId = paymentMethodId;
    }
    protected Payment( )
    {
    }
    public static Payment Of(string cardNumber,
                             string expiration,
                             string cardName,
                             string cvv,
                             int paymentMethodId)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(expiration);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
        return new Payment(cardNumber,
                           cardName,
                           expiration,
                           cvv,
                           paymentMethodId);
    }
}