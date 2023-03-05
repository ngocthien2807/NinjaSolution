﻿namespace Obj_Common
{
    public static class ApiUrl
    {
        public const string BaseUrlCommon = "https://localhost:44374/api";
        public const string BaseUrlPlayer = "https://localhost:44323/api/Player";
        public const string BaseUrlAdmin = "https://localhost:44395/api/Admin";
    }

    public static class ApiController
    {
        public const string Ability = "Abilities";
        public const string Account = "Accounts";
        public const string Boss = "Bosses";
        public const string Character = "Characters";
        public const string Item = "Items";
        public const string Mission = "Missions";
        public const string Monster = "Monsters";
        public const string Skill = "Skills";
    }

    public static class Route
    {
        public static string Login = $"{ApiUrl.BaseUrlCommon}/{ApiController.Account}/Login";
        public static string Register = $"{ApiUrl.BaseUrlCommon}/{ApiController.Account}/Register";
        
        public static string Profile = $"{ApiUrl.BaseUrlPlayer}/{ApiController.Account}/Profile";


        public static string getAllAbility = $"{ApiUrl.BaseUrlCommon}/{ApiController.Ability}/GetAllAbility";
        public static string getByIDAbility = $"{ApiUrl.BaseUrlCommon}/{ApiController.Ability}/GetAbilitybyID/" + "{0}";

        public static string getAllBoss = $"{ApiUrl.BaseUrlCommon}/{ApiController.Boss}/GetAllBoss";
        public static string getByIDBoss = $"{ApiUrl.BaseUrlCommon}/{ApiController.Boss}/GetBossbyID/" + "{0}";

        public static string getAllCharacter = $"{ApiUrl.BaseUrlCommon}/{ApiController.Character}/GetAllCharacter";
        public static string getByIDCharacter = $"{ApiUrl.BaseUrlCommon}/{ApiController.Character}/GetCharacterbyID/" + "{0}";

        public static string getAllItem = $"{ApiUrl.BaseUrlCommon}/{ApiController.Item}/GetAllItem";
        public static string getByIDItem = $"{ApiUrl.BaseUrlCommon}/{ApiController.Item}/GetItembyID/" + "{0}";

        public static string getAllMonster = $"{ApiUrl.BaseUrlCommon}/{ApiController.Monster}/GetAllMonster";
        public static string getByIDMonster = $"{ApiUrl.BaseUrlCommon}/{ApiController.Monster}/GetMonsterbyID/" + "{0}";
        
    }
}
