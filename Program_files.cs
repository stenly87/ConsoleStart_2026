// работа с файлами

using System.IO; // Input Output
//DirectoryInfo dirInfo = Directory.CreateDirectory("путь к создаваемой директории");

// если путь указан не полностью (не начиная с буквы диска), 
// то это относительный путь 
// относительно рабочей папки приложения
//Directory.CreateDirectory(@"C:\1\2\3\4\5");
// при указании абсолютного пути создаются все папки,
// которые не существуют

// удалить папку 5, она должна быть пуста
//Directory.Delete(@"C:\1\2\3\4\5");

// удалить папку 5 и все её содержимое
//Directory.Delete(@"C:\1\2\3\4\5", true);

// получить список директорий по указанному пути
/*
string[] dirs = Directory.GetDirectories("C:\\");
foreach (string dir in dirs)
{
    try
    {
        string[] subdirs = Directory.GetDirectories(dir);
        foreach (string subdir in subdirs)
        {
            Console.WriteLine(subdir);
        }
    }
    catch
    {
    }
}*/

//синтаксис try catch
// try catch нужен для перехвата исключений - необрабатываемых
// ошибок в выполнении методов
/*
try
{
    // сюда записывается код, который
    // может вызвать исключения
    //int t = int.Parse(Console.ReadLine());
    // ошибки внутри метода тоже будут перехватываться
    SomeMethod(1);
}
// catch это блок обработки ошибок указанного типа
// блоков catch может быть несколько
// в аргументе после catch указывается тип перехватываемого
// исключения. Если указать Exception - будут перехватываться все ошибки
catch (FormatException e)
{
    // этот блок будет перехватывать только ошибки FormatException
    Console.WriteLine("Напиши число, дурачок");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    //Console.WriteLine(e.StackTrace);
}

double SomeMethod(int i)
{
    double result = 0;
    Parallel.For(1, 1000, t =>
        result = Math.Pow(t, 100 + i));
    return result + SomeMethod(++i);
}
*/

// получить список файлов
/*
string[] files = Directory.GetFiles(@"C:\Windows", "*.exe");
foreach (string file in files)
{
    // класс FileInfo, он инициализируется через путь к файлу
    FileInfo fi = new FileInfo(file);
    Console.WriteLine(fi.Name);
    Console.WriteLine(fi.Extension); // расширение
    Console.WriteLine(fi.Length); // кол-во байт
    
    Console.WriteLine(fi.CreationTime);
    Console.WriteLine(fi.LastWriteTime);
    Console.WriteLine(fi.LastAccessTime);
    Console.WriteLine(new string('-', 30));
}
// в методах GetFiles и GetDirectories можно указывать
// второй аргумент для фильтрации (часть имени файла
// или директории. Можно использовать спецсимволы, например
// * - любое кол-во символов, ? - любой один символ и тп
*/

