using AutoMapper;
using Shipment.Models.Parcel;

namespace Shipment.Common
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
