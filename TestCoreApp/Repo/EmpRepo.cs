using TestCoreApp.Areas.Employees.Models;
using TestCoreApp.Data;
using TestCoreApp.Repo.Base;

namespace TestCoreApp.Repo
{
    public class EmpRepo : MainRepo<Employee>, IEmpRepo
    {
        public EmpRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;
        decimal IEmpRepo.getSalary(Employee employee)
        {
            throw new NotImplementedException();
        }

        void IEmpRepo.setPayroll(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
