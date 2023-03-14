using AutoMapper;
using DataAccess;
using DataAccess.Models;
using DTOs.ItemDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AppServices.ItemRepo
{
    public class ItemRepositories
    {
        private readonly LienminhnhangiaContext context;
        IMapper mapper;

        public ItemRepositories(LienminhnhangiaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool AddItem(Item item)
        {
            try
            {
                if (context.Items.SingleOrDefault(it => it.ItemId.Equals(item.ItemId)) != null)
                {
                    throw new Exception($"ID vật phẩm {item.ItemId} đã tồn tại");
                }

                context.Items.Add(item);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateItem(Item updateItem)
        {
            try
            {
                context.Entry<Item>(updateItem).State = EntityState.Modified;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteItem(string itemId)
        {
            try
            {
                var item = context.Items.SingleOrDefault(i => i.ItemId.Equals(itemId));
                if (item == null) return false;

                item.Delete = true;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Item> GetAllItem(int? total)
        {
            try
            {
                if (total != null)
                {
                    return context.Items.Where(i => i.Delete == false).Take((int)total).ToList(); ;
                }
                return context.Items.Where(i => i.Delete == false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public List<ViewItem> GetItembyAccount(int accountID) 
        {
            try
            {
                var accountItems = context.AccountItems.Include(accountItem => accountItem.Item)
                                    .Where(accountItem => accountItem.AccountId == accountID 
                                                && accountItem.Delete == false)
                                    .Select(accountItem => accountItem)
                                    .ToList();

                List<ViewItem> viewItems = mapper.Map<List<AccountItem>, List<ViewItem>>(accountItems);


                return viewItems;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public Item GetItembyID(string itemId)
        {
            try
            {
                return context.Items.SingleOrDefault(item => item.ItemId.Equals(itemId) && item.Delete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

    }
}
