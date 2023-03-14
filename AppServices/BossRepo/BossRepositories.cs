using AutoMapper;
using DataAccess;
using DataAccess.Models;
using DTOs.BossDTOs;
using DTOs.MonsterDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppServices.BossRepo
{
    public class BossRepositories
    {
        private readonly LienminhnhangiaContext context;
        IMapper mapper;

        public BossRepositories(LienminhnhangiaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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

        public List<T> GetAllBoss<T>(int? total, bool isAdmin = false)
        {
            try
            {
                var bosses = context.Bosses.Where(boss => boss.Delete == false).ToList();

                if (isAdmin)
                {
                    return bosses.Cast<T>().ToList();
                }

                var viewBosses = mapper.Map<List<Boss>, List<ViewBoss>>(bosses);

                if (total != null)
                {
                    viewBosses = viewBosses.Take((int)total).ToList();
                }

                return viewBosses.Cast<T>().ToList();
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
