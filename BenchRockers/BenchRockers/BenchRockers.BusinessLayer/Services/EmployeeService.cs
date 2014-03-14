using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchRockers.BusinessLayer.Interfaces;
using BenchRockers.DataAccessLayer;
using System.Data.Entity;
using BenchRockers.Common.DataObjects;
using BenchRockers.Common.Interfaces;

namespace BenchRockers.BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDataContext _dataSource;

        public EmployeeService()
        {

        }

        public EmployeeService(IDataContext datasource)
        {

            _dataSource = datasource;

        }

        public List<Common.DataObjects.Employee> GetAllEmployees()
        {
            return _dataSource.Query<Employee>().ToList();
        }

        public Common.DataObjects.Employee CreateEmployee(Common.DataObjects.Employee employee)
        {
            _dataSource.Add<Employee>(employee);            
            return employee;
        }

        public void UpdateEmployee(Common.DataObjects.Employee employee)
        {
            _dataSource.Update<Employee>(employee);
            //_dataSource.SaveChanges();
        }

        public void DeleteEmployee(int employeeId)
        {
            Employee employee = _dataSource.Query<Employee>().FirstOrDefault(s => s.EmployeeId == employeeId);
            employee.IsOnBench = false;
            _dataSource.Update<Employee>(employee);

            //db.Employees.Remove(employee);
            //_dataSource.SaveChanges();
        }

        public List<Common.DataObjects.Role> GetRoles()
        {
            return _dataSource.Query<Role>().ToList();
        }
    }
}
