using API_Med.Data;
using API_Med.Dtos;
using API_Med.Dtos.Appointment;
using API_Med.Dtos.Event;
using API_Med.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly IMapper _mapper;

        public APIController(IAPIRepo repository, 
            IMapper mapper,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet]
        public IActionResult GetAllRoutes()
        {
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Select(x => new {
                Action = x.RouteValues["Action"],
                Controller = x.RouteValues["Controller"],
                Name = x.AttributeRouteInfo.Name,
                Template = x.AttributeRouteInfo.Template
            }).ToList();
            return Ok(routes);
        }

        [HttpGet("closest/{id}")]
        public ActionResult<ClosestDateViewReadDto> GetClosestSuitableDate(int id)
        {
            var APIItems = _repository.GetClosestSuitableDate(id);
            if (APIItems != null)
            {
                return Ok(_mapper.Map<ClosestDateViewReadDto>(APIItems));
            }
            return NotFound();
        }

        [HttpGet("id/{id}")]
        public ActionResult<IEnumerable<AppointmentReadDto>> GetUnattachedAppointmentsById(int id)
        {
            var APIItems = _repository.GetUnattachedAppointmentsById(id);

            if (APIItems.Any())
            {
                return Ok(_mapper.Map<IEnumerable<AppointmentReadDto>>(APIItems));
            }
            return NotFound();
        }

        [HttpGet("name/{name}")]
        public ActionResult<AppointmentReadDto> GetUnattachedAppointmentsByName(string name)
        {
            var APIItems = _repository.GetUnattachedAppointmentsByName(name);

            if (APIItems.Any())
            {
                return Ok(_mapper.Map<AppointmentReadDto>(APIItems));
            }
            return NotFound();
        }

        [HttpPut("bind/{evId}/{apId}")]
        public ActionResult<EventReadDto> BindAppointmentToEvent(int evId, int apId)
        {
            var eventToBind = _repository.GetEventById(evId);
            var appointmentToBind = _repository.FindAppointmentById(apId);

            if (eventToBind == null || appointmentToBind == null)
            {
                return NotFound();
            }
            if (eventToBind.ServiceId != appointmentToBind.ServiceId)
            { 
                return ValidationProblem();
            }
            eventToBind.AppointmentId = apId;

            _repository.BindAppointmentToEvent(eventToBind);
            _repository.SaveChanges();

            return _mapper.Map<EventReadDto>(eventToBind);
        }

        [HttpGet("event/{id}")]
        public ActionResult<EventReadDto> GetEventById(int id)
        {
            var APIItems = _repository.GetEventById(id);

            if (APIItems != null)
            {
                return Ok(_mapper.Map<EventReadDto>(APIItems));
            }
            return NotFound();
        }
    }
}
