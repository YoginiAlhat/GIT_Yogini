using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchRockers.BusinessLayer.Interfaces;
using BenchRockers.Common.DataObjects;
using BenchRockers.Common.Interfaces;
using BenchRockers.DataAccessLayer;

namespace BenchRockers.BusinessLayer.Services
{
    public class EmployeeSkillService : IEmployeeSkillService
    {
        private readonly IDataContext _dataSource;

        //public EmployeeSkillService()
        //{

        //}

        public EmployeeSkillService(IDataContext datasource)
        {

            _dataSource = datasource;

        }


        public List<Common.DataObjects.EmployeeSkill> GetEmployeeSkills()
        {
            return _dataSource.Query<EmployeeSkill>().ToList();
        }

        public List<Common.DataObjects.Skill> GetSkills()
        {
            return _dataSource.Query<Skill>().ToList();
        }

        public Common.DataObjects.EmployeeSkill CreateSkill(Common.DataObjects.EmployeeSkill employeeSkill)
        {
            _dataSource.Add<EmployeeSkill>(employeeSkill);
            return employeeSkill;
        }

        public void Edit(int employeeSkillId)
        {
            EmployeeSkill employeeSkill = _dataSource.Query<EmployeeSkill>().FirstOrDefault(s => s.EmployeeSkillId == employeeSkillId);
            _dataSource.Update<EmployeeSkill>(employeeSkill);
        }
    }
}
