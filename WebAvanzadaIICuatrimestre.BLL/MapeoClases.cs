using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAvanzadaIICuatrimestre.BLL
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<DAL.Entidades.Carro, Dtos.CarroDto>().ReverseMap();
            CreateMap<DAL.Entidades.Duenno, Dtos.DuennoDto>().ReverseMap();
            CreateMap<DAL.Entidades.Cliente, Dtos.ClienteDto>().ReverseMap();
            CreateMap<DAL.Entidades.Telefono, Dtos.TelefonoDto>().ReverseMap();
        }
    }
}
