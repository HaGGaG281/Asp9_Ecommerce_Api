using Asp9_Ecommerce_Core.DTOs.ItemsDTO;
using Asp9_Ecommerce_Core.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Ecommerce_Core.Mapping_Profiles
{
    // MappingProfile.cs (معدل)
    public static class Mapping_Profile
    {
        private static readonly TypeAdapterConfig _config = new TypeAdapterConfig();

        static Mapping_Profile()
        {
            _config.NewConfig<Items, Item_dto>()
                   .Map(dest => dest.ItemsUnits, src => src.ItemsUnits.Select(unit => unit.UnitCode).ToList()) // هنا unit هو نوع يحتوي على UnitCode
                   .Map(dest => dest.Stores, src => src.ItemsStores.Select(store => store.Stores.name).ToList());
        }

        public static TypeAdapterConfig Config => _config;
    }




}
