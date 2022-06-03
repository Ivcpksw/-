using System;

namespace Method_Golden_Ratio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Точность = ");
            double accuracy = Convert.ToDouble(Console.ReadLine());

            if (accuracy < 0)
            {
                Console.WriteLine("Точность не может быть меньше 0");
                return;
            }

            Console.Write("точка А0 = ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("точка B0 = ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("L0 = [" + a + "; " + b + "]");

            Method_Golden_Ratio(accuracy, a, b);
        }

        public static double fun(double x)
        {
            //return 2 * Math.Pow(x, 2) + 2 * x + ((double)7 / 2);
            return Math.Pow(x, 2) - 6 * x + 12;
        }

        public static void Method_Golden_Ratio(double accuracy, double a, double b)
        {
            int k = 0;
            int N = 2;
            double x = 0, y = 0, max_a = 0, max_b = 0, max_x = 0, max_y = 0, delta = 0, t_min = 0;

            while (Math.Abs(a - b) > accuracy)
            {
                x = a + 0.382 * (b - a);
                y = a + b - x;

                double Fx = fun(x);
                double Fy = fun(y);

                if (Fx <= Fy)
                {
                    max_a = a;
                    max_b = y;
                    max_x = max_a + max_b - x;
                    max_y = x;
                }
                else if (Fx > Fy)
                {
                    max_a = x;
                    max_b = b;
                    max_x = y;
                    max_y = max_a + max_b - y;
                }

                delta = Math.Abs(max_b - max_a);

                if (delta <= accuracy)
                {
                    Console.Write("x* Є [" + max_a + "; " + max_b + "]");
                    Console.WriteLine(" < " + accuracy);

                    t_min = (max_a + max_b) / 2;

                    Console.WriteLine("Точка min = " + t_min);
                    Console.WriteLine("k = " + k);

                    N = N * k;

                    Console.WriteLine("N = " + N);

                    a = max_a;
                    b = max_b;

                    max_a = 0;
                    max_b = 0;
                }
                else
                {
                    k++;

                    a = max_a;
                    b = max_b;

                    max_a = 0;
                    max_b = 0;
                }
            }
        }
    }
}