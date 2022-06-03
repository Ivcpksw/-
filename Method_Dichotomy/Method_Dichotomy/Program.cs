using System;

namespace Method_Dichotomy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Эпсилон = ");
            double eps = Convert.ToDouble(Console.ReadLine());

            if (eps < 0)
            {
                Console.WriteLine("Эпсилон не может быть меньше 0");
                return;
            }

            Console.Write("Точность = ");
            double accuracy = Convert.ToDouble(Console.ReadLine());

            if (accuracy < 0 )
            {
                Console.WriteLine("Точность не может быть меньше 0");
                return;
            }

            Console.Write("Точка А0 = ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("Точка В0 = ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("L0 = [" + a + "; " + b + "]" );

            Method_Dichotomy(eps, accuracy, a, b);
        }

        public static double fun(double x)
        {
            //return 2 * Math.Pow(x, 2) + 2 * x + ((double)7 / 2);
            return (Math.Pow(x, 2) - 6 * x + 12);
        }

        public static void Method_Dichotomy(double eps, double accuracy, double a, double b, double ale = 1.1)
        {
            int k = 0;
            int N = 2;
            double x = 0, y = 0, max_a = 0, max_b = 0;

            while (Math.Abs(ale) > 2 * eps)
            {
                x = (a + b - eps) / 2;
                y = (a + b + eps) / 2;

                double Fx = fun(x);
                double Fy = fun(y);

                if (Fx <= Fy)
                {
                    max_a = a;
                    max_b = y;
                }
                else if (Fx > Fy)
                {
                    max_a = x;
                    max_b = b;
                }

                ale = Math.Abs(max_b - max_a);

                if (Math.Abs(ale) <= 2 * eps) 
                {
                    Console.Write("x*Є [" + max_a + "; " + max_b + "]");
                    Console.WriteLine(" < " + accuracy);
                    Console.WriteLine("k = " + k);
                    N += k;
                    Console.WriteLine("N = " + N);
                    Console.WriteLine("");
                }
                else if (Math.Abs(ale) > accuracy)
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






