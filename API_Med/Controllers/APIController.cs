using API_Med.Data;
using API_Med.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Med.Controllers
{
    [Route("api")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IAPIRepo _repository;

        public APIController(IAPIRepo repository)
        {
            _repository = repository;
        }

        [HttpGet("closest/{id}")]
        public ActionResult<ClosestDateView> GetClosestDateView(int id)
        {
            var APIItems = _repository.GetClosestDateView(id);
            if (APIItems != null)
            {
                return Ok(APIItems);
            }
            return NotFound();
        }

        [HttpGet("id/{id}")]
        public ActionResult<Event> GetUnattachedAppointmentsById(int id)
        {
            var APIItems = _repository.GetUnattachedAppointmentsById(id);

            if (APIItems.Any())
            {
                return Ok(APIItems);
            }
            return NotFound();
        }

        [HttpGet("name/{name}")]
        public ActionResult<Event> GetUnattachedPatietnsByName(string name)
        {
            var APIItems = _repository.GetUnattachedAppointmentsByName(name);

            if (APIItems.Any())
            {
                return Ok(APIItems);
            }
            return NotFound();
        }
    }
}
