using AutoMapper;
using Shipment.Core;
using Shipment.Models.Parcel;
using System.Collections.Generic;

namespace Shipment.Business
{
    public class ParcelManager : IParcelManager
    {
        private readonly IRepository<Parcel> _repository;
        private readonly IMapper _mapper;

        public ParcelManager(IRepository<Parcel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public GetParcelDto Get(int id)
        {
            var parcel = _repository.Get(id);
            return _mapper.Map<GetParcelDto>(parcel);
        }

        public List<GetParcelDto> GetList()
        {
            var parcelList = _repository.GetAll();
            return _mapper.Map<List<GetParcelDto>>(parcelList);
        }

        public Parcel Save(SaveParcelDto parcelDto)
        {
            var parcel = _mapper.Map<Parcel>(parcelDto);
            var maxId = GetNextId();
            parcel.Id = maxId;
            _repository.Upsert(maxId, parcel);
            return parcel;
        }

        public void Update(UpdateParcelDto parcelDto)
        {
            var parcel = _mapper.Map<Parcel>(parcelDto);
            _repository.Upsert(parcel.Id, parcel);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        private int GetNextId()
        {
            return _repository.GetMaxKey() + 1;
        }
    }
}
