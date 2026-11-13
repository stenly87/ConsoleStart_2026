namespace ConsoleApp2;

// интерфейсы
/*
ссылочный тип данных, который используется для описания возможностей классов
экземпляры интерфейса не создаются сами по себе (да и напрямую)
объявляется с помощью ключевого слова interface, внутри определяются:
свойства, методы, события, делегаты, статичные поля, константы
при этом реализация не указывается - это особенность интерфейса, он объявляет, но не реализует
с версии c# 8.0 интерфейсы могут содержать реализацию по умолчанию для методов, но она вызывается явным образом через интерфейс
пример:
interface ISomeInterface
{ // можно объявить в интерфейсе
    const int CONST_MUST = 10;
    void SomeMethod();
    static int field;
    public string SomeProperty { get; set; }
    event EventHandler someEvent;
    delegate void SomeDelegate();
}
*/
interface ICanSay
{
    void SayHello();
    void SayGoodbye();
}

interface ICanSleep
{
    void Sleep();
}

// использование интерфейса сводится к его реализации
// ВАЖНО интерфейсов в отличии от классов можно реализовать несколько
// интерфейсы в наследовании перечисляются после наследуемого класса
public class Valera : ICanSay, ICanSleep
{
    public void SayHello()
    {
        Console.WriteLine("Hello");
    }

    public void SayGoodbye()
    {
        Console.WriteLine("Goodbye");
    }

    public void Sleep()
    {
        Console.WriteLine("Valera sleep");
    }
}

// Пример, показывающий, как интерфейсами можно заменить абстрактные классы
public abstract class Animal
{
    public string ClassName { get; set; }
    override public string ToString() => ClassName;
}

public interface ISweemming
{
    public void Sweem()
    {
        Console.WriteLine($"{this} sweeming");
    }
}

public class Duck : Animal, ISweemming, IFlying
{
    private string name;
    public Duck(string name)
    {
        // через this мы обращаемся к элементам класса
        this.name = name;
        ClassName = "Duck";
    }
}

public class Duckbill : Animal, ISweemming
{
    public Duckbill()
    {
        ClassName = "Duckbill";
    }

    // определяем свою реализацию интерфейса
    public void Sweem()
    {
        Console.WriteLine($"{this} sweeming как-нибудь по-другому ");
    }
}

public class Crocodile : Animal, ISweemming
{
    public Crocodile()
    {
        ClassName = "Crocodile";
    }
}

public interface IFlying 
{
    public void Fly()
    {
        Console.WriteLine($"{this} flying");
    }
}

public class Dodo : Animal, IFlying
{
    public Dodo()
    {
        ClassName = "Dodo";
    }
}

public class Zoo
{
    List<Animal> animals = new();

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
    }

    public void RunZooLife()
    {
        foreach (var animal in animals)
        {
            // с помощью ключевого слова is мы можем проверить, реализует ли
            // объект указанный тип
            if (animal is ISweemming sweemming)
                sweemming.Sweem();
            if (animal is IFlying flying)
                flying.Fly();            
            
        }
    }
}

public class Program_interface1
{
    public static void Main(string[] args)
    {
        // так нельзя, не скомпилируется
        // ISomeInterface obj = new ISomeInterface();

        /*Valera valera = new Valera();
        valera.SayHello();
        valera.SayGoodbye();
        valera.Sleep();

        // можно создать ссылку с типом интерфейса,
        // которая будет указывать на экземпляр класса, реализующего этот интерфейс
        ICanSay canSay = (ICanSay)valera;
        canSay.SayGoodbye();
        canSay.SayHello();

        ICanSleep canSleep = valera;
        canSleep.Sleep();*/

        Zoo zoo = new Zoo();
        zoo.AddAnimal(new Duck("Утя"));
        zoo.AddAnimal(new Duckbill());
        zoo.AddAnimal(new Crocodile());
        zoo.AddAnimal(new Dodo());
        
        zoo.RunZooLife();
    }
}