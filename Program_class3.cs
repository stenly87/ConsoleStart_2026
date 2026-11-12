namespace ConsoleApp2;

public class Program
{
    public static void Main(string[] args)
    {
        Mother mother = new Mother { Name = "Марьюшка" };
        Father father = new Father { Name = "Инокентий" };
        Child child = new Child { Name = "Никита" };
        // подписка на событие
        // child издатель
        // mother подписчик
        child.Poo += father.SeeSomePoo;
        child.Poo += mother.DoSomethingWithPoo;
        child.DoSomething(); // в данном методе вызывается событие
        // событие вызывает подписанные на него методы

        Console.WriteLine($"{father.Name} уходит за хлебом");
        // отец отписывается от события
        child.Poo -= father.SeeSomePoo;

        // отписка от событий часто также необходима, поскольку
        // хранение ссылки на метод это связь между объектами
        // соответственно такие объекты не будут удаляться сборщиком мусора
        // т.е. если нам надо удалить объект из памяти, следует убедиться
        // что он не подписан на чужие события, или на него не подписан

        Calc calc = Sum; // делегат может ссылаться на метод, совпадающий с делегатом по заголовку
        calc += Minus; // делегат может хранить ссылки на несколько методов
        int result = calc(10, 20); // мы получаем результат из последнего метода, хотя выполняются все
        Console.WriteLine(result);
        calc -= Minus; // убираем метод из делегата
        Console.WriteLine(result); // выведет сумму
        // есть какой-то неотсортированный список 
        List<int> list = new List<int>([
            5, 1, 2, 5, 6, 2, 3, 5, 7, 8, 2, 3, 5, 6, 8, 2, 54, 322, 3, 23, 42, 33, 3, 42, 3, 2
        ]);

        Sort(list, Compare3); // мы его сортируем c помощью другого метода
        foreach (int i in list) // мы его выводим на консоль
            Console.WriteLine(i);
        
        
        // DateTime - структура для хранения даты и времени, занимает 8 байт
        DateTime dt = DateTime.Now; // Now - текущая дата и время со сдвигом по Гринвичу
        Console.WriteLine(dt.Year);
        Console.WriteLine(dt.Month);
        // диапазон значений от 01.01.01 до 31.12.9999
        //dt.Year = 100; // нельзя переназначить месяц или год и тп
        dt = dt.AddYears(100); // можно добавить к текущей дате год, месяц и тп
        dt = dt.AddMinutes(-30000);// также можно отнять какое-то значение от даты
        Console.WriteLine(dt);
        Console.WriteLine(dt.ToShortDateString()); // вывод только даты
        Console.WriteLine(dt.ToShortTimeString()); // вывод только времени
        Console.WriteLine(dt.ToString("yyyy-MM-dd HH:mm:ss zz")); // кастомный формат
        DateTime utc = DateTime.Now.ToUniversalTime(); // получить дату в UTC (без сдвига)
        Console.WriteLine(utc);
        DateTime local = utc.ToLocalTime(); // получить дату со сдвигом по Гринвичу (используется локальное время компьютера)
        Console.WriteLine(local);

        // можно сохранить дату в число
        long binaryDate = utc.ToBinary();
        Console.WriteLine(binaryDate);
        DateTime fromBinary = DateTime.FromBinary(binaryDate);
        Console.WriteLine(fromBinary);
        string date = "02.11.25 05:59";
        DateTime parsed = DateTime.Parse(date);
        Console.WriteLine(parsed);
        // метод Parse понимает название месяца
        date = "11 сентября 2001 05:59";
        parsed = DateTime.Parse(date);
        Console.WriteLine(parsed);
    }

    static Random rnd = new Random();
    public static bool Compare1(int a, int b) => a > b;
    public static bool Compare2(int a, int b) => a < b;
    public static bool Compare3(int a, int b) => rnd.Next() % 2 == 0;

    public static void Sort(List<int> list, Compare compare)
    {
        int temp;
        for (int i = 0; i < list.Count; i++)
        for (int j = i; j < list.Count; j++)
        {
            // мы используем делегат в условии вместо жесткого условия if (list[i] < list[j])
            if (compare(list[i], list[j]))
            {
                temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }

    public static int Sum(int a, int b)
    {
        Console.WriteLine("Сложение");
        return a + b;
    }

    public static int Minus(int a, int b)
    {
        Console.WriteLine("Вычитание");
        return a - b;
    }
}

// события в классе
// ключевое слово event после него указывается делегат (ссылка на метод), который
// задает синтаксис метода, который может подписаться на событие
public class Child
{
    public string Name { get; set; }

    public void DoSomething()
    {
        // вызов события на исполнение
        // указывается имя события, далее оператор ?. 
        // ?. - проверка на null, определяет есть ли подписчики
        // если подписчики есть, то эти методы будут вызваны с указанными аргументами
        Poo?.Invoke(this, new EventArgs());

        // Аналогичный код внизу
        //if (Poo != null)
        //    Poo(this, new EventArgs());
    }

    // пример события
    public event EventHandler Poo;
    // EventHandler это делегат, который задает структуру метода
    // у него задается два аргумента object sender и EventArgs e
    // EventArgs это заглушка, в таком виде делегата она не используется
    // object sender - сюда принято передавать ссылку на текущий объект
}

public class Father
{
    public string Name { get; set; }

    public void SeeSomePoo(object? sender, EventArgs e)
    {
        Console.WriteLine($"{Name}: Мать! Ребенок обосрался! Я за хлебом!");
    }
}

public class Mother
{
    public string Name { get; set; }

    public void DoSomethingWithPoo(object? sender, EventArgs e)
    {
        Child child = (Child)sender;
        Console.WriteLine($"{Name}: Ура, мой {child.Name} опять сделал какашку!");
    }
}

// пример существующего делегата:
// public delegate void EventHandler(object? sender, EventArgs e);
// как видно, он отличается от объявления метода только ключевым словом делегат
// делегаты объявляются внутри пространства имен, например
public delegate int Calc(int a, int b);

// делегат для сортировки - принимает 2 аргумента, возвращает bool
public delegate bool Compare(int a, int b);


