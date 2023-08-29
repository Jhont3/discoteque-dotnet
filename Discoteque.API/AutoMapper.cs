using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Discoteque.Business;
using Discoteque.Data;
using Discoteque.Data.Dto;
using Discoteque.Data.Models;


namespace Discoteque.API
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap <Tour, TourDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ReverseMap();
        }
    }
}
