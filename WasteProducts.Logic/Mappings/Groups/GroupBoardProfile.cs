﻿using AutoMapper;
using System;
using WasteProducts.DataAccess.Common.Models;
using WasteProducts.Logic.Common.Models;

namespace WasteProducts.Logic.Mappings
{
    public class GroupBoardProfile : Profile
    {
        public GroupBoardProfile()
        {
            CreateMap<GroupBoard, GroupBoardDB>()
                .ForMember(x => x.TimeCreate, y => y.Ignore())
                .ForMember(x => x.TimeDelete, y => y.Ignore())
                .ForMember(x => x.Bool, y => y.Ignore())
                .ForMember(x=>x.GroupDBId, y=>y.MapFrom(z=>z.GroupId))
                .ForMember(x=>x.GroupProductDBs, y=>y.MapFrom(z=>z.GroupProducts))
                .ReverseMap();
        }
    }
}
