// строки в c#
// тип string - ссылочный

using System.Text;

string someThing = "Hello World";
Console.WriteLine(someThing[2]); // получение символа по индексу
// строки являются неизменяемыми объектами
string anotherThing = someThing; // копирование ссылки
anotherThing += "!!!!";// при каких-либо операциях со
                       // строками создается новая строка
Console.WriteLine("anotherThing " + anotherThing);
Console.WriteLine("someThing " + someThing);
// за счет иммутабельности (неизменность) строки хранятся
// в единственном экземпляре, при создании происходит проверка
// на существование, если строка уже существует мы просто
// получим ссылку на нее. Случайно сломать строку с помощью
// второй ссылки нельзя.
//someThing[4] = ' '; // не скомпилируется
/*
foreach (int ch in someThing)
{
    Console.Write(ch);
    Console.Write(' ');
}
Console.WriteLine();
foreach (char ch in someThing)
{
    Console.Write(ch);
    Console.Write(' ');
}
*/
/*
Console.WriteLine("utf8");
int index = 65;
for (; index < 123; )
{
    for (int i = 0; i < 15; i++)
    {
        Console.Write($"{(char)index}:{index} ");
        index++;
    }
    Console.WriteLine();
}
Console.WriteLine();
index = 1020;
for (; index < 1200; )
{
    for (int i = 0; i < 15; i++)
    {
        Console.Write($"{(char)index}:{index} ");
        index++;
    }
    Console.WriteLine();
}
*/

int length = someThing.Length; // длина строки
//строка не ограничена по размерам программно, только технически
//максимальный размер строки зависит от ОЗУ компьютера
//удалением строк из памяти занимается сборщик мусора
//удаление происходит, если ни одна ссылка не ссылается на значение строки
// код типа нижнего создаст много промежуточных строк в памяти
string str = 0 + "1" + "2" + "3" + "4" + "5" + "6" + 7;
// такое происходит в подобных циклах:
for(int i = 0 ; i < 10; i++)
    str += i;
Console.WriteLine(str);
// для решения такой проблемы есть специальный тип:
StringBuilder sb = new StringBuilder();
sb.Append("Hello World").Append(123).Append(true).Append(1.2);
str = sb.ToString(); // получение итоговой строки
Console.WriteLine(str);

// сравнение строк
string a = "a", b = "a";
// сравнение на равенство и неравенство происходит по ссылке
if (a != b) // лишнее напоминание, что строки хранятся в единственном экземпляре
{
    Console.WriteLine("Строки не равны");
}
else
{
    Console.WriteLine("Строки равны");
}
Console.WriteLine(a.Equals(b)); // то же самое, что a == b
Console.WriteLine(a.CompareTo(b));// 0 в случае равенства, 1 в случае если a > (раньше) b, -1 в случае если a < b

// методы для работы со строками
str = $"{a} какой-то текст {b}"; // интернирование строки
bool check = str.Contains('a'); // содержит ли символ 
check = str.Contains(a+b);//или подстроку
// c# регистрозависимый язык, так что все сравнения поиски и тп
// происходят с учетом регистра
check = str.EndsWith(".end"); //проверка завершения строки
check = str.StartsWith("start"); // начало строки
int index = str.IndexOf('a'); // возвращает индекс символа или -1, если символа в строке нет
index = str.LastIndexOf('a'); // последний индекс
str = "abcdef";
string s = str.Insert(3, "_substring_");// вставка подстроки, начиная с указанного индекса
Console.WriteLine(s);//abc_substring_def
s = str.PadLeft(30);// удлинение строки до заданного размера
Console.WriteLine(s);// добавятся пробелы слева
s = str.PadRight(30);
Console.WriteLine(s);// добавятся пробелы справа
s = str.Trim(); // обрезать пробелы слева и справа
s = str.TrimEnd();// обрезать пробелы справа
s = str.TrimStart();// обрезать пробелы слева
s = str.Trim([' ', '_']); // можно указать другой символ/ы вместо пробела
s = str.Remove(3); // удалит часть строки, начиная с 3 индекса
s = str.Remove(3, 2);// удалит 2 символа, начиная с 3 индекса
s = str.Substring(3);// получить подстроку, начиная с 3 индекса
s = str.Substring(3, 2);// получить 2 символа, начиная с 3 индекса
s = str.Replace('a', 'b'); //замена символов (а на b)
s = str.Replace("123", "345"); //замена подстрок (123 на 345)
s = str.ToUpper();// все символы заглавные
s = str.ToLower();// все символы в нижнем регистре
string[] array = str.Split();// делит строку на подстроки,
                             // разделитель - пробел
char[] splitters = ['.', ',', '!'];
array = str.Split(splitters);// деление с несколькими разделителями
// удаление пустых подстрок (длиной 0)
array = str.Split(splitters, StringSplitOptions.
                                RemoveEmptyEntries);
