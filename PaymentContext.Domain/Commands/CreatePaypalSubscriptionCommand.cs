using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using ICommand = System.Windows.Input.ICommand;

namespace PaymentContext.Domain.Commands;

public class CreatePaypalSubscriptionCommand : Notifiable, ICommand
{
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
    public string Document { get;  set; }
    public string Email { get;  set; }
    public string TransactionCode { get;  set; }
    public string PaymentNumber { get;  set; }
    public DateTime PaidDate { get;  set; }
    public DateTime ExpireDate { get;  set; }
    public decimal Total { get;  set; }
    public decimal TotalPaid { get;  set; }
    public string Payer { get;  set; }
    public string PayerDocument { get;  set; }
    public string PayerEmail { get;  set; }
    public EDocumentType PayerDocumentType { get;  set; }
    public string Street { get;  set; }
    public string Number { get;  set; }
    public string Neighborhood { get;  set; }
    public string City { get;  set; }
    public string State { get;  set; }
    public string Country { get;  set; }
    public string zipCode { get;  set; }

    public void Validate()
    {
        AddNotifications(new Contract()
            .Requires()
            .IsGreaterThan(0, Total, "Payment.Total", "O total não pode ser zero")
            .IsGreaterOrEqualsThan(Total, TotalPaid, "Payment.TotalPayd",
                "O valor pago é menor que o valor do pagamento"));
    }

    public bool CanExecute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public void Execute(object? parameter)
    {
        throw new NotImplementedException();
    }

    public event EventHandler? CanExecuteChanged;
}