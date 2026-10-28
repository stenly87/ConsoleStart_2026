using System.Collections;

namespace ConsoleApp2;

public class Program
{
    public static void Main(string[] args)
    {
        //var table = new Table(600, 1600);
        //table.Height = 1000; // error без части set свойство становится только для чтения
        //Console.WriteLine(table.Height);

        // например есть какое-то кол-во книг
        Book book1 = new Book { Title = "C# за 2 часа" };
        Book book2 = new Book { Title = "C++ за 2 часа" };

        // есть книжная полка, где лежат книги
        BookShelf shelf = new();
        shelf.AddBook(book1);
        shelf.AddBook(book2);

        // мы можем взять книгу с полки по индексу
        Book b1 = shelf[0];
        Console.WriteLine(b1.Title);
        
    }
}

//Пример класса как напоминание
public class Table
{
    public Table(int height, int width)
    {
        Height = height; // запись в свойство возможна даже если нет блока set у свойства
        _width = width;
    }

    //public void SomeTestMethod()
    //{
    //    Height = 500; // внутри назначение в свойство будет работать, если в свойство Height добавить private set;
    //}

    private int _width;
    public int Height { get; } // без блока set, задать значение можно только через конструктор

    public int Width
    {
        get => _width;
        set
        {
            if (value > 1600)
                throw new ArgumentOutOfRangeException(nameof(value), "value must be less than 1600");
            _width = value;
        }
    }
}

public class Human
{
    private string firstName;
    private string lastName;
    private string patronymic;

    public Human(string firstName, string lastName, string patronymic)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.patronymic = patronymic;
    }

    public string FirstName
    {
        get => firstName;
    }

    public string LastName
    {
        get => lastName;
    }

    public string Patronymic
    {
        get => patronymic;
    }

    public string FIO
    {
        get => $"{lastName} {firstName[0]}.{patronymic[0]}.";
    }

    // свойство заменяет такие методы:
    public string GetName() => firstName;
    public void SetName(string name) => firstName = name;
    // тоже самое, в виде свойства:
    public string Name
    {
        get => firstName;
        set => firstName = value;
    }

}

public class Book
{
    public string Title { get; set; }
}

// Индексатор 
public class BookShelf 
{
    List<Book> _books;

    public BookShelf()
    {
        _books = new();
    }
    
    public void AddBook(Book book)
    {
        _books.Add(book);
    }
    
    // индексатор
    public Book this[int index]
    {
        get => _books[index];
        set => _books[index] = value;
    }

    
}

record BookRecord(string title, string author, string publisher, string pages);