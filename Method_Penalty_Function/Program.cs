using System;

namespace Method_Penalty_Function
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Целевая ф-ия = f(x)=5x^2-y^2-xy+x");
            Console.WriteLine("Ф-ия ограничения = g(x)=x+y=1");
            Console.WriteLine('\n');

            Console.Write("Начальная точка Х0 = ");
            double begin_point = Convert.ToDouble(Console.ReadLine());

            Console.Write("Начальное значение параметра штрафа r^>0 = ");
            int begin_penalty = Convert.ToInt32(Console.ReadLine());

            if (begin_penalty <= 0)
            {
                Console.WriteLine("Начальное значение параметра штрафа не может равняться нулю или быть меньше нуля!");
                return;
            }

            Console.Write("Значение для увеличения параметра C>0 = ");
            int inc_param = Convert.ToInt32(Console.ReadLine());

            if (inc_param <= 0)
            {
                Console.WriteLine("Значение для увеличения параметра не может равняться нулю или быть меньше нуля!");
                return;
            }

            Console.Write("Малое число eps>0 = ");
            double eps = Convert.ToDouble(Console.ReadLine());

            if (eps <= 0)
            {
                Console.WriteLine("Малое число не может равняться нулю или быть меньше нуля!");
                return;
            }

            Console.WriteLine("m = 1, ограничения-неравенства отсутствуют");
            Console.WriteLine('\n');

            Method_Penalty_Function(begin_point, begin_penalty, inc_param, eps);
        }

        // Вычитаем 2-е ур-ие из 1-о
        public static double Point_min_x(double penalty)
        {
            return (2 * penalty - 2) / (14 * penalty + 19);
        }

        public static double Point_min_y(double penalty)
        {
            return (12 * penalty - 1) / (14 * penalty + 19);
        }

        //// Безусловный минимум ф-ии x
        //public static double Absolute_Minimum_Function_x(double x, double y, double penalty, double g)
        //{
        //    return 10 * x - y + 1 + penalty * g;
        //}

        //// Безусловный минимум ф-ии y
        //public static double Absolute_Minimum_Function_y(double x, double y, double penalty, double g)
        //{
        //    return 2 * y - x + penalty * g;
        //}

        // Разложение выражения (x+y-1)^2 (Декомпозиция)
        public static double Decomposition(double x, double y)
        {
            return Math.Pow(x, 2) + 2 * x * y - 2 * x + Math.Pow(y, 2) - 2 * y + 1;
        }

        // Вспомогательная ф-ия
        public static double Auxiliary_Function(double x, double y, double penalty, double dec)
        {
            return 5 * Math.Pow(x, 2) + Math.Pow(y, 2) - x * y + x + ((penalty * dec) / 2);
        }

        // Целевая ф-ия
        public static double Purpose_Fun(double x, double y)
        {
            return 5 * Math.Pow(x, 2) + Math.Pow(y, 2) - x * y + x;
        }

        // Ф-ия ограничения
        public static double Limit_Fun(double x, double y)
        {
            return x + y - 1;
        }

        public static void Method_Penalty_Function(double begin_point, int begin_penalty, int inc_param, double eps)
        {
            int k = 0;
            double aux_fun, penalty = 1, x, y, dec, g, point_Lag, result_min, count_lamb = 1;

            while (k < 7)
            {
                x = Point_min_x(penalty);
                y = Point_min_y(penalty);

                Console.WriteLine("x*(r^" + k + ")= " + x);
                Console.WriteLine("y*(r^" + k + ")= " + y);

                g = Limit_Fun(x, y);

                dec = Decomposition(x, y);
                aux_fun = Auxiliary_Function(x, y, penalty, dec);
                Console.WriteLine("F(x*,r^" + k + ")= " + aux_fun);

                point_Lag = penalty * g;
                Console.WriteLine("Лаг." + count_lamb + "(r^" + k + ")= " + point_Lag);

                count_lamb++;
                k++;

                if (penalty == 1000) 
                {
                    Console.WriteLine('\n');

                    penalty = double.PositiveInfinity;

                    if(penalty == double.PositiveInfinity)
                    {
                        x = 1;
                        y = 1;
                    }

                    //x = Point_min_x(penalty);
                    //y = Point_min_y(penalty);

                    Console.WriteLine("x*(r^" + k + ")= " + x);
                    Console.WriteLine("y*(r^" + k + ")= " + y);

                    result_min = Purpose_Fun(x, y);
                    Console.WriteLine("F(x,r^" + k + ")= " + result_min);

                    point_Lag = -(x + y);
                    Console.WriteLine("Лаг." + count_lamb + "(r^" + k + ")= " + point_Lag);

                    break;
                }

                if (penalty == 100) penalty = 1000;
                if (penalty == 10) penalty = 100;
                if (penalty == 2) penalty = 10;
                if (penalty == 1) penalty = 2;

                Console.WriteLine('\n');
            }
        }
    }
}