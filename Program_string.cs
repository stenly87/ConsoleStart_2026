// строки в c#
// тип string - ссылочный
/*
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
/*
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
*/


// 12.2

/*Console.Write("Введите государство:");
string country = Console.ReadLine();
Console.Write("Введите столицу:");
string town = Console.ReadLine();
string result = $"Столица государства {country}  – город {town}";
Console.WriteLine(result);*/

// так тоже можно, но нет ссылки на результат и не оттеняется ввод данных
//Console.WriteLine($"Столица государства {Console.ReadLine()}  – город {Console.ReadLine()}");

//12.5
/*Console.Write("Введите название футбольного клуба:");
string club = Console.ReadLine();
int result = club.Length;
Console.WriteLine($"Кол-во символов: {result}");*/

//12.9
/*
Console.Write("Введите страну 1:");
string country1 = Console.ReadLine();
Console.Write("Введите страну 2:");
string country2 = Console.ReadLine();
// обмен значениями между двух переменных требует третью
string temp = country1;
country1 = country2;
country2 = temp;
Console.WriteLine($"country1 = {country1}, country2 = {country2}");
*/

//12.12
/*Console.Write("Введите слово:");
string word = Console.ReadLine();
Console.WriteLine(word[^1]);// последний символ*/

//12.17
/*Console.Write("Введите слово:");
string word = Console.ReadLine();
//string result = word[1] + "" + word[3]; // можно так или сяк
string result = word[1].ToString() + word[3];
Console.WriteLine(result);*/

//12.20
/*Console.Write("Введите слово четной длины:");
string word = Console.ReadLine();
string result = word.Substring(0, word.Length / 2);
Console.WriteLine(result);*/

//12.21
/*Console.Write("Введите слово:");
string word = Console.ReadLine();
Console.Write("Введите m:");
int.TryParse(Console.ReadLine(), out int m);
Console.Write("Введите n:");
int.TryParse(Console.ReadLine(), out int n);
string result = word.Substring(m, n - m + 1);
Console.WriteLine(result);*/

//12.25
/*string str = "программа";
string result = str.
     Insert(3, str[str.IndexOf('м')].ToString())
    .Substring(1, 3);
Console.WriteLine(result);
result = str.Insert(str.LastIndexOf('м'), str[0].ToString());
result = result.Remove(result.LastIndexOf('м'), 1)
    .Substring(4);
Console.WriteLine(result);*/

//12.29
/*string str = "вирус";
string result = str.Replace("вир", "фок");
Console.WriteLine(result);*/

//12.79
//Console.WriteLine("Введите несколько слов через пробел");
//string str = Console.ReadLine();
/*
string temp = string.Empty;
int count = 6;
for (int i = 0; i < str.Length && count > 0; i++)
{
    if (str[i] == ' ')
    {
        Console.WriteLine(temp);
        temp = string.Empty;
        count--;
    }
    else
        temp += str[i];
}*/
// проще и компактнее с точки кол-ва созданных строк в памяти
/*string[] words = str.Split();
for (int i = 0; i < words.Length && i < 6; i++)
{
    Console.WriteLine(words[i]);
}*/

//12.103
/*Console.WriteLine("Введите предложение");
string str = Console.ReadLine();
string result = str.Replace("да", "не");
Console.WriteLine(result);*/

//12.115

/*
using System.Text;

Console.WriteLine("Введите предложение");
string str = Console.ReadLine();
int indexО = str.LastIndexOf("о");
int indexA = str.IndexOf("а");
if (indexО == -1 || indexA == -1)
{
    Console.WriteLine("Невозможно поменять о и а. Кого-то из них нет");
    return;
}
// так нельзя
//str[indexA] = str[indexО];
// можно проинициализировать StringBuilder строкой str
StringBuilder builder = new StringBuilder(str);
builder[indexО] = 'a';
builder[indexA] = 'о';
str = builder.ToString();
Console.WriteLine(str);*/

//12.125
/*
Console.WriteLine("Введите предложение");
string str = Console.ReadLine();//АРГЕНТИНА МАНИТ НЕГРА
str = str.Replace(" ", ""); // АРГЕНТИНАМАНИТНЕГРА

string str2 = new string(str.Reverse().ToArray());
Console.WriteLine(str2);
Console.WriteLine(str == str2);

// создание строки возможно из массива символов
string s = new string(['a', 'b', 'c', 'd', 'e', 'f']);
s = new string('c', 10);// cccccccccc


bool result = true;
for (int i = 0, j = str.Length - 1; i < str.Length; i++, j--)
{
    if (str[i] != str[j])
    {
        result = false;
        break;
    }
}
Console.WriteLine(result);
*/