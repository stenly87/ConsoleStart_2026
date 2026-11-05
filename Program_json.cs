using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ConsoleApp2;
/// <summary>
/// Сериализация и десериализация
/// Сериализация - сохранение объекта из памяти в файл (или строку)
/// Десериализация - чтение объекта из строки/файла
/// Существуют разные форматы в которые можно сериализовать объект
/// json, xml, (устаревшие: soap, binary) и тп
/// Маскимально популярный сейчас - json, поскольку он текстовый и компактный
/// Сериализации подвергаются свойства объекта, выбираются публичные
/// и доступные для чтения и записи свойства. Если у свойства комплексный тип данных
/// то к такому свойству тоже применяется сериализация. По итогу сериализация
/// на выходе имеет дерево сериализованных объектов
/// Желательно (иногда обязательно), чтобы у классов был конструктор по умолчанию (т.е. без аргументов)
/// Для сериализации в Json и обратно, используется класс JsonSerializer (требует пространство имен System.Text.Json)
/// Сериализация и десериализация используются, когда нужно передать
/// объект между системами (между приложениями или устройствами)
/// Очень удобно хранить настройки в формате json
/// Для замены баз данных сериализация не очень подходит, только если данных очень мало
///
/// Для работы с форматом Xml используется класс XmlSerializer (пространство имен System.Xml.Serialization)
/// </summary>


public class Program_json
{
    public static void Main(string[] args)
    {
        Student student = new Student { Id = 1, FirstName = "Борис", Surname = "Петров", BirthDate = DateTime.Parse("05.06.07") };
        
        string BorisString = JsonSerializer.Serialize(student);
        Console.WriteLine(BorisString);

        // запись файла
        using (var fs = File.Create("boris.json"))
        {// в текстовом файле сохранится строковое представление Бориса в UTF8
            JsonSerializer.Serialize(fs, student);
        }

        Student deserializedStudent;
        // чтение файла
        using (var fs = File.OpenRead("boris.json"))
            deserializedStudent = JsonSerializer.Deserialize<Student>(fs);
        Console.WriteLine(deserializedStudent.Id);
        Console.WriteLine(deserializedStudent.FirstName);
        Console.WriteLine(deserializedStudent.Surname);
        Console.WriteLine(deserializedStudent.BirthDate);

        Group group = new Group { Id = 100, Title = "1125" };
        group.Students.Add(student);
        group.Students.Add(new Student{Id = 2, BirthDate =  DateTime.Parse("04.03.05"), FirstName = "Василиса", Surname = "Иванова"});
        
        // сериализуем иерархию из объектов
        using(var fs = File.Create("group.json"))
            JsonSerializer.Serialize(fs, group);
        Group deserializedGroup;
        using (var fs = File.OpenRead("group.json"))
            deserializedGroup = JsonSerializer.Deserialize<Group>(fs);
        
        Console.WriteLine(deserializedGroup.Id);
        Console.WriteLine(deserializedGroup.Title);
        foreach (var s in deserializedGroup.Students)
        {
            Console.WriteLine(s.Id);
            Console.WriteLine(s.FirstName);
            Console.WriteLine(s.Surname);
            Console.WriteLine(s.BirthDate);
        }
        
        // нам нужно создать экземпляр XmlSerializer нацеленного на определенный тип данных
        XmlSerializer serializeStudent = new XmlSerializer(typeof(Student)); 
        
        // запись аналогичная как у json
        using(var fs = File.Create("student.xml"))
            serializeStudent.Serialize(fs, group.Students.Last());
        
        // чтение немного отличается, метод Deserialize возвращает тип object, который нужно распаковать в требуемый тип
        using(var fs = File.OpenRead("student.xml"))
            deserializedStudent = (Student)serializeStudent.Deserialize(fs);
        
        // создание циклической связи может привести к проблемам

        Curator curator = new Curator { Id = 666, Name = "Довгань Сергей" };
        curator.Groups.Add(group);
        group.Curator = curator; // вот тут появляется цикл - группа ссылается на куратора, а куратор содержит в себе список групп

        // тут получаем ошибку при попытке сериализовать объект
        // файл curator.json будет создан, но он будет пустой
        using (var fs = File.Create("curator.json"))
            JsonSerializer.Serialize(fs, curator);
        // при сохранении группы проблема будет такой же
        //using(var fs = File.Create("group.json"))
        //    JsonSerializer.Serialize(fs, group);
        // xml тоже не смог
        //XmlSerializer serializeCurator = new XmlSerializer(typeof(Curator));
        //using (var fs = File.Create("curator.xml"))
        //    serializeCurator.Serialize(fs, curator);
        Curator deserializedCurator;
        // первое и правильное решение - сохранять объект без циклических связей
        // второе решение имеет меньший простор применения - добавить настройку
        // разрешающую циклические ссылки с помощью индексации объектов
        // третий вариант оставить циклические ссылки, но заэкранировать их в классе с помощью атрибута JsonIgnore
        // в случае третьего варианта придется восстановить циклическую ссылку вручную после десериализации
        // пример:
        //using (var fs = File.OpenRead("curator.json"))
        //    deserializedCurator = JsonSerializer.Deserialize<Curator>(fs);
        // восстановление циклической ссылки
        //foreach (var g in deserializedCurator.Groups )
        //    g.Curator = deserializedCurator;
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.ReferenceHandler = ReferenceHandler.Preserve; // вот эта опция поможет сохранить граф с циклическими связями
        using (var fs = File.Create("curator.json"))
            JsonSerializer.Serialize(fs, curator, options); // применяем опцию
        
        // чтение такого файла теперь возможно только с такой же опцией
        using (var fs = File.OpenRead("curator.json"))
            deserializedCurator = JsonSerializer.Deserialize<Curator>(fs, options); // без опции будет ошибка чтения
        Console.WriteLine(deserializedCurator.Id);
    }
}

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}

public class Group
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<Student> Students { get; set; } = new();
    [JsonIgnore]
    public Curator Curator { get; set; }
}

public class Curator
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Group> Groups { get; set; } = new();
}