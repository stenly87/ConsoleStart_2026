/*
 int.TryParse(Console.ReadLine(), out int a);
int.TryParse(Console.ReadLine(), out int b);
for (int i = a; i < b; i++) 
    Console.WriteLine(i);
*/
/*
// можно так
for(double a = 10, b = 10.4;
    a <= 25;
    a++, b++)
    Console.WriteLine($"{a} {b}");
// можно так
for(double a = 10;
        a <= 25;
        a ++)
        Console.WriteLine($"{a} {a + 0.4}");
*/
/*
double cost = 20.4;
for(int i = 2; i <= 20; i++)
    Console.WriteLine($"{i}шт. = {i*cost}руб.");
    */
/*
double p, p0 = 1.29, z = 1.25 * Math.Pow(10, -4);
for (int h = 0; h <= 1000; h += 100)
{
    p = p0 * Math.Pow(Math.E, -h * z);
    Console.WriteLine($"h = {h} p = {p}");
}
*/
//«Странный муж» ☺
/*
Console.WriteLine("Сколько раз он поменял решение???");
int.TryParse(Console.ReadLine(), out int n);
double totalPath = 0;
double path = 0;
for (double i = 1; i <= n; i++)
{
    totalPath += 1 / i;
    path += Math.Pow(-1, i) *  (1 / i);
}
Console.WriteLine($"Расстояние от дома: {Math.Round(Math.Abs(path), 2)}км");
Console.WriteLine($"Пройденный путь: {Math.Round(totalPath, 2)}км");
*/
// фибоначчи
/*
int.TryParse(Console.ReadLine(), out int n);
int f1 = 0, f2 = 1;
for (int i = 1; i <= n; i++)
{
    Console.WriteLine(f2);
    int temp = f2;
    f2 += f1;
    f1 = temp;
}
Console.WriteLine($"{n}-ый член фибоначчи = {f1}");
*/
// амебы (1шт делится каждый 3 часа на 2шт)
/*
int count = 1;
Random random = new Random();
for (int i = 1; i <= 8; i++)
{
    count *= 2;
    if (random.NextDouble() > 0.7)
    {
        count = (int)Math.Round(count / 3f, 5);
        if (count < 0)
        {
            Console.WriteLine("Все амебы сдохли");
            break;
        }

        Console.WriteLine($"Произошло заболевание. Часть амеб сдохло. Осталось {count}шт.");
    }

    if (random.NextDouble() > 0.7)
    {
        count = 0;
        Console.WriteLine("Пришел пьяный лаборант и смыл амеб в унитаз");
        break;
    }

    if (random.NextDouble() > 0.5)
    {
        Console.WriteLine("Произошли выборы. Кол-во амеб увеличилось на 146%");
        count = (int)((count / 100f) * 146);
    }

    Console.WriteLine($"спустя {i*3} часа/ов амеб стало {count}");
}
*/