using Shipment.Models.Parcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Business
{
    public interface IParcelManager
    {
        Task<GetParcelDto> Get(int id);
        Task<List<GetParcelDto>> GetList();
        Task<Parcel> Save(SaveParcelDto parcelDto);
        Task Update(UpdateParcelDto parcelDto);
        Task Delete(int id);
    }
}
