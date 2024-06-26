namespace proj03_fluent_builder_with_recursive_generic
{
    public class EmployeePositionBuilder<T> : EmployeeInfoBuilder<EmployeePositionBuilder<T>> where T : EmployeePositionBuilder<T>
    {
        public T AtPosition(string position)
        {
            employee.Position = position;
            return (T)this;
        }
    }
}
