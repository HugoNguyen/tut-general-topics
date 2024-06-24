namespace proj019
{
    internal class Sample09TaskContinuationV1
    {
        public static void Run()
        {
            Task<string> task1 = Task.Run(() =>
            {
                return 12;
            }).ContinueWith((antecedent) =>
            {
                return $"The Square of {antecedent.Result} is: {antecedent.Result * antecedent.Result}";
            });
            Console.WriteLine(task1.Result);

            Console.ReadKey();
        }
    }
}
