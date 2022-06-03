using System;

namespace Method_Fletcher_Reeves
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

            Method_Fletcher_Reeves(Point_1, Point_2, eps_1, eps_2, M);
        }

        public static double F_max_x(double x, double y)
        {
            return 5 * Math.Pow(x, 2) + Math.Pow(y, 2) - x * y + x;
            //return 2 * Math.Pow(x, 2) + x * y + Math.Pow(y, 2);
        }

        public static double F_max_y(double Point_1, double Point_2)
        {
            return 5 * Math.Pow(Point_1, 2) + Math.Pow(Point_2, 2) - Point_1 * Point_2 + Point_1;
            //return 2 * Math.Pow(Point_1, 2) + Point_1 * Point_2 + Math.Pow(Point_2, 2);
        }

        public static double Max_X(double x, double Point_1, double t)
        {
            return (float)(Point_1 + (x * t));
        }

        public static double Max_Y(double y, double Point_2, double t)
        {
            return (float)(Point_2 + (y * t));
        }

        // Сначала берем все значения, где есть неизвестная переменная
        public static double Sqr_x2(double x, double y)
        {
            return 5 * Math.Pow(x, 2) + Math.Pow(y, 2) - (-(x) * (-(y)));
            //return 2 * Math.Pow(x, 2) + (-(x) * (-(y))) + Math.Pow(y, 2);
        }

        // Расскрываем скобки в ф-ии + значение t
        public static double Value_x(double Point_1, double Point_2, double x, double y)
        {
            return 5 * (-2 * Point_1 * x) + (-2 * Point_2 * y) - (Point_1 * (-(y)) + (-(x) * Point_2)) - (x);
            //return 2 * (-2 * Point_1 * x) + (Point_1 * (-(y)) + (-(x) * Point_2)) + (-2 * Point_2 * y);
        }

        public static double Flet_Reev_x(double Point_1, double Point_2)
        {
            return 10 * (float)Point_1 - (float)Point_2 + 1;
            //return 4 * (float)Point_1 + (float)Point_2;
        }

        public static double Flet_Reev_y(double Point_1, double Point_2)
        {
            return (2 * (float)Point_2) - (float)Point_1;
            //return (float)Point_1 + (float)(2 * Point_2);
        }

        public static void Method_Fletcher_Reeves(double Point_1, double Point_2, double eps_1, double eps_2, int M)
        {
            int k = 0;

            double x, y, norma, pred_norma = 1, max_norma = 0, f_max_x, f_max_y, modul, count = 0, t, value_t, value, Beta;
            double[] d = new double[2];
            double[] pred_d = new double[2];

            while (k < M)
            {
                // Шаг 3
                x = Flet_Reev_x(Point_1, Point_2);
                y = Flet_Reev_y(Point_1, Point_2);
                Console.WriteLine("▼f(x^" + k + ") = (" + x + "; " + y + ")");

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

                if (k == 0)
                {
                    // Шаг 6
                    Console.Write("d(" + k + ") = -▼f(x^" + k + ") = (");
                    for (int i = 0; i < d.Length; i++)
                    {
                        if (i == 0)
                        {
                            d[i] = -(x);
                            pred_d[i] = d[i];
                            continue;
                        }
                        else
                        {
                            d[i] = -(y);
                            pred_d[i] = d[i];
                        }
                    }
                    Console.WriteLine(d[0] + "; " + d[1] + ")");
                    pred_norma = norma;
                }
                else
                {
                    // Шаг 7
                    Beta = Math.Pow(norma, 2)/Math.Pow(pred_norma, 2);
                    Console.WriteLine("Beta(" + k + ") = " + Beta);

                    // Шаг 8
                    Console.Write("d(" + k + ") = -▼f(x^" + k + ") = (");
                    for (int i = 0; i < d.Length; i++)
                    {
                        if (i == 0)
                        {
                            d[i] = -(x) + Beta * pred_d[i];

                            continue;
                        }
                        else
                        {
                            d[i] = -(y) + Beta * pred_d[i];
                        }
                    }
                    Console.WriteLine(d[0] + "; " + d[1] + ")");
                }

                // Шаг 9
                Console.Write("t^*(" + k + ") = f(x^(" + k + ")+t(" + k + ") * d^(" + k + ") = ");

                value_t = Sqr_x2(d[0], d[1]);
                value = Value_x(Point_1, Point_2, d[0], d[1]);
                t = (float)value / (float)(2 * value_t); // Находим величину шага
                Console.WriteLine("t(" + k + ") = " + t);

                // Шаг 10
                x = Max_X(d[0], Point_1, t);
                y = Max_Y(d[1], Point_2, t);

                Console.WriteLine("X^(" + (k + 1) + ") = " + " (" + x + "; " + y + ")");

                norma = Math.Sqrt(Math.Pow((x - Point_1), 2) + Math.Pow((y - Point_2), 2));
                Console.Write("||x^" + (k + 1) + " - x^" + k + "|| = " + norma);

                if (norma > eps_2) Console.WriteLine(" > " + eps_2);

                f_max_x = F_max_x(x, y);
                f_max_y = F_max_y(Point_1, Point_2);
                modul = Math.Abs(f_max_x - f_max_y);
                Console.Write("|f(x^" + (k + 1) + ") - f(x^" + k + ")| = " + modul);
                if (modul > eps_2) Console.WriteLine(" > " + eps_2);

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