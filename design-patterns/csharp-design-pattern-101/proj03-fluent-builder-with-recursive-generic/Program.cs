namespace proj03_fluent_builder_with_recursive_generic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var emp = EmployeeBuilderDirector
                .NewEmployee
                .SetName("Maks")
                .AtPosition("Software Developer")
                .WithSalary(3500)
                .Build();
            Console.WriteLine(emp);
        }
    }
}
