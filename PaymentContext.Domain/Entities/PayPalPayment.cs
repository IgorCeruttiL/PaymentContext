using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities;

public class PayPalPayment : Payment
{
    public PayPalPayment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Document document, Adress address, Email email, string transactionCode) : base(paidDate, expireDate, total, totalPaid, payer, document, address, email)
        {
            TransactionCode = transactionCode;

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(0, Total, "Payment.Total", "O total não pode ser zero")
                .IsGreaterOrEqualsThan(Total, TotalPaid, "Payment.TotalPayd",
                    "O valor pago é menor que o valor do pagamento"));
        }

        public string TransactionCode { get; private set; }
}