using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipment.Models.Parcel;

namespace Shipment.Business
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SaveParcelDto, Parcel>();
            CreateMap<UpdateParcelDto, Parcel>();
            CreateMap<Parcel, GetParcelDto>();
        }
    }
}
