using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchRockers.Common.DataObjects;

namespace BenchRockers.BusinessLayer.Interfaces
{
    public interface ISkillService
    {
        List<Skill> GetAllSkills();
        Skill CreateSkill(Skill skill);
        void UpdateSkill(Skill skill);
        void DeleteSkill(int skillId);
    }
}
