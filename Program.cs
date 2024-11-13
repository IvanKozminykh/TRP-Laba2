using System;

public class OptimizationMethods
{
    // Функция f(x, y) = (x - 3)^2 + (y - 2)^2
    public double Function(double x, double y)
    {
        return Math.Pow(x - 3, 2) + Math.Pow(y - 2, 2);
    }

    // Частные производные для метода наискорейшего спуска
    public double PartialDerivativeX(double x, double y)
    {
        return 2 * (x - 3);
    }

    public double PartialDerivativeY(double x, double y)
    {
        return 2 * (y - 2);
    }

    // Метод наискорейшего спуска
    public void SteepestDescent()
    {
        double x = 0.0; // Начальные значения
        double y = 0.0;
        double tolerance = 1e-6;
        double lambda = 0.1; // Шаг
        int maxIterations = 1000;

        for (int i = 0; i < maxIterations; i++)
        {
            double gradX = PartialDerivativeX(x, y);
            double gradY = PartialDerivativeY(x, y);

            double nextX = x - lambda * gradX;
            double nextY = y - lambda * gradY;

            // Проверка на достижение точности
            if (Math.Sqrt(Math.Pow(nextX - x, 2) + Math.Pow(nextY - y, 2)) < tolerance)
            {
                Console.WriteLine($"Минимум методом наискорейшего спуска: x = {nextX}, y = {nextY}, f(x, y) = {Function(nextX, nextY)}");
                return;
            }

            x = nextX;
            y = nextY;
        }

        Console.WriteLine("Метод наискорейшего спуска не сошелся за указанное количество итераций.");
    }

    // Метод Нелдера-Мида
    public void NelderMead()
    {
        double[][] points = new double[3][]
        {
            new double[] { 0, 0 },
            new double[] { 1, 0 },
            new double[] { 0, 1 }
        };
        double tolerance = 1e-6;
        int maxIterations = 1000;

        for (int iter = 0; iter < maxIterations; iter++)
        {
            Array.Sort(points, (a, b) => Function(a[0], a[1]).CompareTo(Function(b[0], b[1])));
            double[] centroid = new double[] { (points[0][0] + points[1][0]) / 2, (points[0][1] + points[1][1]) / 2 };

            double[] reflected = new double[] { centroid[0] + (centroid[0] - points[2][0]), centroid[1] + (centroid[1] - points[2][1]) };

            if (Function(reflected[0], reflected[1]) < Function(points[0][0], points[0][1]))
            {
                double[] expanded = new double[] { centroid[0] + 2 * (reflected[0] - centroid[0]), centroid[1] + 2 * (reflected[1] - centroid[1]) };
                points[2] = Function(expanded[0], expanded[1]) < Function(reflected[0], reflected[1]) ? expanded : reflected;
            }
            else if (Function(reflected[0], reflected[1]) < Function(points[1][0], points[1][1]))
            {
                points[2] = reflected;
            }
            else
            {
                double[] contracted = new double[] { centroid[0] + 0.5 * (points[2][0] - centroid[0]), centroid[1] + 0.5 * (points[2][1] - centroid[1]) };
                if (Function(contracted[0], contracted[1]) < Function(points[2][0], points[2][1]))
                {
                    points[2] = contracted;
                }
                else
                {
                    points[1] = new double[] { (points[0][0] + points[1][0]) / 2, (points[0][1] + points[1][1]) / 2 };
                    points[2] = new double[] { (points[0][0] + points[2][0]) / 2, (points[0][1] + points[2][1]) / 2 };
                }
            }

            if (Math.Sqrt(Math.Pow(points[2][0] - points[0][0], 2) + Math.Pow(points[2][1] - points[0][1], 2)) < tolerance)
            {
                Console.WriteLine($"Минимум методом Нелдера-Мида: x = {points[0][0]}, y = {points[0][1]}, f(x, y) = {Function(points[0][0], points[0][1])}");
                return;
            }
        }

        Console.WriteLine("Метод Нелдера-Мида не сошелся за указанное количество итераций.");
    }

    // Основной метод для запуска
    public static void Main()
    {
        OptimizationMethods optimization = new OptimizationMethods();
        optimization.SteepestDescent(); // Запуск метода наискорейшего спуска
        optimization.NelderMead(); // Запуск метода Нелдера-Мида
    }
}
