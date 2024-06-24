namespace proj019
{
    internal class Sample06RetryPatterns
    {
        public static void Run()
        {
            Console.WriteLine("Main Method Started");
            RetryMethod();

            Console.WriteLine("Main Method Completed");
            Console.ReadKey();
        }

        static async void RetryMethod()
        {
            //It will retry 3 times, here the function is RetryOperation1
            try
            {
                await Retry(RetryOperation1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(RetryOperation1)} was Failed");
            }

            //It will retry 4 times, here the function is RetryOperation2
            try
            {
                await Retry(RetryOperation2, 4);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(RetryOperation2)} was Failed");
            }

            //It will retry 3 times, here the function is RetryOperationValueReturning
            try
            {
                var result = await Retry(RetryOperationValueReturning);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(RetryOperationValueReturning)} was Failed");
            }
        }

        //Generic Retry Method
        //Func is a generate delegate which returns something, in our case it is returning a Task
        //We are setting the default value for RetryTimes = 3 and WaitTime = 500 milliseconds
        static async Task Retry(Func<Task> fun, int RetryTimes = 3, int WaitTime = 500)
        {
            //Reducing the for loop Exection for 1 time
            for (int i = 0; i < RetryTimes - 1; i++)
            {
                try
                {
                    //Do the Operation
                    //We are going to invoke whatever function the generic func delegate points to
                    await fun();
                    Console.WriteLine("Operation Successful");
                    break;
                }
                catch (Exception Ex)
                {
                    //If the operations throws an error
                    //Log the Exception if you want
                    Console.WriteLine($"Retry {i + 1}: Getting Exception : {Ex.Message}");
                    //Wait for 500 milliseconds
                    await Task.Delay(WaitTime);
                }
            }
            //Final try to execute the operation
            await fun();
        }

        //Generic Retry Method Returning Value
        //Func is a generate delegate which returns something, in our case it is returning a Task
        //We are setting the default value for RetryTimes = 3 and WaitTime = 500 milliseconds
        static async Task<T> Retry<T>(Func<Task<T>> fun, int RetryTimes = 3, int WaitTime = 500)
        {
            //Reducing the for loop Exection for 1 time
            for (int i = 0; i < RetryTimes - 1; i++)
            {
                try
                {
                    //Do the Operation
                    //We are going to invoke whatever function the generic func delegate points to
                    //We will return from here if the operation was successful
                    return await fun();

                }
                catch (Exception Ex)
                {
                    //If the operations throws an error
                    //Log the Exception if you want
                    Console.WriteLine($"Retry {i + 1}: Getting Exception : {Ex.Message}");
                    //Wait for 500 milliseconds
                    await Task.Delay(WaitTime);
                }
            }
            //Final try to execute the operation
            return await fun();
        }

        static async Task RetryOperation1()
        {
            //Doing Some Processing
            await Task.Delay(500);
            //Throwing Exception so that retry will work
            throw new Exception($"Exception Occurred in {nameof(RetryOperation1)}");
        }
        static async Task RetryOperation2()
        {
            //Doing Some Processing
            await Task.Delay(500);
            //Throwing Exception so that retry will work
            throw new Exception($"Exception Occurred in {nameof(RetryOperation2)}");
        }

        public static async Task<string> RetryOperationValueReturning()
        {
            //Doing Some Processing and return the value
            await Task.Delay(500);
            //Uncomment the below code to successfully return a string
            return "Operation Successful";
            //Throwing Exception so that retry will work
            //throw new Exception($"Exception Occurred in {nameof(RetryOperationValueReturning)}");
        }
    }
}
