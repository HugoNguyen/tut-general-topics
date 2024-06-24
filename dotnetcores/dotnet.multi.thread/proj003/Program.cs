namespace proj003
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // PassData2ThreadFnWithoutTypeSafe();
            PassData2ThreadFnWithTypeSafe();
            Console.Read();
        }

        /// <summary>
        /// At the time of compilation, we will not get any compile-time error. But when we run the application, we will get the following runtime error.
        /// </summary>
        public static void PassData2ThreadFnWithoutTypeSafe()
        {
            Program obj = new Program();
            ParameterizedThreadStart PTSD = new ParameterizedThreadStart(obj.DisplayNumbers);
            Thread t1 = new Thread(PTSD);

            t1.Start("Hi");
        }

        public void DisplayNumbers(object Max)
        {
            int Number = Convert.ToInt32(Max);
            for (int i = 1; i <= Number; i++)
            {
                Console.WriteLine("Method1 :" + i);
            }
        }

        #region Pass data to thread fn with typesafe

        public static void PassData2ThreadFnWithTypeSafe()
        {
            int Max = 10;
            NumberHelper obj = new NumberHelper(Max);

            Thread T1 = new Thread(new ThreadStart(obj.DisplayNumbers));

            T1.Start();
            Console.Read();
        }

        public class NumberHelper
        {
            int _Number;

            public NumberHelper(int Number)
            {
                _Number = Number;
            }

            public void DisplayNumbers()
            {
                for (int i = 1; i <= _Number; i++)
                {
                    Console.WriteLine("value : " + i);
                }
            }
        }

        #endregion
    }
}
