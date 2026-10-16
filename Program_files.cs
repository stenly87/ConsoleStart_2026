// работа с файлами
/*
using System.IO;
using System.Text; // Input Output
*/
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

// чтение и запись файлов
// существует 2 подхода к чтению и записи
// 1. чтение или запись целиком - либо получаем один набор байтов при чтении или сохраняем набор байтов как файл
// 2. потоковое чтение или запись

// для чтения/записи целиком есть набор методов в классе File:
/*
byte[] array = File.ReadAllBytes(@"C:\1\2\1.txt");
foreach (var item in array)
    Console.Write(item.ToString() + ' ') ;
Console.WriteLine(Encoding.UTF8.GetString(array));*/
/*
string[] lines = File.ReadAllLines(@"C:\1\2\1.txt");
foreach (var line in lines)
    Console.WriteLine(line);
string text = File.ReadAllText(@"C:\1\2\1.txt");
Console.WriteLine(text);
*/
// методы Write перезаписывают файлы целиком!
//File.WriteAllBytes("путь", массив байт);
//File.WriteAllLines("путь", массив строк);
//File.WriteAllText(@"C:\1\2\2.txt", "те\nкст\r");
// методы, производящие дозапись (запись в конец файла) если файла нет, он будет создан
//File.AppendAllBytes("путь", массив байт);
//File.AppendAllLines("путь", массив строк);
//File.AppendAllText(@"C:\1\2\3.txt", "те\r\nкст");

// проверки, существует ли директория или файл
/*bool result = Directory.Exists("путь");
result = File.Exists("путь к файлу");
Console.WriteLine(result);*/

// Потоковая работа с файлами
/*
string someString = "какая-то строка\n";
byte[] array = Encoding.UTF8.GetBytes(someString);/
/*FileStream fs = File.Open("test.txt", FileMode.Create, FileAccess.Write);
fs.Write(array, 0, array.Length); // записать в файл массив array, начиная с 0 индекса в кол-ве: все байты array
fs.Write(array, 0, array.Length); 
fs.Close();
fs.Dispose();*/
//тоже самое, но без лишних строк про Close и Dispose:
//по окончанию тела using происходит закрытие файла и освобождение памяти
/*using (FileStream fs = File.Open("test.txt", FileMode.Create, FileAccess.Write))
{
    fs.Write(array, 0, array.Length); // записать в файл массив array, начиная с 0 индекса в кол-ве: все байты array
    fs.Write(array, 0, array.Length);
}

bool test = true;
byte[] array2 = BitConverter.GetBytes(test);
// array2 становится массивом размером в 4 байта, потому что число 121332132 - int, размер 4 байта 
array2 = BitConverter.GetBytes(121332132); // int32
int getValue = BitConverter.ToInt32(array2, 0);*/
// записать 100 чисел подряд
/*using (FileStream fs = File.Open("test1.bin", FileMode.Create, FileAccess.Write))
{
    for (int i = 0; i < 100; i++)
    {
        array2 = BitConverter.GetBytes(i);// array2 опять становится массивом байт размером 4 байта, но с другим числом
        fs.Write(array2, 0, array2.Length); // записываем 4 байта
    }
}*/
/*
using (FileStream fs = File.Open("test1.bin", FileMode.Open, FileAccess.Read))
{
    for (int i = 0; i < 100; i++)
    {
        //fs.Read перезаписывает array2
        fs.Read(array2, 0, 4); // прочесть из потока fs 4 байта в массив array, записываем их с 0 индекса
        // BitConverter получает int из 4 байт в массиве array2
        Console.WriteLine(BitConverter.ToInt32(array2, 0));
    }
}*/


// оаип, 91 стр, 1 задание
/*
Random rng = new ();
int value;
using (var fs = File.OpenWrite("f"))
using (var bw = new BinaryWriter(fs))
    for (int i = 0; i < 100; i++)
        bw.Write(rng.Next(1, 1001));

using (var fs = File.OpenRead("f"))
using (var gs = File.OpenWrite("g"))
using (var br = new BinaryReader(fs))
using (var bw = new BinaryWriter(gs))
    for (int i = 0; i < 100; i++)
    {
        value = br.ReadInt32();
        if (value % 2 == 0)
            bw.Write(value);
    }

using (var gs = File.OpenRead("g")) 
using (var br = new BinaryReader(gs))
    while(gs.Position < gs.Length)
        Console.WriteLine(br.ReadInt32());


// внутри файла мы можем быстро сместиться на нужную позицию
// без чтения лишних байт
using (var gs = File.OpenRead("g"))
{
    gs.Seek(20, SeekOrigin.Begin); // сдвинуться на 20 байт относительно старта файла
    gs.Seek(0, SeekOrigin.Begin); // свинуться на начало
    gs.Seek(0, SeekOrigin.End); // сдвинуться в конец файла
    gs.Seek(-20, SeekOrigin.Current); // сдвинуться на 20 байт влево от текущей позиции
}
*/

// стр 91, 4 задание
/*
int count = 0, leftValue = 0;
Random rnd = new ();
Console.WriteLine("кол-во чисел?");
int n = int.Parse(Console.ReadLine());
using (var fs = File.Create("f"))
using (var bw = new BinaryWriter(fs))
    for (int i = 0; i < n; i++)
    {
        leftValue = rnd.Next(-5, 6);
        bw.Write(leftValue); // 4 байта потому что rnd.Next -> int
        Console.WriteLine(leftValue);
    }
Console.WriteLine(new string('-', 30));

List<long> skipPositions = new();
long lastPosition = 0;
using (var fs = File.Open("f", FileMode.Open, FileAccess.ReadWrite))
using (var br = new BinaryReader(fs))
using (var bw = new BinaryWriter(fs))
    for (int i = 0; i < n; i++) // читаем числа для левой части сравнения
    {
        leftValue  = br.ReadInt32(); // 4 байта, int
        if (leftValue == 0)
            continue;
        
        lastPosition = fs.Position; // запомнили позицию, с которой начнется чтение следующего числа для левой части
        // в случае с доп массивом нам не нужно изменять файл
        if (skipPositions.Contains(lastPosition))
            continue;
        while (fs.Position < fs.Length) // чтение до конца файла
        {
            if (br.ReadInt32() == -leftValue)
            {
                if (skipPositions.Contains(fs.Position))
                    continue;
                count++;
                // для большей точности мы перезаписали файл
                // в некоторых случаях так нельзя делать
                //fs.Seek(-4, SeekOrigin.Current);
                //bw.Write(0);
                // вместо перезаписи можно запомнить позицию, проверять ее в двух местах
                skipPositions.Add(fs.Position);
                Console.WriteLine($"Найдена пара для числа {leftValue}");
                break;
            }
        }
        // возвращаемся в запомненную ранее позицию
        // для правильного выполнения цикла for
        fs.Seek(lastPosition, SeekOrigin.Begin);
    }
Console.WriteLine($"Кол-во противоположных пар чисел: {count}");
*/

// стр 91, 5 задание
/*
Random rnd = new ();
Console.WriteLine("кол-во чисел?");
int n = int.Parse(Console.ReadLine());
using (var fs = File.Create("f"))
using (var bw = new BinaryWriter(fs))
    for (int i = 0; i < n; i++)
        bw.Write(rnd.Next(1, 10)); // 4 байта потому что rnd.Next -> int
int value = 0;
bool exist;
using (var fs = File.OpenRead("f"))
using (var gs = File.Open("g", FileMode.Create, FileAccess.ReadWrite))
using (var br = new BinaryReader(fs))
using (var brG = new BinaryReader(gs))
using (var bwG = new BinaryWriter(gs))
{
    for (int i = 0; i < n; i++)
    {
        value = br.ReadInt32();
        // переходим в начало файла g, чтобы прочитать его целиком и сравнить все значения с value
        gs.Seek(0, SeekOrigin.Begin);
        exist = false;
        while (gs.Position < gs.Length)
        {
            if (brG.ReadInt32() == value)
            {
                exist = true;
                break;
            }
        }
        // exist - показатель - выполнится или нет условие в цикле
        if (!exist) // если условие не выполнилось, дубликатов нет
        {
            bwG.Write(value); // записываем в конец файла новое число
            Console.WriteLine(value);
        }
    }
}*/

// стр 91, 12
/*
int value;
Random rnd = new ();
Console.WriteLine("кол-во чисел?");
int n = int.Parse(Console.ReadLine());
Console.WriteLine("Записаны числа:");
using (var fs = File.Create("f"))
using (var bw = new BinaryWriter(fs))
    for (int i = 0; i < n; i++)
    {
        value = rnd.Next(-100, 100);
        bw.Write(value);
        Console.WriteLine(value);
    }
Console.WriteLine("Поиск наибольшего числа...");
int max = Int32.MinValue;
using (var fs = File.OpenRead("f"))
using (var br = new BinaryReader(fs))
    while (fs.Position < fs.Length)
    {
        value = br.ReadInt32();
        if (value > max)
            max = value;
    }
Console.WriteLine(max);
*/

// кроме filestream мы можем пользоваться текстовыми стримами:
/*
using (StreamWriter sw = new StreamWriter("f"))
{
    sw.WriteLine("строка заканчивается переходом на новую строку");
    string s = "строка без перехода на новую строчку";
    sw.Write(s.Length);
    sw.Write(s);
    sw.Write(100);
    sw.Write((long)1000);
}

using(StreamReader sr = new StreamReader("f"))
{
    string line = sr.ReadLine(); // прочесть строку
    Console.WriteLine(line);
    line = sr.ReadLine(); // прочесть строку
    Console.WriteLine(line);
}*/

