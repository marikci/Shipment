using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Shipment.Business;
using Shipment.Models.Parcel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shipment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelManager _parcelManager;
        public ParcelController(IParcelManager parcelManager)
        {
            _parcelManager = parcelManager;
        }

        [HttpGet]
        public List<GetParcelDto> Get()
        {
            return _parcelManager.GetList();
        }

        [HttpGet("{id}")]
        public GetParcelDto Get(int id)
        {
            return _parcelManager.Get(id);
        }

        [HttpPost]
        public Parcel Post([FromBody] SaveParcelDto parcel)
        {
            return _parcelManager.Save(parcel);
        }

        [HttpPut]
        public void Put([FromBody] UpdateParcelDto parcel)
        {
            _parcelManager.Update(parcel);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _parcelManager.Delete(id);
        }
    }
}