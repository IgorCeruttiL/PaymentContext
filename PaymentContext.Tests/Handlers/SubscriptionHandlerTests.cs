using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;
[TestClass]


public class SubscriptionHandlerTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "Igor";
        command.LastName = "Cerutti";
        command.Document = "99999999999";
        command.Email = "hello@balta.io2";
        command.BarCode = "12345";
        command.BoletoNumber = "1231231";
        command.PaymentNumber = "1234214";
        command.PaidDate = DateTime.Now;
        command.ExpireDate = DateTime.Now.AddMonths(1);
        command.Total = 60;
        command.TotalPaid = 60;
        command.Payer = "Wayne Corp";
        command.PayerDocument = "12345678911";
        command.PayerDocumentType = EDocumentType.CPF;
        command.PayerEmail = "batman@dc.com";
        command.Street = "asdasd";
        command.Number = "asdasd";
        command.Neighborhood = "asdsad";
        command.City = "asdasd";
        command.State = "asdasd";
        command.Country = "asd";
        command.zipCode = "123123";

        handler.Handle(command);
        Assert.AreEqual(false, handler.Valid);

    }
}