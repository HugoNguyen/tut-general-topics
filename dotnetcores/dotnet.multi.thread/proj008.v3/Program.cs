namespace proj008.v3
{
    internal class Program
    {
        static Mutex _mutex;

        static void Main()
        {
            //If IsSingleInstance returns true continue with the Program else Exit the Program
            if (!IsSingleInstance())
            {
                Console.WriteLine("More than one instance"); // Exit program.
            }
            else
            {
                Console.WriteLine("One instance"); // Continue with program.
            }
            // Stay Open.
            Console.ReadLine();
        }
        static bool IsSingleInstance()
        {
            try
            {
                // Try to open Existing Mutex.
                //If MyMutex is not opened, then it will throw an exception
                Mutex.OpenExisting("MyMutex");
            }
            catch
            {
                // If exception occurred, there is no such mutex.
                _mutex = new Mutex(true, "MyMutex");
                // Only one instance.
                return true;
            }
            // More than one instance.
            return false;
        }
    }
}
