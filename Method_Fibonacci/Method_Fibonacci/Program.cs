using System;

namespace Method_Fibonacci
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

            Console.Write("Эпсилон = ");
            double eps = Convert.ToDouble(Console.ReadLine());

            if (eps < 0)
            {
                Console.WriteLine("Эпсилон не может быть меньше 0");
                return;
            }

            Console.Write("точка А0 = ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("точка B0 = ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("L0 = [" + a + "; " + b + "]");

            Method_Fibonacci(accuracy, a, b, eps);
        }

        public static double fun(double x)
        {
            //return 2 * Math.Pow(x, 2) + 2 * x + ((double)7 / 2);
            return Math.Pow(x, 2) - 6 * x + 12;
            //return 2 * Math.Pow(x, 2) - 12 * x;
        }

        public static void Method_Fibonacci(double accuracy, double a, double b, double eps)
        {
            int k = 0, N = 1;
            double x = 0, y = 0, max_a = 0, max_b = 0, max_x = 0, max_y = 0, Fib = 1.0,  Fib_a = 1.0, Fib_c = 0.0;

            if (b == 0)
            {
                Console.WriteLine("F" + "[" + (b + 1) + "] = " + Fib);
            }

            if (b == 1)
            {
                Console.WriteLine("F" + "[" + b + "] = " + Fib);
            }

            if (b == 2)
            {
                Console.WriteLine("F" + "[" + b + "] = " + (Fib + 1));
                N++;
            }

            if (b == 3)
            {
                Console.WriteLine("F" + "[" + b + "] = " + (Fib + 2));
                N += 2;
            }

            List<double> arr_value_Fib = new List<double>();

            for (int i = 3; i < 4; i++)
            {
                Fib_c = Fib_a + Fib;
                Console.WriteLine("F" + "[" + (i - 2) + "] = " + Fib);
                arr_value_Fib.Add(Fib);

                Fib_c = 0;

                while (Fib < (Math.Abs(b) / accuracy))
                {
                    Fib_c = Fib_a + Fib;
                    Fib_a = Fib;
                    Fib = Fib_c;

                    Console.WriteLine("F" + "[" + (i - 1) + "] = " + Fib);
                    arr_value_Fib.Add(Fib);

                    i++;
                    N++;
                }
                break;
            }
            Console.WriteLine("N = " + N);

            double[] arr_print_Fib = new double[N];
            arr_value_Fib.CopyTo(0, arr_print_Fib, 0, N);


            // В формуле Fn-2, а в программе, чтобы взять это знач., надо отнять на эл-т больше, т.к. индексация с 0
            x = a + (arr_print_Fib[arr_print_Fib.Length - 3] / Fib) * (b - a); 
            y = a + (arr_print_Fib[arr_print_Fib.Length - 2] / Fib) * (b - a);

            do
            {
                double Fx = fun(x);
                double Fy = fun(y);

                if (Fx <= Fy)
                {
                    max_a = a;
                    max_b = y;
                    max_y = x;

                    int count_1 = 0;
                    int count_2 = 0;

                    count_1 = N - k - 4;
                    count_2 = N - k - 2;

                    max_x = max_a + ((arr_print_Fib[count_1]) * (max_b - max_a)) / (arr_print_Fib[count_2]);

                    Console.WriteLine("L[" + max_a + "; " + max_b + "]");
                }
                else if (Fx > Fy)
                {
                    int count_11 = 0;
                    int count_22 = 0;

                    count_11 = N - k - 3;
                    count_22 = N - k - 2;

                    max_a = x;
                    //max_b = max_b;
                    max_x = y;
                    max_y = max_a + ((arr_print_Fib[count_11]) * (max_b - max_a)) / (arr_print_Fib[count_22]);

                    Console.WriteLine("L[" + max_a + "; " + max_b + "]");
                }

                x = max_x;
                y = max_y;

            } while (k++ != N - 3);

            x = y;
            y += eps;

            double Fxx = fun(x);
            double Fyy = fun(y);

            if (Fxx < Fyy)
            {
                //max_a = a;
                max_b = y;
            }

            double modul_Ln;

            Console.WriteLine("x* Є L[" + N +"]= [" + max_a + "; " + max_b + "]");

            modul_Ln = Math.Abs(max_b - max_a);

            Console.Write("|L" + N + "| = " + modul_Ln);
            Console.WriteLine(" < l = " + accuracy);

            double check_modul_Ln;

            check_modul_Ln = modul_Ln / b;
            Console.Write(check_modul_Ln + " ~= ");
            Console.WriteLine(1 / Fib);

            double min_x;

            min_x = (max_a + max_b) / 2;
            Console.WriteLine("x* = " + min_x);
        }
    }
}