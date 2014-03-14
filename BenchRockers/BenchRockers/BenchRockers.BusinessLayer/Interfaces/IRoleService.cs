using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchRockers.Common.DataObjects;

namespace BenchRockers.BusinessLayer.Interfaces
{
    public interface IRoleService
    {
        List<Role> GetAllRoles();
        Role CreateRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(int RoleId);
    }
}
