using System.Text.Json;

namespace ConsoleApp2;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Group { get; set; }
    public bool Gender { get; set; }
    public DateTime Birthday { get; set; }
}

public class Exercise4
{
    public static void Main()
    {
        string command;
        do
        {
            Console.WriteLine("Введите команду");
            command = Console.ReadLine();
            switch (command)
            {
                case "add":
                    AddStudent();
                    break;
                case "all":
                    ShowAllStudents();
                    break;
                case "find":
                    FindStudent();
                    break;
                case "update":
                    UpdateStudent();
                    break;
                case "delete":
                    DeleteStudent();
                    break;
            }
        } while (command != "exit");
    }

    private static void DeleteStudent()
    {
        if (File.Exists("students.db") && new FileInfo("students.db").Length > 0)
        {
            Console.WriteLine("Напиши id для удаления?");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                List<Student> students;
                Student find;
                int autoincrement = 0;
                using (var fs = File.Open("students.db", FileMode.Open, FileAccess.ReadWrite))
                using (var br = new BinaryReader(fs))
                using (var bw = new BinaryWriter(fs))
                {
                    autoincrement = br.ReadInt32();
                    students = JsonSerializer.Deserialize<List<Student>>(fs);
                    find = students.FirstOrDefault(s => s.Id == id);
                }

                if (find != null)
                {
                    students.Remove(find);
                    using (var fs = File.Create("students.db"))
                    using (var bw = new BinaryWriter(fs))
                    {
                        bw.Write(autoincrement);
                        JsonSerializer.Serialize(fs, students);
                    }
                }
                else
                {
                    Console.WriteLine("Студент с таким id не найден");
                }
            }
        }
        else
        {
            Console.WriteLine("Не индекс");
        }
    }


    private static void UpdateStudent()
    {
        if (File.Exists("students.db") && new FileInfo("students.db").Length > 0)
        {
            Console.WriteLine("Кого ищем?");
            string input = Console.ReadLine();
            using (var fs = File.Open("students.db", FileMode.Open, FileAccess.ReadWrite))
            using (var br = new BinaryReader(fs))
            using (var bw = new BinaryWriter(fs))
            {
                int autoincrement = br.ReadInt32();
                var students = JsonSerializer.Deserialize<List<Student>>(fs);
                fs.Seek(0, SeekOrigin.Begin);
                var find = students.FirstOrDefault(s => s.FirstName.Contains(input) || s.LastName.Contains(input));
                if (find == null)
                    Console.WriteLine("Таких нет");
                else
                {
                    Console.WriteLine(find.Id + " "
                                              + find.FirstName + " "
                                              + find.LastName + " "
                                              + find.Group + " " +
                                              find.Gender + " " +
                                              find.Birthday.ToShortDateString());
                    Console.WriteLine("Укажите новые данные!");
                    if (find.Id == 0)
                        find.Id = autoincrement++;
                    Console.WriteLine("Имя?");
                    find.FirstName = Console.ReadLine();
                    Console.WriteLine("Фамилия?");
                    find.LastName = Console.ReadLine();
                    Console.WriteLine("Группа?");
                    find.Group = Console.ReadLine();
                    Console.WriteLine("Мальчик? y/n");
                    find.Gender = Console.ReadLine() == "y";
                    Console.WriteLine("Дата рождения?");
                    find.Birthday = DateTime.Parse(Console.ReadLine());
                    bw.Write(autoincrement);
                    JsonSerializer.Serialize(fs, students);
                }
            }
        }
        else
        {
            Console.WriteLine("В списке никого нет");
        }
    }

    private static void FindStudent()
    {
        if (File.Exists("students.db") && new FileInfo("students.db").Length > 0)
        {
            Console.WriteLine("Кого ищем?");
            string input = Console.ReadLine();
            using (var fs = File.OpenRead("students.db"))
            {
                fs.Seek(4, SeekOrigin.Begin);
                var students = JsonSerializer.Deserialize<List<Student>>(fs);
                var find = students.FirstOrDefault(s => s.FirstName.Contains(input) || s.LastName.Contains(input));
                if (find == null)
                    Console.WriteLine("Таких нет");
                else
                    Console.WriteLine(find.Id + " "
                                              + find.FirstName + " "
                                              + find.LastName + " "
                                              + find.Group + " " +
                                              find.Gender + " " +
                                              find.Birthday.ToShortDateString());
            }
        }
        else
        {
            Console.WriteLine("В списке никого нет");
        }
    }

    private static void ShowAllStudents()
    {
        if (File.Exists("students.db") && new FileInfo("students.db").Length > 0)
        {
            using (var fs = File.OpenRead("students.db"))
            {
                fs.Seek(4, SeekOrigin.Begin);
                var students = JsonSerializer.Deserialize<List<Student>>(fs);
                foreach (var student in students)
                    Console.WriteLine(student.Id + " " + student.FirstName + " "
                                      + student.LastName + " "
                                      + student.Group + " "
                                      + student.Gender + " "
                                      + student.Birthday.ToShortDateString());
            }
        }
        else
        {
            Console.WriteLine("В списке никого нет");
        }
    }

    private static void AddStudent()
    {
        Student student = new Student();
        Console.WriteLine("Имя?");
        student.FirstName = Console.ReadLine();
        Console.WriteLine("Фамилия?");
        student.LastName = Console.ReadLine();
        Console.WriteLine("Группа?");
        student.Group = Console.ReadLine();
        Console.WriteLine("Мальчик? y/n");
        student.Gender = Console.ReadLine() == "y";
        Console.WriteLine("Дата рождения?");
        student.Birthday = DateTime.Parse(Console.ReadLine());

        int autoincrement = 1;
        using (var fs = File.Open("students.db", FileMode.OpenOrCreate))
        using (var br = new BinaryReader(fs))
        using (var bw = new BinaryWriter(fs))
        {
            List<Student> students;
            if (fs.Length == 0)
                students = new();
            else
            {
                autoincrement = br.ReadInt32();
                students = JsonSerializer.Deserialize<List<Student>>(fs);
                fs.Seek(0, SeekOrigin.Begin);
            }

            student.Id = autoincrement++;
            students.Add(student);

            bw.Write(autoincrement);
            JsonSerializer.Serialize(fs, students);
        }
    }
}