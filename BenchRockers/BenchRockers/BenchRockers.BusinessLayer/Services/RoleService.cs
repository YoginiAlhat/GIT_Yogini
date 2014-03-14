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
    public class RoleService: IRoleService
    {
        private readonly IDataContext _dataSource;

        //public RoleService()
        //{

        //}

        public RoleService(IDataContext datasource)
        {

            _dataSource = datasource;

        }

        public List<Common.DataObjects.Role> GetAllRoles()
        {
            return _dataSource.Query<Role>().ToList();
        }

        public Common.DataObjects.Role CreateRole(Common.DataObjects.Role role)
        {
            _dataSource.Add<Role>(role);
            return role;
        }

        public void UpdateRole(Common.DataObjects.Role role)
        {
            _dataSource.Update<Role>(role);
        }

        public void DeleteRole(int roleId)
        {
            Role role = _dataSource.Query<Role>().FirstOrDefault(s => s.RoleId== roleId);
            _dataSource.Delete<Role>(role);
        }
    }
}
 