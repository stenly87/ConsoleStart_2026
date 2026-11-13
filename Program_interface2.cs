namespace ConsoleApp2;

public class Document
{
    public string Title { get; set; }
    public string Content { get; set; }

    public Document(string title, string content)
    {
        Title = title;
    }

    // интерфейс позволяет переложить ответственность за выполнение
    // определенных действий на другие объекты
    public void Save(IDocumentSaver saver)
    {
        saver.Save(this);
    }
}

// создаем интерфейс для перекладывания ответственности за сохранение документа на других
public interface IDocumentSaver
{
    void Save(Document document);
}

// реализация сохранения в Docx выносится в отдельный класс
public class DocxSaver : IDocumentSaver
{
    public void Save(Document document)
    {
        Console.WriteLine($"Saving {document.Title} to docx");
    }
}

// реализация сохранения в Pdf выносится в отдельный класс
public class PdfSaver : IDocumentSaver
{
    public void Save(Document document)
    {
        Console.WriteLine($"Saving {document.Title} to pdf");
    }
}

public class Program_interface2
{
    public static void Main()
    {
        // разные реализации интерфейса позволяют сохранить документ
        // в разные форматы, при этом нам не нужно редактировать класс документа
        // мы только добавляем новые классы
        /*DocxSaver saver = new DocxSaver();
        PdfSaver pdfSaver = new PdfSaver();
        Document doc = new Document("Заголовок", "Содержимое");
        // вызов сохранения через интерфейс из класса Document
        doc.Save(saver);
        doc.Save(pdfSaver);

        // прямой вызов сохранения также возможен
        saver.Save(doc);*/

        BattleRoom battleRoom = new BattleRoom();
        battleRoom.AddLeftSide(new RKN());
        battleRoom.AddRightSide(new IlonMask());
        battleRoom.AddRightSide(new IlonMask());
        battleRoom.AddRightSide(new IlonMask());
        battleRoom.PrintInfo();
        battleRoom.PlayRound();
    }
}

public interface IMonster
{
    string Name { get; }
    int HP { get; }
    int MaxHP { get; }
    void Attack(IMonster currentTarget);
    void TakeDamage(int damage);
}

public class RKN : IMonster
{
    public string Name { get; } = "Рос ком NADZOR";
    public int HP { get; } = 100;
    public int MaxHP { get; } = 100;
    public void Attack(IMonster currentTarget)
    {
        Console.WriteLine($"{this.Name} банит {currentTarget.Name}!");
        currentTarget.TakeDamage(404);
    }

    public void TakeDamage(int damage)
    {
        Console.WriteLine($"{Name} игнорирует урон в размере {damage}!");
    }
}

public class IlonMask : IMonster
{
    public string Name { get; } = "Илон";
    public int HP { get; private set; } = 1000;
    public int MaxHP { get; } = 1000;
    public void Attack(IMonster currentTarget)
    {
        Console.WriteLine($"{Name} закидывает деньгами {currentTarget.Name}!");
        currentTarget.TakeDamage(100);
    }

    public void TakeDamage(int damage)
    {
        Console.WriteLine($"{Name} страдает и обижается");
        HP -= damage;
    }
}

public class BattleRoom
{
    private List<IMonster> leftSide = new();
    private List<IMonster> rightSide = new();
    public void AddLeftSide(IMonster monster) => leftSide.Add(monster);
    public void AddRightSide(IMonster monster) => rightSide.Add(monster);

    public void PrintInfo()
    {
        Console.WriteLine("С левой стороны ринга:");
        if (!leftSide.Any())
            Console.WriteLine("Никого нет!");
        else
            foreach (var monster in leftSide)
                Console.WriteLine($"{monster.Name}! Здоровье: {monster.HP}/{monster.MaxHP}");
        Console.WriteLine("С правой стороны ринга:");
        if (!rightSide.Any())
            Console.WriteLine("Никого нет!");
        else
            foreach (var monster in rightSide)
                Console.WriteLine($"{monster.Name}! Здоровье: {monster.HP}/{monster.MaxHP}");
    }

    Random random = new();

    public void PlayRound()
    {
        int countStep = random.Next(5, 10);
        for (int i = 0; i < countStep; i++)
        {
            Console.WriteLine("А вот и новая атака!");
            IMonster currentAttacker = random.Next(0, 2) == 0 ? GetRandomMonster(leftSide) : GetRandomMonster(rightSide);
            IMonster currentTarget = random.Next(0, 2) == 0 ? GetRandomMonster(leftSide) : GetRandomMonster(rightSide);
            
            currentAttacker.Attack(currentTarget);
            Console.WriteLine("Вот это удар!");
        }
    }

    IMonster GetRandomMonster(List<IMonster> monsters)
    {
        int index = random.Next(0, monsters.Count);
        return monsters[index];
    }
}