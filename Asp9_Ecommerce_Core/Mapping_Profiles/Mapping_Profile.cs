using Asp9_Ecommerce_Core.DTOs.ItemsDTO;
using Asp9_Ecommerce_Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Mapping_Profiles
{
    public class Mapping_Profile : Profile
    {
        public Mapping_Profile()
        {
            // ماب بين الـ Items و Item_dto
            CreateMap<Items, Item_dto>()
                .ForMember(dest => dest.ItemsUnits, opt => opt.MapFrom(src => src.ItemsUnits.Select(unit => unit.UnitCode).ToList()))
                .ForMember(dest => dest.Stores, opt => opt.MapFrom(src => src.ItemsStores.Select(store => store.Stores.name).ToList()));
        }
    }
}
