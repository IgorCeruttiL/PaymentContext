using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string adress)
    {
        Adress = adress;
    }

    public string Adress { get; private set; }
}