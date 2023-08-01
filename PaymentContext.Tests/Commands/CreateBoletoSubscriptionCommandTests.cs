using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands;
[TestClass]
public class CreateBoletoSubscriptionCommandTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenNameIsInvalid()
    {
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "";
        command.LastName = "";
        
        command.Validate();
        Assert.AreEqual(false, command.Valid);
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenNameIsValid()
    {
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "igor";
        command.LastName = "lima";
        
        command.Validate();
        Assert.AreEqual(true, command.Valid);
    }
    
}