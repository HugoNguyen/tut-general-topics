namespace proj019
{
    internal class Sample05ImplementSynchronousMethodUsingTask
    {
        public static void Run()
        {
            Console.WriteLine("Main Method Started");
            SomeMethod1();
            SomeMethod2();
            SomeMethod3();
            SomeMethod4();

            Console.WriteLine("Main Method Completed");
            Console.ReadKey();
        }
        
        //Method returning Task but it is synchronous
        static Task SomeMethod1()
        {
            //Do Some Task
            Console.WriteLine("SomeMethod1 Executing, It does not return anything");
            //When your method returning Task in synchronous, return Task.CompletedTask
            return Task.CompletedTask;
        }
        
        //Synchronous Method returning Task<T>
        static Task<string> SomeMethod2()
        {
            string someValue = "";
            someValue = "SomeMethod2 Returing a String";
            Console.WriteLine("SomeMethod2 Executing, It return a string");
            //When your synchronous method returning Task<T>, use Task.FromResult
            return Task.FromResult(someValue);
        }
        
        //Synchronous Method returning Task with Exception
        static Task SomeMethod3()
        {
            Console.WriteLine("SomeMethod3 Executing, It will throw an Exception");
            //When your synchronous method returning Task with Exception use, Task.FromException
            return Task.FromException(new InvalidOperationException());
        }
        
        //Synchronous Method Cancelling a Task
        static Task SomeMethod4()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Cancel();
            Console.WriteLine("SomeMethod4 Executing, It will Cancel the Task Exception");
            //When your synchronous method cancelling a Task, Task.FromCanceled
            return Task.FromCanceled(cts.Token);
        }
    }
}
