using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreatePaypalSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }
    
    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {   // Fail fast validation
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possivel realizar seu cadastro");
        }
        // Verificar se documento já existe
        if(_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este cpf já está em uso");
        // Verificar se o Email já existe
        if(_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está em uso");
        
        // Gerar os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var adress = new Adress(command.Street, command.Number, command.Neighborhood, command.City,command.State, command.Country, command.zipCode);

        // Gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(
            command.BarCode,
            command.BoletoNumber,
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            command.Payer,
            new Document(command.PayerDocument, command.PayerDocumentType),
            adress,
            email
            );
        
        // Relacionamento
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);
        
        // Agrupar notificações
        AddNotifications(name, document, adress, student, subscription, payment);
        
        // Salvar as informações
        _repository.CreateSubscription(student);
        
        // Enviar E-mail de boas vindas
        _emailService.Send(student.Name.ToString(), student.Email.Adress, "Bem vindo ao balta.io", "Sua assinatura foi criada!");
        
        // Retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso");
    }

    public ICommandResult Handle(CreatePaypalSubscriptionCommand command)
    {
        // Fail fast validation
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possivel realizar seu cadastro");
        }
        
        // Verificar se documento já existe
        if(_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este cpf já está em uso");
        // Verificar se o Email já existe
        if(_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está em uso");
        
        // Gerar os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var adress = new Adress(command.Street, command.Number, command.Neighborhood, command.City,command.State, command.Country, command.zipCode);

        // Gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new PayPalPayment(
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            command.Payer,
            new Document(command.PayerDocument, command.PayerDocumentType),
            adress,
            email,
            command.TransactionCode);
        
        
        // Relacionamento
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);
        
        // Agrupar notificações
        AddNotifications(name, document, adress, student, subscription, payment);
        
        // Salvar as informações
        _repository.CreateSubscription(student);
        
        // Enviar E-mail de boas vindas
        _emailService.Send(student.Name.ToString(), student.Email.Adress, "Bem vindo ao balta.io", "Sua assinatura foi criada!");
        
        // Retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso");
    }
}