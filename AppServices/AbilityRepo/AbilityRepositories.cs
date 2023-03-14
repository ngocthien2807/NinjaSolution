using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppServices.AbilityRepo
{
    public class AbilityRepositories
    {
        private readonly LienminhnhangiaContext context;

        public AbilityRepositories(LienminhnhangiaContext context)
        {
            this.context = context;
        }

        public bool AddAbility(Ability ability)
        {
            try
            {
                if (context.Abilities.SingleOrDefault(abi => abi.AbilityId.Equals(ability.AbilityId)) != null)
                {
                    throw new Exception($"ID nội tại {ability.AbilityId} đã tồn tại");
                }

                context.Abilities.Add(ability);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateAbility(Ability updateAbi)
        {
            try
            {
                context.Entry<Ability>(updateAbi).State = EntityState.Modified;
                return context.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteAbility(string abilityId)
        {
            try
            {
                var ability = context.Abilities.SingleOrDefault(abi => abi.AbilityId.Equals(abilityId));
                if (ability == null) return false;
                ability.Delete = true;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Ability> GetAllAbility(int? total)
        {
            try
            {
                if(total != null) 
                    return context.Abilities.Where(i => i.Delete == false).Take((int)total).ToList();

                return context.Abilities.Where(i => i.Delete == false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ability GetAbilitybyID(string abilityId)
        {
            try
            {
                return context.Abilities
                    .SingleOrDefault(abi => abi.AbilityId.Equals(abilityId) && abi.Delete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<Ability> GetAbilitybyAccount(int accountId)
        {
            try
            {
                var accountAbilities = context.AccountAbilities
                                        .Where(accountAbility => 
                                                    accountAbility.AccountId == accountId 
                                                &&  accountAbility.Delete == false)
                                        .Select(accountAbility => accountAbility.AbilityId)
                                        .ToList();

                var abilities = context.Abilities
                                .Where (ability => accountAbilities.Contains(ability.AbilityId))
                                .Select(ability => ability)
                                .ToList();

                return abilities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
