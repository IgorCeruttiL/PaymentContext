using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Queries;
[TestClass]
public class StudentQueriesTests
{
    private readonly IList<Student> _students;

    public StudentQueriesTests(IList<Student> students)
    {
        _students = students;
        for (var i = 0; i <= 10; i++)
        {
            _students.Add(new Student(
                new Name("Aluno", i.ToString()), 
                new Document("12345678910" + i.ToString(), EDocumentType.CPF),
                new Email(i.ToString() +  "@balta.io")
                ));
        }
    }

    [TestMethod]
    public void ShouldReturnNullWhenDocumentNotExist()
    {
        var exp = StudentQueries.GetStudentInfo("12345678911");
        var student = _students.AsQueryable().Where(exp).FirstOrDefault();
        
        Assert.AreEqual(null, student);
    }
    
    [TestMethod]
    public void ShouldReturnStudentWhenDocumentExist()
    {
        var exp = StudentQueries.GetStudentInfo("12345678910");
        var student = _students.AsQueryable().Where(exp).FirstOrDefault();
        
        Assert.AreNotEqual(null, student);
    }
}