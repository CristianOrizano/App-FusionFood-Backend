using AutoMapper;
using Food.Domain.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Application.Admin.Dtos.Usuarios.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() {

            CreateMap<Usuario,UsuarioDto>();
            CreateMap<Usuario, UsuarioSaveDto>().ReverseMap();
            CreateMap<Usuario, UserSecurityDto>().ReverseMap();

        } 

    }
}
