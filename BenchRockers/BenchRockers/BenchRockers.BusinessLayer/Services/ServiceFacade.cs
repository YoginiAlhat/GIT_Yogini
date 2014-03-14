using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchRockers.BusinessLayer.Interfaces;
using BenchRockers.Common.Interfaces;
using BenchRockers.DataAccessLayer;

namespace BenchRockers.BusinessLayer.Services
{
    public class ServiceFacade : IServiceFacade
    {
        public ServiceFacade(IDataContext dbContext)
        {
            EmployeeService = new EmployeeService(dbContext);
            RoleService = new RoleService(dbContext);
            SkillService = new SkillService(dbContext);
            RecommendationService = new RecommendationService(dbContext);
            EmployeeSkillService = new EmployeeSkillService(dbContext);

        }

        public IEmployeeService EmployeeService { get; set; }

        public IEmployeeSkillService EmployeeSkillService { get; set; }

        public IRecommendationService RecommendationService { get; set; }

        public IRoleService RoleService { get; set; }

        public ISkillService SkillService { get; set; }
    }
}
