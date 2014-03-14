using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchRockers.BusinessLayer.Interfaces
{
    public interface IServiceFacade
    {
        IEmployeeService EmployeeService { get; set; }

        IEmployeeSkillService EmployeeSkillService { get; set; }

        IRecommendationService RecommendationService { get; set; }

        IRoleService RoleService { get; set; }

        ISkillService SkillService { get; set; }

    }
}
