using TestCoreApp.Areas.Employees.Models;

namespace TestCoreApp.Repo.Base
{
    public interface IEmpRepo : IRepo<Employee>
    {
        void setPayroll(Employee employee);
        decimal getSalary(Employee employee);
    }
}
