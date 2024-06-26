namespace proj03_fluent_builder_with_recursive_generic
{
    public class EmployeeInfoBuilder<T> : EmployeeBuilder where T : EmployeeInfoBuilder<T>
    {
        public T SetName(string name)
        {
            employee.Name = name;
            return (T)this;
        }
    }
}
