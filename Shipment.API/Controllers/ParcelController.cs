using System.Collections.Generic;
using System.Threading.Tasks;
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

        // GET: api/<ParcelController>
        [HttpGet]
        public Task<List<GetParcelDto>> Get()
        {
            return _parcelManager.GetList();
        }

        // GET api/<ParcelController>/5
        [HttpGet("{id}")]
        public Task<GetParcelDto> Get(int id)
        {
            return _parcelManager.Get(id);
        }

        // POST api/<ParcelController>
        [HttpPost]
        public Task<Parcel> Post([FromBody] SaveParcelDto parcel)
        {
            return _parcelManager.Save(parcel);
        }

        // PUT api/<ParcelController>/5
        [HttpPut]
        public Task Put([FromBody] UpdateParcelDto parcel)
        {
            return _parcelManager.Update(parcel);
        }

        // DELETE api/<ParcelController>/5
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return _parcelManager.Delete(id);
        }
    }
}
