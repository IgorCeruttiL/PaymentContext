using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void AdicionarAssinatura()
    {
        var subscription = new Subscription(null);
        var student = new Student("André", "Baltieri", "1234567", "balta@io");
        student.AddSubscription(subscription);
    }
}