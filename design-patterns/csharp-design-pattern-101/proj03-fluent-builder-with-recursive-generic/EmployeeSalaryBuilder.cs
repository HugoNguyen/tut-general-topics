namespace proj03_fluent_builder_with_recursive_generic
{
    public class EmployeeSalaryBuilder<T> : EmployeePositionBuilder<EmployeeSalaryBuilder<T>> where T : EmployeeSalaryBuilder<T>
    {
        public T WithSalary(double salary)
        {
            employee.Salary = salary;
            return (T)this;
        }
    }
}
