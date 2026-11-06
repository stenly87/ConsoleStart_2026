using System.Reflection.Metadata;
using System.Text.Json;

namespace ConsoleApp2;

/// <summary>
/// Наследование - возможность языка создавать новые классы на основе
/// уже существующих. Наследование позволяет создавать иерархии по принципу
/// родительский объект - дочерний объект. Дочерний объект расширяет родительский
/// Чтобы унаследовать класс, нужно после имени нового класса
/// через двоеточие указать наследуемый класс
/// Дочерний класс наследует (т.е. получает) все состояние и поведение
/// родительского класса кроме элементов с модификатором private
/// Оговорка - приватные данные никуда не деваются, просто мы не можем к ним
/// обращаться напрямую в дочернем классе
/// Мы можем в любой момент преобразовать дочерний класс к родительскому
/// Существует ограничение - один класс может наследовать один класс
/// Также класс может наследовать множество интерфейсов, они перечисляются
/// после наследуемого класса
/// Дочерние классы могут изменять (переопределять) поведение родительского класса
/// Это возможно для элементов с модификатором virtual
/// Дочерний объект в любой момент можно преобразовать к любому из его
/// родительских классов, а наоборот сделать нельзя
/// т.е. если изначально создан экземляр типа Human, его нельзя преобразовать
/// к любому из его наследников, посколько для этого необходимо состояние наследников, а его нет
/// существует модификатор abstract - им можно помечать классы и элементы внутри классов
/// abstract дает возможность не реализовывать методы и запрещает создавать экземпляры классов
/// </summary>
public class Program_class4
{
    /// <summary>
    /// Что это за метод
    /// </summary>
    /// <param name="args">Что это за аргумент</param>
    public static void Main(string[] args)
    {
        /*List<Human> family = new();
        family.Add(new Woman { Surname = "Петрова" });
        family.Add(new Man { Surname = "Петров" });
        family.Add(new Granny { Surname = "Петрова" });
        family.Add(new Granny { Surname = "Петрова" });
        family.Add(new Granny { Surname = "Петрова" });
        family.Add(new Grandpa { Surname = "Петров" });
        family.Add(new GrandNina { Surname = "Сидорова" });
        family.Add(new Sister { Surname = "Петрова" });
        family.Add(new Sister { Surname = "Петрова" });
        foreach (var human in family)
            human.Speak();

        Human abstractHuman = new();
        //Man man = (Man)abstractHuman; // это компилируется, но при выполнении будет ошибка  Unable to cast object of type...
        Man man = new();
        abstractHuman = man; // такое действие возможно в любой момент, поскольку дочерний класс имеет состояние родительского класса
        Man man2 = (Man)abstractHuman;// теперь преобразование сработает! потому что в памяти ссылка типа Human указывает на объект типа Man*/
        
        Goblin gob1 = new Goblin();
        Goblin gob2 = new Goblin();
        Goblin gob3 = new Goblin();
        
        Orc orc1 = new Orc();
        gob1.Attack(orc1);
        gob2.Attack(orc1);
        orc1.Attack(gob3);
    }
}

public class Human
{
    public string Surname { get; set; }

    // virtual - разрешает дочерним классам переопределить этот метод
    public virtual void Speak()
    {
        Console.WriteLine($"{this}: Ля-ля-ля");
    }

    // ToString существует везде, поскольку все типы в фреймворке неявно наследуются от object
    // назначение метода - получить строковое представление объекта
    // для классов ToString по умолчанию возвращает полное имя класса
    // мы поменяли работу ToString на возрат краткого имени
    public override string ToString()
    {
        return this.GetType().Name;
    }
}

public class Woman : Human
{
    // класс Woman решил перепределить действие родительского метода
    // на свой вариант, для этого создается такой же метод, как у родителя
    // с модификатором override
    public override void Speak()
    {
        Console.WriteLine($"{this}: Ля-ля-ля-ля-ля-ля-ля-ля-ля-ля-ля-ля-ля-ля");
    }
}

public class Man : Human
{
}

public class Granny : Woman
{
    public override void Speak()
    {
        Console.WriteLine($"{this}: Кушай супчик, кушай творожок, пей молочко");
    }
}

public class Grandpa : Man
{
}

public class Sister : Woman
{
    
}

public class GrandNina : Granny
{
    public void CookPieWithCow()
    {
    }
}
// есть описание абстрактного монстра
// мы не можем создать экземпляр этого типа
public abstract class Monster
{
    private int _hitPoints;
    public string Name { get; set; }

    public int HitPoints
    {
        get => _hitPoints;
        protected set
        {
            _hitPoints = value;
            if (_hitPoints < 0)
            {
                Console.WriteLine($"{Name} был измельчен в кровавое месиво");
                _hitPoints = 0;
            }
           
            if (_hitPoints == 0)
            {
                IsAlive = false;
                Console.WriteLine($"{Name} умирает");
            }
        }
    }
    
    public bool IsAlive { get; protected set; }

    public int MaxHitPoints { get; protected set; }

    // метод тоже может быть абстрактным, мы опускаем его реализацию
    // реализацию должен представить дочерний класс
    public abstract void Attack(Monster monster);

    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
        Console.WriteLine($"{Name} получил урон {damage}. Остаток здоровья {HitPoints}/{MaxHitPoints}.");
    }
}

public class Orc : Monster
{
    public Orc()
    {
        Name = "Орк";
        HitPoints = MaxHitPoints = 100;
    }
    
    public override void Attack(Monster monster)
    {
        Console.WriteLine($"{Name} берет здоровую дубину и бьет {monster.Name} по голове");
        monster.TakeDamage(20);
    }
}

public class Goblin : Monster
{
    public Goblin()
    {
        Name = "Гоблин";
        HitPoints = MaxHitPoints = 10;
    }

    public override void Attack(Monster monster)
    {
        Console.WriteLine($"{Name} бросается под ноги {monster.Name}. Тот спотыкается и ударяется. Гоблину тоже больно.");
        monster.TakeDamage(5);
        TakeDamage(10);
    }
}