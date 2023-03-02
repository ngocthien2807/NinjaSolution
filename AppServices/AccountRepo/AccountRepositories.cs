﻿using DataAccess;
using DataAccess.Models;
using DTOs.AccountDTOs;
using Obj_Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppServices.AccountRepo
{
    public class AccountRepositories
    {
        private readonly LienminhnhangiaContext context;

        public AccountRepositories(LienminhnhangiaContext context)
        {
            this.context = context;
        }

        public List<Account> GetAllAccount()
        {
            try
            {
                return context.Accounts.Where(account => account.Role != Role.Admin && account.Delete == false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public Payload Login(Login login)
        {
            try
            {
                var account = context.Accounts.SingleOrDefault(acc => acc.Username == login.Username && acc.Delete == false);
                if (account == null)
                {
                    throw new Exception("Account does not exists.");
                }

                if (account.Password != Common.Hash(login.Password))
                {
                    throw new Exception("Password is wrong.");
                }

                return new Payload { AccountId = account.AccountId.ToString(), Role = account.Role };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public Account Profile(int accountId)
        {
            try
            {
                return context.Accounts.SingleOrDefault(acc => acc.AccountId == accountId && acc.Delete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Register(Account newAccount)
        {
            try
            {
                var account = context.Accounts.SingleOrDefault(acc => acc.Username == newAccount.Username);

                if (account != null) return false;

                newAccount.Password = Common.Hash(newAccount.Password);
                context.Accounts.Add(newAccount);

                return context.SaveChanges() == 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteAccount(int accountId)
        {
            try
            {
                var account = context.Accounts.SingleOrDefault(acc => acc.AccountId == accountId);
                if (account == null) return false;
                
                account.Delete = true;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateProfile(Account updateAccount)
        {
            try
            {
                var account = context.Accounts.SingleOrDefault(acc => acc.AccountId == updateAccount.AccountId && acc.Delete == false);

                if (account == null) return false;

                account.Name        =   Common.iifString(updateAccount.Name, account.Name);
                account.Avatar      =   Common.iifString(updateAccount.Avatar, account.Avatar);
                account.Password    = updateAccount.Password != null ? 
                                        Common.Hash(updateAccount.Password) : account.Password;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateRole(UpdateRole updateAccount)
        {
            try
            {
                var account = context.Accounts.SingleOrDefault(acc => acc.AccountId == updateAccount.AccountId && acc.Delete == false);

                if (account == null) return false;

                account.Role = updateAccount.Role;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateGameSpecs(Account updateAccount)
        {
            try
            {
                var account = context.Accounts.SingleOrDefault(acc => acc.AccountId == updateAccount.AccountId);

                if (account == null) return false; 
                

                account.Coin            = Common.iifInt     (updateAccount.Coin            , account.Coin);
                account.Experience      = Common.iifInt     (updateAccount.Experience      , account.Experience);  
                account.Level           = Common.iifInt     (updateAccount.Level           , account.Level);
                account.CheckPoint      = Common.iifString  (updateAccount.CheckPoint      , account.CheckPoint);
                account.BossKill        = Common.iifInt     (updateAccount.BossKill        , account.BossKill);
                account.AmountSlotSkill = Common.iifInt     (updateAccount.AmountSlotSkill , account.AmountSlotSkill);
                account.PointSkill      = Common.iifInt     (updateAccount.PointSkill      , account.PointSkill);

                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
