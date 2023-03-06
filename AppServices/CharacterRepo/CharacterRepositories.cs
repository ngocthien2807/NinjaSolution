﻿using DataAccess.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DTOs.CharacterDTOs;
using AppServices.SkillRepo;

namespace AppServices.CharacterRepo
{
    public class CharacterRepositories
    {
        private readonly LienminhnhangiaContext context;
        SkillRepositories SkillManager;

        IMapper mapper;

        public CharacterRepositories(LienminhnhangiaContext context, IMapper mapper, SkillRepositories skillManager)
        {
            this.context = context;
            this.mapper = mapper;
            SkillManager = skillManager;
        }

        public bool AddCharacter(Character character)
        {
            try
            {
                context.Characters.Add(character);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateCharacter(Character updateCharacter)
        {
            try
            {
                context.Entry<Character>(updateCharacter).State = EntityState.Modified;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteCharacter(string ID)
        {
            try
            {
                var character = context.Characters.SingleOrDefault(cha => cha.CharacterId.Equals(ID));
                if (character == null) return false;
                character.Delete = true;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ViewCharacter> GetAllCharacter(int? total)
        {
            try
            {
                var characters = context.Characters.Where(cha => cha.Delete == false).ToList();
                var viewCharacters = mapper.Map<List<Character>, List<ViewCharacter>>(characters);
               
                if (total != null)
                {
                    viewCharacters =viewCharacters.Take((int)total).ToList(); ;
                }

                return viewCharacters;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ViewCharacterInfo GetCharacterbyID(string ID)
        {
            try
            {
                var character = context.Characters.SingleOrDefault(cha => cha.CharacterId.Equals(ID) && cha.Delete == false);
                var viewCharacter = mapper.Map<Character, ViewCharacterInfo>(character);

                viewCharacter.Skills = SkillManager.GetSkillbyCharacter(viewCharacter.CharacterId);

                return viewCharacter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ViewCharacter> GetAllCharacterbyAccount(int accountID)
        {
            try
            {
                var accountCharacters = context.AccountCharacters
                                        .Include(accountCharacter => accountCharacter.Character)
                                        .Where(accountCharacter => accountCharacter.AccountId == accountID
                                                                && accountCharacter.Delete == false)
                                        .Select(accountCharacter => accountCharacter)
                                        .ToList();

                var viewCharacters  = mapper.Map<List<AccountCharacter>, List<ViewCharacter>>(accountCharacters);

                return viewCharacters;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public ViewCharacterAccountInfo GetCharacterbyAccount(int accountID, string characterID)
        {
            try
            {
                var accountCharacter = context.AccountCharacters
                                        .Include(accountCharacter => accountCharacter.Character)
                                        .SingleOrDefault(accountCharacter => accountCharacter.AccountId == accountID
                                                                && accountCharacter.CharacterId.Equals(characterID)
                                                                && accountCharacter.Delete == false);
                
                var viewCharacters = mapper.Map<AccountCharacter, ViewCharacterAccountInfo>(accountCharacter);
                viewCharacters.Skills = SkillManager.GetSkillbyAccount(accountID, characterID);

                return viewCharacters;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
