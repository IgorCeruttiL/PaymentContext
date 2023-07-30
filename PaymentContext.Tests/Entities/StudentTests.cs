using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Adress _adress;
    private readonly Email _email;
    
    private readonly Student _student;
    
    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Igor", "Cerutti");
        _document = new Document("43397406938", EDocumentType.CPF);
        _email = new Email("igor@gmail.com");
        _adress = new Adress("gothan", "123", "gothan", "gothan","SP", "BR", "12345667");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
          
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenActiveSubscription()
    {
        var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne", _document, _adress, _email, "123456778");
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);
            
        Assert.IsTrue(_student.Invalid);
    }
    
    public void ShouldReturnErrorWhenSubscriptionHadNoPayment()
    {
        _student.AddSubscription(_subscription);;
        Assert.IsTrue(_student.Invalid);
    }
    
    public void ShouldReturnSucessWhenHadNoActiveSubscription()
    {
        var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne", _document, _adress, _email, "123456778");
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        
        Assert.IsTrue(_student.Valid);
    }
}