using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppServices.BossRepo
{
    public class BossRepositories
    {
        private readonly LienminhnhangiaContext context;

        public BossRepositories(LienminhnhangiaContext context)
        {
            this.context = context;
        }

        public bool AddBoss(Boss boss)
        {
            try
            {
                context.Bosses.Add(boss);
                return context.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateBoss(Boss updateBoss)
        {
            try
            {
                context.Entry<Boss>(updateBoss).State = EntityState.Modified;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteBoss(string ID)
        {
            try
            {
                var boss = context.Bosses.SingleOrDefault(boss => boss.BossId.Equals(ID));
                if (boss == null) return false;
                boss.Delete = true;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Boss> GetAllBoss()
        {
            var list = new List<Boss>();
            try
            {
                list = context.Bosses.Where(boss => boss.Delete == false).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public Boss GetBossbyID(string ID)
        {
            try
            {
                return context.Bosses.SingleOrDefault(boss => boss.BossId.Equals(ID) && boss.Delete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
    }
}
