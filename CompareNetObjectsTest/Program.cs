

using KellermanSoftware.CompareNetObjects;

Test3();

void Test3()
{
    ComparisonConfig config = new() {MaxDifferences = 100};
    
    CompareLogic compareLogic = new CompareLogic(config);
    Person person1 = new Person() {DateCreated = DateTime.Now, Name = "Greg"};

    Person person2 = new Person() {Name = "John", DateCreated = person1.DateCreated.AddDays(1)};
    var result = compareLogic.Compare(person1, person2);
    if (!result.AreEqual)
    {
        Console.WriteLine(result.DifferencesString);
    }
}
void Test2()
{
    CompareLogic compareLogic = new CompareLogic();
    compareLogic.Config.MaxDifferences = 100;
    Person person1 = new Person() {DateCreated = DateTime.Now, Name = "Greg"};

    Person person2 = new Person() {Name = "John", DateCreated = person1.DateCreated.AddDays(1)};
    var result = compareLogic.Compare(person1, person2);
    if (!result.AreEqual)
    {
        Console.WriteLine(result.DifferencesString);
    }
}

void Test1()
{
    CompareLogic compareLogic = new CompareLogic();
    Person person1 = new Person() {DateCreated = DateTime.Now, Name = "Greg"};

    Person person2 = new Person() {Name = "John", DateCreated = person1.DateCreated.AddDays(1)};
    var result = compareLogic.Compare(person1, person2);
    if (!result.AreEqual)
    {
        Console.WriteLine(result.DifferencesString);
    }
}
public class Person
{
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
}