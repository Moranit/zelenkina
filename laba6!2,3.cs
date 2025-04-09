using System;

public class LineSegment
{
    private double _x1;
    private double _x2;


    public LineSegment(double x1, double x2)
    {
        _x1 = x1;
        _x2 = x2;
        Normalize();
    }


    public LineSegment(LineSegment other)
    {
        _x1 = other._x1;
        _x2 = other._x2;
    }

    public double X1 => _x1;
    public double X2 => _x2;

    // Упорядочивание координат (чтобы x1 всегда было меньше x2)
    private void Normalize()
    {
        if (_x1 > _x2)
        {
            double temp = _x1;
            _x1 = _x2;
            _x2 = temp;
        }
    }

    //для второго задания

    public bool Contains(double number)
    {



        return number >= _x1 && number <= _x2;


    }




    public override string ToString()
    {
        return $"[{_x1}, {_x2}]";
    }

    // для третьего задания


    // Унарный оператор ! - длина отрезка
    public static double operator !(LineSegment segment)
    {
        return segment._x2 - segment._x1;
    }

    // Унарный оператор ++ - увеличение координат на 1
    public static LineSegment operator ++(LineSegment segment)
    {
        return new LineSegment(segment._x1 + 1, segment._x2 + 1);
    }

    // Явное приведение к int (целая часть x1)



    public static explicit operator int(LineSegment segment)
    {
        return (int)segment._x1;
    }

    // Неявное приведение к double (значение y)
    public static implicit operator double(LineSegment segment)
    {
        return segment._x2;
    }

    // Бинарный оператор + добавление числа к координатам
    public static LineSegment operator +(LineSegment segment, int d)
    {

        return new LineSegment(segment._x1 + d, segment._x2 + d);
    }

    public static LineSegment operator +(int d, LineSegment segment)
    {

        return segment + d;
    }

    // Бинарный оператор < проверка попадания числа в отрезок



    public static bool operator <(LineSegment segment, int number)
    {
        return segment.Contains(number);
    }

    public static bool operator >(LineSegment segment, int number)
    {

        return !(segment < number);
    }
}

class Program
{
    static void Main(string[] args)
    {

        TestLineSegmentBasic();


        TestLineSegmentOperators();
    }

    static void TestLineSegmentBasic()
    {
        Console.WriteLine("Координаты отрезка");

        try
        {
            Console.WriteLine("Введите координаты отрезка:");

            double x1 = GetDoubleInput("x1: ");

            double x2 = GetDoubleInput("x2: ");

            LineSegment segment = new LineSegment(x1, x2);
            Console.WriteLine($"Создан отрезок: {segment}");

            double number = GetDoubleInput("Введите число для проверки: ");


            bool contains = segment.Contains(number);


            Console.WriteLine($"Отрезок {(contains ? "содержит" : "не содержит")} число {number}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    static void TestLineSegmentOperators()
    {
        Console.WriteLine("\n перегруженные операторы ");

        try
        {
            Console.WriteLine("Введите координаты отрезка:");
            double x1 = GetDoubleInput("x1: ");

            double x2 = GetDoubleInput("x2: ");

            LineSegment segment = new LineSegment(x1, x2);
            Console.WriteLine($"Исходный отрезок: {segment}");


            // Тест унарных операторов
            Console.WriteLine($"\nДлина отрезка (!сегмент): {!segment}");
            Console.WriteLine($"Отрезок после ++сегмент: {++segment}");


            // Тест операторов приведения
            Console.WriteLine($"\nЯвное приведение к int: {(int)segment}");
            Console.WriteLine($"Неявное приведение к double: {(double)segment}");




            // Тест бинарных операторов
            int d = GetIntInput("\nВведите число для добавления к координатам: ");
            Console.WriteLine($"сегмент + {d}: {segment + d}");
            Console.WriteLine($"{d} + сегмент: {d + segment}");




            int num = GetIntInput("Введите число для  <: ");

            Console.WriteLine($"сегмент < {num}: {segment < num}");
            Console.WriteLine($"сегмент > {num}: {segment > num}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static double GetDoubleInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            if (double.TryParse(Console.ReadLine(), out double result))
                return result;
            Console.WriteLine(" введите число.");
        }
    }

    static int GetIntInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);


            if (int.TryParse(Console.ReadLine(), out int result))
                return result;
            Console.WriteLine(" введите целое число.");
        }
    }
}