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
    public class SkillService : ISkillService
    {
        private readonly IDataContext _dataSource;

        public SkillService()
        {

        }

        public SkillService(IDataContext datasource)
        {

            _dataSource = datasource;

        }

        //public SkillService()
        //{

        //}

        public List<Common.DataObjects.Skill> GetAllSkills()
        {
            return _dataSource.Query<Skill>().ToList();
        }

        public Common.DataObjects.Skill CreateSkill(Common.DataObjects.Skill skill)
        {
            _dataSource.Add<Skill>(skill);
            return skill;
        }

        public void UpdateSkill(Common.DataObjects.Skill skill)
        {
            _dataSource.Update<Skill>(skill);
        }

        public void DeleteSkill(int skillId)
        {
            Skill skill = _dataSource.Query<Skill>().FirstOrDefault(s => s.SkillId == skillId);
            _dataSource.Delete<Skill>(skill);
        }
    }
}
