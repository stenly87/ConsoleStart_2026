namespace ConsoleApp2;

public class Program
{
    public static void Main(string[] args)
    {
        //Console.WriteLine(Sum(0));
        // полезные методы для работы с массивами
        int[] array = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        int summa = array.Sum();
        bool notEmpty = array.Any(); // для списков true/false
        bool valueExist = array.Contains(1); // true/false
        int index = array.IndexOf(1);// номер ячейки со значением (или -1)
        int indexLast = array.LastIndexOf(1); // тоже самое, но поиск с конца
        int min = array.Min();// минимальное значение в массиве
        int max = array.Max();// максимальное значение в массиве
        Array.Sort(array); // отсортировать массив
        
        // Список List
        // List это коллекция данных с изменяемым размером
        // представить можно также как одномерный массив
        // List<Тип> переменная = new List<Тип>();
        // Тип - тип ячеек в коллекции
        // пример списка строк
        List<string> strings = new List<string>();
        var check = strings.Any(); // false, 0 элементов
        // по умолчанию список пустой, каждый новый элемент
        // добавляется командой Add или AddRange
        // элементы идут в конец списка
        strings.Add("перезапиши меня полностью");
        // перезапись и чтение также как в массивах
        strings[0] = "новое значение";
        var str = strings[0];
        Console.WriteLine(str);
        // можно передать массив или другой список в AddRange
        // эти значения добавятся в конец списка
        strings.AddRange(["1", "2", "3"]);
        // удаление элемента происходит через метод Remove
        strings.Remove("1"); // поиск первого попавшегося элемента и его удаление
        // при удалении индексы обновляются (происходит сдвиг элементов относительно их индексов, ну или наоборот, главное что было понятно)
        // под капотом у List находится обычный массив
        // мы можем задать начальную емкость этого массива указав число элементов при создании коллекции
        // когда внутренний массив будет заполнен полностью, он заменится массивом с размером в 2 раза больше прежнего
        // следует учитывать, что начальное заполнение коллекции
        // может вызвать множественное пересоздание внутреннего массива
        // т.е. часто следует задавать начальную емкость
        
        // 11.11 массивами
        int[] array11 = new int[25];
        for (int i = 0; i < 25; i++)
            array11[i] = i + 1;
        int[] array12 = new int[27];
        array11.CopyTo(array12, 0);
        array11 = array12;
        array11[^2] = 100;
        array11[^1] = 200;
        foreach (var i in array11)
            Console.WriteLine(i);
        // 11.11 через List
        Console.WriteLine(new string('-', 30));
        List<int> list = new();
        for (int i = 1; i <= 25; i++)
            list.Add(i);
        list.Add(100);
        list.Add(200);
        foreach (var i in list)
            Console.WriteLine(i);

        // создание коллекции из массива
        list = array11.ToList();
    }
    /*
    static int Sum(int a)
    {
        if (a >= 10)
            return a;
        return a + Sum(a + 1);
    }*/
}