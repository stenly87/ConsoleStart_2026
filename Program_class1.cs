// классы
// классы используются для определения новых типов данных
// ключевое слово class используется для определения новых
// классов
// синтаксис
/*
модификаторы class Наименование : Наследование/Реализация
{
    тело класса
}
*/
// использование классов Student и Group
/*
public class Program
{
    public static void Main(string[] args)
    {
        Student denis = new Student("Денис");
        denis.LastName = "Иванов";
        denis.Age = 20;

        Student.SomeStaticMethod();
        //denis.PrintInfo();
        //ivan.FirstName = "Иван"
        Student ivan = new Student("Никита", "Денисов", 25);

        //ivan.FirstName = "Иван";
        //ivan.LastName = "Денисов";
        //ivan.Age = 25;

        //ivan.PrintInfo();

        Group group = new Group { Title = "1125" };
        //var list = group.students; // мы не может получить доступ к приватным полям
        group.AddStudent(denis);
        group.AddStudent(ivan);

        group.PrintInfo();
    }
}
*/

public class Program
{
    public static void Main(string[] args)
    {
        /*CreateSomeObjects();
        GC.Collect(); // принудительный вызов сборщика мусора
        Console.ReadLine(); // мы не завершаем программу сразу, чтобы сборщик мусора успел отработать
        */
        
        Human human = new();
        human.FirstName = "Петр";
        human.LastName = "Петрович";
        //human._lastName = ""; //Cannot access private field '_lastName' here
        Console.WriteLine(human.FIO); //"Петр Петрович"

        human.Age = 40;
        Console.WriteLine(human.Age); // 40
        human.Age = -20; // за счет проверки внутри свойства мы не допустим неправильного значения 
        Console.WriteLine(human.Age); // значение свойства останется без изменений
    }
    public static void CreateSomeObjects()
    {
        // переменная test должна быть уничтожена после окончания метода
        // т.е. уничтожится ссылка, а объекты в памяти останутся сиротами
        // этих сирот найдет и добьет сборщик мусора
        KillMe test;
        for(int i = 0 ; i < 100000; i++)
            test = new KillMe();
    }
}

// использование класса CatDog
/*
public class Program
{
    public static void Main(string[] args)
    {
        CatDog catdog = new CatDog { name = "Барсошарик" };
        catdog.PrintInfo();
        string command;
        do
        {
            command = Console.ReadLine();
            switch (command)
            {
                case "feed":
                    Console.WriteLine("Чем будем кормить?");
                    catdog.Feed(Console.ReadLine());
                    break;
                case "sleep":
                    catdog.Sleep();
                    break;
                case "drink":
                    Console.WriteLine("Чем будем поить?");
                    catdog.GiveDrink(Console.ReadLine());
                    break;
            }
        } while (command != "exit");

    }
}*/


// тело класса состоит из данных и поведения
// пример - класс Student, экземпляр, которого хранит имя, фамилию и возраст студента
// класс это шаблон, по которому создаются экземпляры
// экземпляром класса называется переменная с типом этого класса
// Student denis = new Student(); //<- экземпляр
// public string FirstName и остальные - поля класса
// каждый экземпляр хранит свои собственные значения для полей
// обратиться к полям без инициализации экземпляра нельзя
// ключевое слово new вызывает на исполнение конструктор
// класса. Конструктор выделяет память для инициализации 
// полей класса. Класс также может иметь набор собственных 
// конструкторов для контроля инициализации полей
// конструктор внутри класса определяется схожим образом с методом
// название конструктора совпадает с названием класса
// у конструктора нет типа возвращаемого значения
// может быть несколько конструкторов с разным набором аргументов
// если мы определяем свой конструктор, то конструктор
// по умолчанию (без аргументов) перестает быть доступным
// конструктор может передать часть своих аргументов на обработку
// в другой конструктор (тогда тот выполнится первее)

// модификаторы доступа public private internal protected file
// используются для определения доступности элемента (класса/поля/метода и тп)
// например, класс может быть публичным - т.е. доступным по всему приложению и даже за его пределами
// а поле может быть приватным - оно будет доступно только внутри класса
// public - самый расширенный доступ - элемент доступен отовсюду
// internal - доступ задается внутри сборки
// file - доступ ограничен текущим файлом
// protected - элемент доступен внутри класса и внутри потомков класса
// private - доступ только внутри класса
internal class Student
{
    public string FirstName;
    public string LastName;
    public double Age;

    private static string school = "Какая-то школа";

    static Student() // статичный конструктор
    {
        school = "";
        SomeStaticMethod();
        // Age = 20; нет доступа к полям экземпляра
    }

    public Student(string firstName)
    {
        if (firstName == "Никита")
            Console.WriteLine("Бывает...");
        FirstName = firstName;
        // обращение к статичному полю
        Console.WriteLine($"Новый ученик в школе: {school}");
    }

    public static void SomeStaticMethod()
    {
        // статичный метод не может обращаться к полям экземпляра
        school = "Теперь тут другая школа";
    }

    public Student(string firstName, string lastName)
        : this(firstName) // передаем firstName на обработку в конструктор с 1 аргументом
    {
        LastName = lastName;
        Age = (DateTime.Today - DateTime.Parse("01.01.2000")).TotalSeconds;
    }

    public Student(string firstName, string lastName, double age = 20)
        : this(firstName, lastName)
    {
        Age = age;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{FirstName} {LastName}. Возраст: {Age} секунд");
    }
}

public class Group
{
    public string Title;
    List<Student> students; // private

    public Group()
    {
        students = new();
    }

    // для использования Student как аргумент, метод не может быть public
    // максимум internal
    internal void AddStudent(Student student)
    {
        students.Add(student);
    }

    internal void PrintInfo()
    {
        students.ForEach(student => student.PrintInfo());
    }
}

// пример класса с приватным состоянием и публичными методами
public class CatDog
{
    public string name;
    int hungry;
    int weakness;
    int thirsty;

    private Random rnd = new();

    public void PrintInfo()
    {
        Console.WriteLine($"Наш котопёс по имени {name}");
        Console.WriteLine($"Показатель \"Хочет жрать\": {hungry}%");
        Console.WriteLine($"Показатель \"Хочет пить\": {thirsty}%");
        Console.WriteLine($"Показатель \"Усталость\": {weakness}%");
    }

    public void Feed(string food)
    {
        Console.WriteLine($"Котопёс сожрал {food}");
        hungry -= rnd.Next(10, 30);
        thirsty += rnd.Next(5, 10);
        weakness += rnd.Next(10, 30);

        if (rnd.Next(1, 20) == 1)
        {
            Console.WriteLine($"Котопёс подавился и выблевал все и даже больше");
            hungry += rnd.Next(40, 70);
            thirsty += rnd.Next(30, 60);
            weakness += rnd.Next(10, 30);
        }

        NormalizeData();
        PrintInfo();
    }

    public void GiveDrink(string drink)
    {
        Console.WriteLine($"Котопёс выпил {drink}");
        thirsty -= rnd.Next(50, 80);
        weakness += rnd.Next(10, 30);

        if (rnd.Next(1, 20) < 10)
        {
            Console.WriteLine($"Котопёс подавился и выблевал все и даже больше");
            hungry += rnd.Next(40, 70);
            thirsty += rnd.Next(50, 90);
            weakness += rnd.Next(10, 30);
        }

        NormalizeData();
        PrintInfo();
    }

    public void Sleep()
    {
        Console.WriteLine($"Котопёс неплохо подрых");
        hungry += rnd.Next(30, 50);
        thirsty += rnd.Next(30, 50);
        weakness -= rnd.Next(50, 80);

        NormalizeData();
        PrintInfo();
    }

    private void NormalizeData()
    {
        if (hungry <= 0) hungry = 0;
        if (thirsty < 0) thirsty = 0;
        if (weakness <= 0) weakness = 0;

        if (hungry > 100) hungry = 100;
        if (thirsty > 100) thirsty = 100;
        if (weakness > 100) weakness = 100;
    }
}

// static - помечает класс или элемент класса статичным
// статичные элементы класса существуют в единственном экземпляре
// для использования статичных элементов не нужны экземпляры класса
// класс помеченный static нельзя инстанциировать (создать его экземпляр)
// статичные методы могут взаимодействовать только со статичными элемента класса
// публичный статичный метод можно вызвать извне через название класса
// можно создать статичный конструктор, такой конструктор может быть только один
// он вызывается один раз при первом обращении к классу и также может 
// обращаться только к статичным элементам класса
// у статичного конструктора не может быть аргументов, он всегда private


// деструктор - можно объявить в классе один деструктор, который будет
// заниматься освобождением и очисткой памяти при уничтожении объектов
// сложность - обычно мы не можем точно предсказать, когда деструктор 
// будет вызван, поскольку мы напрямую не занимается уничтожением объектов
// уничтожением занимается сборщик мусора Garbage Collector - стандартная
// часть фреймворка. Сборщик мусора периодически сканирует память приложения
// и уничтожает объекты-сироты. Сиротами называют объекты, на которые
// никто не ссылается. Можно вызвать сборщик принудительно GC.Collect();
// не стоит писать важный код в деструктор, поскольку нет гарантии,
// что деструктор вообще вызовется
// у деструктора имя совпадает с классом, вместо модификатора доступа
// у деструктора стоит знак ~, аргументов нет

class KillMe
{
    private static int autoincrement = 1;

    private string name;

    public KillMe()
    {
        name = "Имя " + autoincrement++;
    }
    // деструктор
    ~KillMe()
    {
        Console.WriteLine($"Экземпляр {name} уничтожен");
    }
}

// свойства - элементы, с помощью которых можно управлять полями
// поля в классе принято создать как private или protected
// с помощью свойств можно производить дополнительные проверки перед
// назначением полей или производить вычисления и возвращать результат

public class Human
{
    // в свойствах есть две части get; set;
    // get - аксессор (чтение)
    // set - мутатор (изменение)
    // свойство доступное по умолчанию на чтение и запись
    // описывается кратким синтаксисом: { get; set; }
    public string FirstName { get; set; } // для свойства FirstName компилятор создаст приватное поле _firstName

    //свойство FirstName можно представить как два метода:
    //public string GetFirstName() => _firstName;
    //public void SetFirstName(string _name) => _firstName = _name;
    //свойства, как и методы, обозначаются с большой буквы, обычно публичные
    
    // полная запись свойства предполагает поле
    private string _lastName;
    // и само свойство с расписанным телом:
    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }
    
    int age;
    public int Age
    {
        get
        {
            return age; // если у get есть тело, оно должно завершаться через return
        }
        set
        {
            if (value < 0) // value ключевое слово, с помощью которого
                return; // мы получаем доступ к назначаемому значению
            if (value > 100)
                Console.WriteLine("Долгожитель детектед");
            age = value;
        }
    }
    
    // свойство FIO будет доступно только для чтения
    public string FIO { get => $"{FirstName} {LastName}"; }
}
