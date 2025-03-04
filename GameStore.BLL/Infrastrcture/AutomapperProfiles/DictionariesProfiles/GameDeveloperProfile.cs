﻿using AutoMapper;
using GameStore.BLL.DTO.Dictionaries;
using GameStore.DAL.Entities.Dictionaries;

namespace GameStore.BLL.Infrastrcture.AutomapperProfiles.DictionariesProfiles
{
    public class GameDeveloperProfile : Profile
    {
        public GameDeveloperProfile() 
        {
            CreateMap<GameDeveloper, GameDeveloperDTO>().ReverseMap();
        }
    }
}
