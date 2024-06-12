using AutoMapper;
using Food.Application.Admin.Dtos.Ordens;
using Food.Domain.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.DetalleOrdens.Profiles
{
    public class DetalleOrdenProfile: Profile
    {
        public DetalleOrdenProfile()
        {
             CreateMap<DetalleOrden, DetalleOrdenDto>().ReverseMap();
            CreateMap<DetalleOrden, DetalleOrdenListDto>().ReverseMap();
            
        }
    }

}
