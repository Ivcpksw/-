using System;

namespace Method_Gradient_Descent
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

            Console.WriteLine("Ўf(x) = (10x - y + 1; 2y-x)");
            Console.WriteLine('\n');

            Method_Gradient_Descent(Point_1, Point_2, eps_1, eps_2, M);
        }

        public static double Fun(double x, double y)
        {
            return 5 * Math.Pow(x, 2) + Math.Pow(y, 2) - x * y + x;
        }

        public static double Grad_Fun_X(double Point_1, double Point_2)
        {
            return 10 * Point_1 - Point_2 + 1;
        }

        public static double Grad_Fun_Y(double Point_1, double Point_2)
        {
            return 2 * Point_2 - Point_1;
        }

        public static double Sqr_x2(double x, double y)
        {
            return 5 * Math.Pow(x, 2) + Math.Pow(y, 2) - (-(x) * (-(y)));
        }

        public static double Value_x(double Point_1, double Point_2, double x, double y)
        {
            return 5 * (-2 * Point_1 * x) + (-2 * Point_2 * y) - (Point_1 * (-(y)) + (-(x) * Point_2)) - (x);
        }

        public static double Max_X(double x, double Point_1, double min_t)
        {
            return Point_1 - x * min_t;
        }

        public static double Max_Y(double y, double Point_2, double min_t)
        {
            return Point_2 - y * min_t;
        }

        public static double F_max_x(double max_x, double max_y)
        {
            return 5 * Math.Pow(max_x, 2) + Math.Pow(max_y, 2) - max_x * max_y + max_x;
        }

        public static double F_max_y(double Point_1, double Point_2)
        {
            return 5 * Math.Pow(Point_1, 2) + Math.Pow(Point_2, 2) - Point_1 * Point_2 + Point_1;
        }

        public static void Method_Gradient_Descent(double Point_1, double Point_2, double eps_1, double eps_2, int M)
        {
            int k = 0;
            double x = 0, y = 0, norma = 0, max_norma = 0, min_t = 0, t = 1, value_t = 0, value = 0, max_x = 0, max_y = 0, f_max_x = 0, f_max_y = 0, modul = 0, pred_modul = 0, count = 0;

            while (k < M)
            {
                // Шаг_3
                x = Grad_Fun_X(Point_1, Point_2);
                y = Grad_Fun_Y(Point_1, Point_2);

                Console.WriteLine("Ўf(x^" + k + ") = (" + x + "; " + y + ")");

                // Шаг_4 + Шаг_5
                norma = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                Console.WriteLine("||Ўf(x^" + k + ")|| = " + norma);
                if (norma < eps_1)
                {
                    Console.WriteLine("Точка минимума x^* = " + norma);
                    return;
                }
                else if (k >= M)
                {
                    Console.WriteLine("Точка минимума x^* = " + norma);
                    return;
                }

                // Шаг_6 + Шаг_7
                value_t = Sqr_x2(x, y);
                value = Value_x(Point_1, Point_2, x, y);

                min_t = -(value) / (2 * value_t);

                Console.WriteLine("t(" + k + ") = " + min_t);

                max_x = Max_X(x, Point_1, min_t);
                max_y = Max_Y(y, Point_2, min_t);
                Console.WriteLine("X^" + (k + 1) + " = " + " (" + max_x + "; " + max_y + ")");

                // Шаг_8
                max_norma = Math.Sqrt(Math.Abs(Math.Pow(max_x, 2) - Math.Pow(max_y, 2)));
                Console.WriteLine("||x^" + (k + 1) + " - x^" + k + "|| = " + max_norma);

                f_max_x = F_max_x(max_x, max_y);
                f_max_y = F_max_y(Point_1, Point_2);
                modul = Math.Abs(f_max_x - f_max_y);
                Console.WriteLine("|f(x^" + (k + 1) + ") - f(x^" + k + ")| = " + modul);


                if (modul < eps_2 && max_norma < eps_2 && count == 1)
                {
                    Console.WriteLine("Точка минимума x^* = " + max_norma);
                    return;
                }
                else k++;

                Point_1 = max_x;
                Point_2 = max_y;
                pred_modul = modul;

                if (max_norma < eps_2 && modul < eps_2) count = 1;
                else count = 0;

                Console.WriteLine('\n');
            }
        }
    }
}