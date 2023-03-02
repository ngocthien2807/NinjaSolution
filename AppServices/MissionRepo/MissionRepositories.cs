using AutoMapper;
using DataAccess;
using DataAccess.Models;
using DTOs.MissionDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppServices.MissionRepo
{
    public class MissionRepositories
    {
        private readonly LienminhnhangiaContext context;
        IMapper mapper;


        public MissionRepositories(LienminhnhangiaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool AddMission(Mission mission)
        {
            try
            {
                context.Missions.Add(mission);
                return context.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateMission(Mission updateMission)
        {
            try
            {
                context.Entry<Mission>(updateMission).State = EntityState.Modified;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteMission(string ID)
        {
            try
            {
                var mission = context.Missions.SingleOrDefault(mission => mission.MissionId.Equals(ID));
                if (mission == null) return false;
                mission.Delete = true;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ViewMission> GetMissionbyAccount(int accountID)
        {
            try
            {
               var accountMissions = context.AccountMissions.Include(accountMission => accountMission.Mission)
                                        .Where(accountMission => accountMission.AccountId == accountID
                                                                && accountMission.Delete == false)
                                        .Select(accountMission => accountMission)
                                        .ToList();

                List<ViewMission> viewMissions = mapper.Map<List<AccountMission>, List<ViewMission>>(accountMissions);

                return viewMissions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
    }
}
