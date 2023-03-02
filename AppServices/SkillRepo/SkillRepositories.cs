using AutoMapper;
using DataAccess;
using DataAccess.Models;
using DTOs.SkillDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppServices.SkillRepo
{
    public class SkillRepositories
    {
        private readonly LienminhnhangiaContext context;
        IMapper mapper;

        public SkillRepositories(LienminhnhangiaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool AddSkill(Skill skill)
        {
            try
            {
                context.Skills.Add(skill);
                return context.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateSkill(Skill updateSkill)
        {
            try
            {
                context.Entry<Skill>(updateSkill).State = EntityState.Modified;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteSkill(string ID)
        {
            try
            {
                var skill = context.Skills.SingleOrDefault(ski => ski.SkillId.Equals(ID));
                if (skill == null) return false;
                skill.Delete = true;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Skill> GetAllSkill()
        {
            try
            {
                var list = context.Skills.Where(ski => ski.Delete == false).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ViewSkillInfo> GetSkillbyCharacter(string characterID)
        {
            try
            {
                var skills = context.Skills.Where(ski => ski.CharacterId.Equals(characterID) && ski.Delete == false).ToList();
                var viewSkills = mapper.Map<List<Skill>, List<ViewSkillInfo>>(skills);
                return viewSkills;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ViewSkillAccountInfo> GetSkillbyAccount(int accountID, string characterID)
        {
            try
            {
                var skills = context.AccountSkills
                                        .Include(accountSkill => accountSkill.Skill)
                                        .Where(accountSkill => accountSkill.AccountId == accountID
                                                            && accountSkill.Skill.CharacterId.Equals(characterID)
                                                            && accountSkill.Delete == false)
                                        .Select(skill => skill)
                                        .ToList();

                var viewSkills = mapper.Map<List<AccountSkill>, List<ViewSkillAccountInfo>>(skills);


                return viewSkills;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
