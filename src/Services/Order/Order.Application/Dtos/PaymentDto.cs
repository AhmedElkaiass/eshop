namespace Order.Application.Dtos;

public record PaymentDto(
    string CardNumber,
    string ExpiryDate,
    string CardType,
    string Cvv,
    int PaymentMethodId
);