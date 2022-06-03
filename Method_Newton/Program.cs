using System;

namespace Method_Newton
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите 1-ю точку = ");
            double Point_1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите 2-ю точку = ");
            double Point_2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Eps_1 = ");
            double eps_1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Eps_2 = ");
            double eps_2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Число итераций (М) = ");
            int M = Convert.ToInt32(Console.ReadLine());

            if (eps_1 <= 0 || eps_2 <= 0)
            {
                Console.WriteLine("Eps не может быть меньше 0");
                return;
            }
            else if (M <= 0)
            {
                Console.WriteLine("Предельное число итераций не может быть меньше 0");
                return;
            }

            Console.WriteLine("▼f(x) = (10x - y + 1; 2y-x)");
            Console.WriteLine('\n');

            Method_Newton(Point_1, Point_2, eps_1, eps_2, M);
        }

        public static double F_max_x(double x, double y)
        {
            return 5 * Math.Pow(x, 2) + Math.Pow(y, 2) - x * y + x;
        }

        public static double F_max_y(double Point_1, double Point_2)
        {
            return 5 * Math.Pow(Point_1, 2) + Math.Pow(Point_2, 2) - Point_1 * Point_2 + Point_1;
        }

        public static double New_Fun_x(double Point_1, double Point_2)
        {
            return 10 * (float)Point_1 - (float)Point_2 + 1;
        }

        public static double New_Fun_y(double Point_1, double Point_2)
        {
            return (2 * (float)Point_2) - (float)Point_1;
        }

        public static void Method_Newton(double Point_1, double Point_2, double eps_1, double eps_2, int M)
        {
            int k = 0;

            double x, y, norma, max_norma = 0, f_max_x, f_max_y, modul, det, revers_det, count = 0;
            double [] d = new double[2];

            // Шаг 3
            x = New_Fun_x(Point_1, Point_2);
            y = New_Fun_y(Point_1, Point_2);
            Console.WriteLine("▼f(x^" + k + ") = (" + x + "; " + y + ")");
            
            while (k < M)
            {
                if (k > 0)
                {
                    x = New_Fun_x(x, y);
                    y = New_Fun_y(Point_1, y);
                }

                // Шаг_4 + Шаг_5
                norma = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                Console.WriteLine("||▼f(x^" + k + ")|| = " + norma);

                if (norma <= eps_1)
                {
                    Console.WriteLine("Точка минимума x^(" + k + ") = (" + Point_1 + "; " + Point_2 + ") " + "- есть точка локального и одновременно глобального минимума f(x)");
                    return;
                }
                else if (k >= M)
                {
                    Console.WriteLine("Точка минимума x^* = " + norma);
                    return;
                }

                // Матрица Гессе. Шаг 6
                double[,] Hesse = new double[,] { { 10, -1 }, { -1, 2 } };
                Console.WriteLine("H(x) = ");
                for (int i = 0; i <= Hesse.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= Hesse.GetUpperBound(1); j++)
                    {
                        Console.Write(Hesse[i, j] + " ");
                    }
                    Console.WriteLine();
                }

                // Находим определитель матрицы
                det = (Hesse[0, 0] * Hesse[1, 1]) - (Hesse[0, 1] * Hesse[1, 0]);
                Console.WriteLine("det = " + det);
                Console.WriteLine();

                // Обратная матрица Гессе. Шаг 7
                double[,] revers_Hesse = new double[,] { { 2, 1 }, { 1, 10 } };
                Console.WriteLine("H^-1(x) = ");
                for (int i = 0; i <= revers_Hesse.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= revers_Hesse.GetUpperBound(1); j++)
                    {
                        Console.Write(revers_Hesse[i, j] + " ");
                    }
                    Console.WriteLine();
                }

                // Находим определитель обратной матрицы
                revers_det = (revers_Hesse[0, 0] * revers_Hesse[1, 1]) - (revers_Hesse[0, 1] * revers_Hesse[1, 0]);
                Console.WriteLine("revers_det = " + revers_det + " > " + 0);
                Console.WriteLine();

                // Шаг 9
                Console.Write("d(" + k + ") = -H^-1(x^" + k + ") * ▼f(x^" + k + ") = (");
                for (int i = 0; i < d.Length; i++)
                {
                    for (int j = 0; j < d.Length; j++)
                    {
                        d[i] = -(((revers_Hesse[i, j]) * x + (revers_Hesse[i, j + 1]) * y) / det);
                        break;
                    }
                }
                Console.WriteLine(d[0] + "; " + d[1] + ")");

                // Шаг 10
                x = Point_1 + d[0];
                y = Point_2 + d[1];
                Console.WriteLine("x^(" + (k + 1) + ") = (" + x + "; " + y + ")");

                // Шаг 11
                norma = Math.Sqrt(Math.Pow((x - Point_1), 2) + Math.Pow((y - Point_2), 2));
                Console.Write("||x^" + (k + 1) + " - x^" + k + "|| = " + norma);

                if (norma > eps_2) Console.WriteLine(" > " + eps_2);

                f_max_x = F_max_x(x, y);
                f_max_y = F_max_y(Point_1, Point_2);
                modul = Math.Abs(f_max_x - f_max_y);
                Console.WriteLine("|f(x^" + (k + 1) + ") - f(x^" + k + ")| = " + modul);

                if (modul < eps_2 && max_norma < eps_2 && count == 1)
                {
                    Console.WriteLine("Точка минимума x^* = " + max_norma);
                    return;
                }
                else k++;

                Point_1 = x;
                Point_2 = y;

                if (norma < eps_2 && modul < eps_2) count = 1;
                else count = 0;

                Console.WriteLine('\n');
            }
        }
    }
}