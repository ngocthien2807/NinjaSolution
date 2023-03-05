using AutoMapper;
using DataAccess.Models;
using DTOs.AccountDTOs;

namespace AutoMapperServices.AccountMapper
{
    public class AccountProfiles : Profile
    {
        public AccountProfiles() {

            CreateMap<Register, Account>();

            CreateMap<UpdateProfile, Account>();

            CreateMap<UpdateGameSpecs, Account>();

            CreateMap<Account, AccountProfile>();

            CreateMap<Account, AccountHeader>();
        }
    }
}
