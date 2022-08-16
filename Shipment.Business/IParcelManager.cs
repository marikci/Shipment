using Shipment.Models.Parcel;
using System.Collections.Generic;

namespace Shipment.Business
{
    public interface IParcelManager
    {
        GetParcelDto Get(int id);
        List<GetParcelDto> GetList();
        Parcel Save(SaveParcelDto parcelDto);
        void Update(UpdateParcelDto parcelDto);
        void Delete(int id);
    }
}
