namespace proj004
{
    internal class Program
    {
        #region First Create the callback delegate with the same signature of the callback method.
        // First Create the callback delegate with the same signature of the callback method.
        public delegate void ResultCallbackDelegate(int Results);

        //Creating the Helper class
        public class NumberHelper
        {
            //Creating two private variables to hold the Number and ResultCallback instance
            private int _Number;
            private ResultCallbackDelegate _resultCallbackDelegate;

            //Initializing the private variables through constructor
            //So while creating the instance you need to pass the value for Number and callback delegate
            public NumberHelper(int Number, ResultCallbackDelegate resultCallbackDelagate)
            {
                _Number = Number;
                _resultCallbackDelegate = resultCallbackDelagate;
            }
            //This is the Thread function which will calculate the sum of the numbers
            public void CalculateSum()
            {
                int Result = 0;
                for (int i = 1; i <= _Number; i++)
                {
                    Result = Result + i;
                }
                //Before the end of the thread function call the callback method
                if (_resultCallbackDelegate != null)
                {
                    _resultCallbackDelegate(Result);
                }
            }
        }
        #endregion

        // Second, Callback method and the signature should be the same as the callback delegate signature
        public static void ResultCallBackMethod(int Result)
        {
            Console.WriteLine("The Result is " + Result);
        }

        static void Main(string[] args)
        {
            //Create the ResultCallbackDelegate instance and to its constructor 
            //pass the callback method name
            ResultCallbackDelegate resultCallbackDelegate = new ResultCallbackDelegate(ResultCallBackMethod);

            int Number = 10;
            //Creating the instance of NumberHelper class by passing the Number
            //the callback delegate instance
            NumberHelper obj = new NumberHelper(Number, resultCallbackDelegate);
            //Creating the Thread using ThreadStart delegate
            Thread T1 = new Thread(new ThreadStart(obj.CalculateSum));

            T1.Start();
            Console.Read();
        }

        
    }
}
