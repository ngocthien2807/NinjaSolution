using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppServices.MonsterRepo
{
    public class MonsterRepositories
    {
        private readonly LienminhnhangiaContext context;

        public MonsterRepositories(LienminhnhangiaContext context)
        {
            this.context = context;
        }

        public bool AddMonster(Monster monster)
        {
            try
            {
                context.Monsters.Add(monster);
                return context.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMonster(Monster updateMonster)
        {
            try
            {
                context.Entry<Monster>(updateMonster).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteMonster(string ID)
        {
            try
            {
                var monster = context.Monsters.SingleOrDefault(monster => monster.MonsterId.Equals(ID));
                if (monster == null) return false;
                monster.Delete = true;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Monster> GetAllMonster()
        {
            try
            {
                return context.Monsters.Where(i => i.Delete == false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Monster GetMonsterbyID(string ID)
        {
            try
            {
                return context.Monsters.SingleOrDefault(monster => monster.MonsterId.Equals(ID) && monster.Delete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
