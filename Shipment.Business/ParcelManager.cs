using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipment.Models;
using Shipment.Models.Parcel;
using AutoMapper;
using Shipment.Common;

namespace Shipment.Business
{
    public class ParcelManager: IParcelManager
    {
        private readonly ILogger<ParcelManager> _logger;
        private readonly IMemoryManager _memoryManager;
        private readonly IMapper _mapper;

        public ParcelManager(IMemoryManager memoryManager, ILogger<ParcelManager> logger, IMapper mapper)
        {
            _logger = logger;
            _memoryManager = memoryManager;
            _mapper = mapper;
        }

        public async Task<GetParcelDto> Get(int id)
        {
            try
            {
               var parcel = await GetParcel(id);
               return _mapper.Map<GetParcelDto>(parcel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET_PARCEL_ERROR");
                throw;
            }
        }

        public async Task<List<GetParcelDto>> GetList()
        {
            try
            {
                var parcelList = await GetParcelList();
                return _mapper.Map<List<GetParcelDto>>(parcelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET_PARCEL_ERROR");
                throw;
            }
        }

        public async Task<Parcel> Save(SaveParcelDto parcelDto)
        {
            try
            {
                var parcel = _mapper.Map<Parcel>(parcelDto);
                return await SaveParcel(parcel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SAVE_PARCEL_ERROR");
                throw;
            }
        }

        public async Task Update(UpdateParcelDto parcelDto)
        {
            try
            {
                var parcel = _mapper.Map<Parcel>(parcelDto);
                await UpdateParcel(parcel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UPDATE_PARCEL_ERROR");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await DeleteParcel(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE_PARCEL_ERROR");
                throw;
            }
        }

        private async Task<Parcel> GetParcel(int id)
        {
            List<Parcel> parcelList = await GetParcelList();
            if (parcelList == null) return null;
            return parcelList.FirstOrDefault(x => x.Id == id);
        }

        private async Task<List<Parcel>> GetParcelList()
        {
            var list= await _memoryManager.GetAsync<List<Parcel>>(ConstVariables.PARCEL_LIST);
            if (list == null) return new List<Parcel>();
            return list;
        }

        private async Task DeleteParcel(int id)
        {
            var parcelList = await GetParcelList();
            parcelList.RemoveAll(x => x.Id == id);
            await  _memoryManager.SetAsync(ConstVariables.PARCEL_LIST, parcelList);
        }

        private async Task<Parcel> SaveParcel(Parcel parcel)
        {
            var parcelList = await GetParcelList();
            parcel.Id = await GetMaxIdAsync();
            parcelList.Add(parcel);
            await _memoryManager.SetAsync(ConstVariables.PARCEL_LIST, parcelList);
            return parcel;
        }

        private async Task<int> GetMaxIdAsync()
        {
            var parcelList = await GetParcelList();

            if (!parcelList.Any())
            {
                return 1;
            }
     
            return parcelList.Max(x => x.Id) + 1;
        }

        private async Task UpdateParcel(Parcel parcel)
        {
            var parcelList = await GetParcelList();
            var index = parcelList.FindIndex(x => x.Id == parcel.Id);
            if (index >-1)
            {
                parcelList[index] = parcel;
            }
            await _memoryManager.SetAsync(ConstVariables.PARCEL_LIST, parcelList);
        }
    }
}
