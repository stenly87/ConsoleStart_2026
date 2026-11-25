using System.Numerics;
using System.Text.Json;

namespace ConsoleApp2;
// Дженерики
public class Program
{
    public static void Main()
    {
        //var result = MyMath.Add<byte>(10, 20);
        //Console.WriteLine(result);
        Chain<string> chain = new Chain<string>();
        chain.Add("единица");
        chain.List();
        Console.WriteLine("--------------");
        chain.Add("двойка");
        chain.Add("тройка");
        chain.List();
        Console.WriteLine("--------------");
        var result = chain.GetById(3);
        Console.WriteLine(result);

        var node = chain.Head;
        node.Print();
        
        try
        {
            Console.WriteLine("Попытка получить значение по несуществующему Id");
            result = chain.GetById(5);
            Console.WriteLine(result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Исключение:" + e.Message);
        }
        
        // стандартные делегаты в платформе, которые тоже используют generic-типы
        // Action это процедура, указываются типы данных для аргументов 
        Action<int> action = i => Console.WriteLine(i);
        Action<int, int> action2 = (x,y) => Console.WriteLine(x + y);
        // Func это функция, указываются типы данных для аргументов и
        // в конце тип данных результата
        Func<int, int, string> func = (x, y) => x + y.ToString();
        Console.WriteLine("Исполнение методов через делегаты");
        action(1);
        action2(2,3);
        var resultFunc = func(1,2);
        Console.WriteLine(resultFunc);
        
        node.SaySomething();
        // получим всю сериализованную цепочку через метод расширения 
        var nodeJson = node.ToJson();
        Console.WriteLine(nodeJson);

        Computer comp = new();
        Console.WriteLine(comp.ToJson());
        comp.SaySomething();
        //comp.OnTurnedOn += (sender, message) => Console.WriteLine(message);
        comp.OnTurnedOn += (sender, data) =>
        {
            Console.WriteLine(data.Title);
            Console.WriteLine("Объем ОЗУ: " +  data.Ram);
            Console.WriteLine("Объем Видеопамяти: " +  data.Ram);
            Console.WriteLine(data.PostMessage);
        };
        comp.TurnOn();
        Console.WriteLine(comp.ToJson());
    }
}

public record BiosData(string Title, string PostMessage, int Ram, int VRam);

public class Computer
{
    public bool IsWorking
    {
        get => field;
        private set
        {
            field = value; 
            //OnTurnedOn?.Invoke(this, "Компьютер работает");
            OnTurnedOn?.Invoke(this, new BiosData("Название и версия Bios-программы",
                "Все системы проверены. GTA6 не пойдет.", 8196, 8196));
        }
    }

    //public EventHandler<string> OnTurnedOn;
    public event EventHandler<BiosData> OnTurnedOn;
    public void TurnOn()
    {
        IsWorking = true;
    }
}

public class MyMath
{
    /*
    public static int Add(int a, int b)
    {
        return a + b;
    }
    public static double Add(double a, double b)
    {
        return a + b;
    }
    public static ulong Add(ulong a, ulong b)
    {
        return a + b;
    }
    */
    public static T Add<T>(T a, T b) where T : IBinaryInteger<T> 
    {
        return a + b;
    }
}

// создание односвязного списка, в котором можно хранить
// любые данные с помощью generic-типа T
public class Node<T>
{
    public int Id { get; private set; }
    public T Value { get; private set; }
    public Node<T> Next { get; set; }

    public Node(T value, int id)
    {
        Value = value;
        Id = id;
    }
}

public class Chain<T>
{
    private int nextId = 1;
    public Node<T> Head { get; private set; }
    public Node<T> Tail { get; private set; }

    public void Add(T value)
    {
        if (Head == null)
        {
            Head = new Node<T>(value, nextId++);
            Tail = Head;
            return;
        }

        var nextNode = new Node<T>(value, nextId++);
        Tail.Next = nextNode;
        Tail = nextNode;
    }

    public void List()
    {
        var current = Head;
        while (current != null)
        {
            Console.WriteLine($"{current.Id}: {current.Value}");
            current = current.Next;
        }
    }

    public T GetById(int id)
    {
        var current = Head;
        while (current != null)
        {
            if (current.Id == id)
                return current.Value;
            current = current.Next;
        }
        throw new Exception("Такого id не существует!");
    }
}

// метод расширения (Extension)
// можно добавить свой метод к любому классу
// ограничения - метод не будет иметь доступ к приватным и защищенным полям и свойства класса
// создается любой статичныый класс
public static class NodeExtension
{
    // ключевое слово this указывает на тот тип данных, к которому мы пишем расширение
    public static void Print<T>(this Node<T> node)
    {
        Console.WriteLine("Это метод расширения, его нет в классе Node");
        Console.WriteLine($"{node.Id} {node.Value}");
    }
    
    // можно добавить свой метод ко всем классам, если прописать метод расширения к object
    public static void SaySomething(this object anywhere)
    {
        Console.WriteLine($"{anywhere}: что-то");
    }

    // можно например добавить возможность сериализовать любой объект
    public static string ToJson(this object anyobject)
    {
        return JsonSerializer.Serialize(anyobject);
    }
}