using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Adress : ValueObject
{
    public Adress(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        this.zipCode = zipCode;
        
        AddNotifications(new Contract()
            .Requires()
            .HasMinLen(Street, 3, "Adress.Street", "A rua deve conter pelo menos 3 caracteres")
            .HasMinLen(Number, 1, "Adress.Number", "O numero deve conter pelo menos 1 digito")
            .HasMinLen(Neighborhood, 3, "Adrees.Neighborhood", "O bairro deve conter pelo menos 3 caracteres")
            .HasMinLen(City, 3, "Adress.City", "A cidade deve conter mais que 3 caracteres")
        );
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string zipCode { get; private set; }
}