﻿using AutoMapper;
using System;
using WasteProducts.DataAccess.Common.Models.DonationManagment;
using WasteProducts.Logic.Common.Models.DonationManagment;

namespace WasteProducts.Logic.Mappings
{
    class DonorProfile : Profile
    {
        public DonorProfile()
        {
            CreateMap<Donor, DonorDB>()
                .ForMember(m => m.CreatedOn, opt => opt.UseValue(DateTime.UtcNow))
                .ForMember(m => m.ModifiedOn, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}