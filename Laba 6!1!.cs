
using System;

public class ThreeNumbers
{
    protected double a;
    protected double b;
    protected double c;

    public ThreeNumbers(double a, double b, double c)
    {
        ValidateNonNegative(a, nameof(a));
        ValidateNonNegative(b, nameof(b));
        ValidateNonNegative(c, nameof(c));

        this.a = a;
        this.b = b;
        this.c = c;
    }

    private void ValidateNonNegative(double value, string paramName)
    {
        if (value < 0)
        {
            throw new ArgumentException($"{paramName} не может быть отрицательным", paramName);
        }
    }

    public ThreeNumbers(ThreeNumbers other)
    {
        a = other.a;
        b = other.b;
        c = other.c;
    }


    public int GetMaxLastDigit()
    {
        int lastA = Math.Abs((int)a % 10);
        int lastB = Math.Abs((int)b % 10);
        int lastC = Math.Abs((int)c % 10);

        return Math.Max(Math.Max(lastA, lastB), lastC);
    }

    public override string ToString()
    {
        return $"A: {a}, B: {b}, C: {c}";
    }

}


public class Triangle : ThreeNumbers
{
    public Triangle(double a, double b, double c) : base(a, b, c)
    {
        ValidatePositive(a, nameof(a));
        ValidatePositive(b, nameof(b));
        ValidatePositive(c, nameof(c));

        if (!IsValidTriangle())
            throw new ArgumentException("Нельзя создать треугольник с такими сторонами");
    }

    private void ValidatePositive(double value, string paramName)
    {
        if (value <= 0)
        {
            throw new ArgumentException($"{paramName} должно быть положительным");
        }
    }

    private bool IsValidTriangle()
    {
        return a + b > c && a + c > b && b + c > a;
    }

    public double CalculatePerimeter()
    {
        return a + b + c;
    }

    public double CalculateArea()
    {
        double p = CalculatePerimeter() / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public override string ToString()
    {
        return $"Треугольник со сторонами: {base.ToString()}";
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(" Три числа ");
        ProcessThreeNumbers();

        Console.WriteLine("\n Треугольник ");
        ProcessTriangle();
    }

    static void ProcessThreeNumbers()
    {
        try
        {
            Console.WriteLine("Введите три числа:");
            double a = GetValidNumber(" число A: ");
            double b = GetValidNumber(" число B: ");
            double c = GetValidNumber(" число C: ");

            ThreeNumbers numbers = new ThreeNumbers(a, b, c);
            Console.WriteLine($"\nРезультат {numbers}:");
            Console.WriteLine($"Максимальная последняя цифра: {numbers.GetMaxLastDigit()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ProcessTriangle()
    {
        try
        {
            Console.WriteLine("\nВведите стороны треугольника");
            double a = GetPositiveNumber("сторона A: ");
            double b = GetPositiveNumber("сторона B: ");
            double c = GetPositiveNumber("сторона C: ");

            Triangle triangle = new Triangle(a, b, c);
            Console.WriteLine($"\nnРезультат {triangle}:");
            Console.WriteLine($"периметр: {triangle.CalculatePerimeter():F2}");
            Console.WriteLine($"площадь: {triangle.CalculateArea():F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static double GetValidNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double result))
                return result;
            Console.WriteLine("введите число");
        }
    }

    static double GetPositiveNumber(string prompt)
    {
        while (true)
        {
            double number = GetValidNumber(prompt);
            if (number > 0)
                return number;
            Console.WriteLine("число должно быть положительным");
        }
    }
}