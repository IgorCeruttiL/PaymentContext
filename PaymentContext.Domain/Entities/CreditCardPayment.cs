using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities;

public class CreditCardPayment
{
    public class CreditPayment : Payment
    {
        public CreditPayment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Document document, Adress address, Email email, string cardHolderName, string cardNumber, string lastTransactionNumber) : base(paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
            
            AddNotifications(new Contract()
                .Requires()
                .IsNullOrEmpty(CardHolderName, "CreditCardPayment.CardHolderName", "Deve ser colocado o nome do cartão")
                .IsGreaterThan(0, Total, "Payment.Total", "O total não pode ser zero")
                .IsGreaterOrEqualsThan(Total, TotalPaid, "Payment.TotalPayd",
                    "O valor pago é menor que o valor do pagamento"));
        }

        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public string LastTransactionNumber { get; private set; }
    }
}