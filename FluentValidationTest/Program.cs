

using FluentValidation;
using FluentValidation.Results;
using System.Text.Json;

Test1();

void Test1()
{
    var customer = new Customer();
    CustomerValidator validator = new CustomerValidator();
    ValidationResult result = validator.Validate(customer);
    if (!result.IsValid)
    {
        foreach (var failure in result.Errors)
        {
            Console.WriteLine($"Property: {failure.PropertyName} failed validation. Error was: {failure.ErrorMessage}");
        }
    }
}

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Surname).NotNull().NotEqual("foo");
        RuleFor(customer => customer.Address).SetValidator(new AddressValidator());
        RuleForEach(x => x.Orders).SetValidator(new OrderValidator());
    }
}

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(address => address.Postcode).NotNull();
    }
}

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleForEach(x => x.AddressLines).NotNull()
            .WithMessage("Address {CollectionIndex} is required.");
    }
}

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.Total).GreaterThan(0);
    }
}



public class Person
{
    public List<string> AddressLines { get; set; } = new();
}
public class Customer 
{
    public int Id { get; set; }
    public string Surname { get; set; }
    public string Forename { get; set; }
    public decimal Discount { get; set; }
    public Address Address { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
}

public class Order
{
    public double Total { get; set; }
}

public class Address 
{
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Town { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
}