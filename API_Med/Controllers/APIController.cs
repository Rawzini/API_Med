using API_Med.Data;
using API_Med.Dtos;
using API_Med.Dtos.Appointment;
using API_Med.Dtos.Event;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_Med.Controllers
{
    [Route("api")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IAPIRepo _repository;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public APIController(IAPIRepo repository, 
            IMapper mapper,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
            ILogger<APIController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _logger = logger;
        }

        //GET api/
        //Возвращает список всех доступных маршрутов, которые обрабатывает контроллер
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

        //GET api/id/{id}
        //Возвращает список всех назначений пациента (по id), не привязанных к расписанию 
        [HttpGet("id/{id}")]
        public ActionResult<IEnumerable<AppointmentReadDto>> GetUnattachedAppointmentsById(int id)
        {
            _logger.LogInformation("Start : Getting unattached appointments by id: {id}", id);

            var APIItems = _repository.GetUnattachedAppointmentsById(id);

            if (APIItems.Any())
            {
                _logger.LogInformation("Completed : Item details for {id}", id, APIItems);
                return Ok(_mapper.Map<IEnumerable<AppointmentReadDto>>(APIItems));
            }

            _logger.LogInformation("Not found");
            return NotFound();
        }

        //GET api/name/{name}
        //Возвращает список всех назначений пациента (по имени), не привязанных к расписанию
        [HttpGet("name/{name}")]
        public ActionResult<AppointmentReadDto> GetUnattachedAppointmentsByName(string name)
        {
            _logger.LogInformation("Start : Getting unattached appointments by name: {name}", HttpUtility.UrlDecode(name));

            var APIItems = _repository.GetUnattachedAppointmentsByName(name);

            if (APIItems.Any())
            {
                _logger.LogInformation("Completed : Item details for name: {name}", name, APIItems);
                return Ok(_mapper.Map<AppointmentReadDto>(APIItems));
            }
            _logger.LogInformation("Not found");
            return NotFound();
        }

        //GET api/closest/{id}
        //Возвращает дату в которую пациент может пройти все назначенные процедуры и список всех доступных в этот день процедур
        [HttpGet("closest/{id}")]
        public ActionResult<ClosestDateViewReadDto> GetClosestSuitableDate(int id)
        {
            _logger.LogInformation("Start : Getting closest suitable date by id: {id}", id);

            var APIItems = _repository.GetClosestSuitableDate(id);

            if (APIItems != null)
            {
                _logger.LogInformation("Completed : Item details for patient id: {id}", id, APIItems);
                return Ok(_mapper.Map<ClosestDateViewReadDto>(APIItems));
            }

            _logger.LogInformation("Not found");
            return NotFound();
        }

        //PUT api/bind/{evId}/{apId}
        //Привязывает назначение пациента к расписанию
        [HttpPut("bind/{evId}/{apId}")]
        public ActionResult<EventReadDto> BindAppointmentToEvent(int evId, int apId)
        {
            _logger.LogInformation("Start : Trying to bind appointment with id: {apId} to event with id: {evId}", apId, evId);

            var eventToBind = _repository.GetEventById(evId);
            var appointmentToBind = _repository.GetAppointmentById(apId);

            if (eventToBind == null || appointmentToBind == null)
            {
                _logger.LogInformation("Not found");
                return NotFound();
            }
            if (eventToBind.ServiceId != appointmentToBind.ServiceId)
            {
                _logger.LogInformation("Appointment.ServiceId don't match Event.ServiceId");
                return ValidationProblem();
            }
            
            eventToBind.AppointmentId = apId;
            _repository.BindAppointmentToEvent(eventToBind);
            _repository.SaveChanges();

            _logger.LogInformation("Completed : Appointment with id: {apId} binded to event with id: {evId}", apId, evId, eventToBind);
            return Ok(_mapper.Map<EventReadDto>(eventToBind));
        }

        //GET api/event/{id}
        //Возвращает ячейку расписания (по id)
        [HttpGet("event/{id}")]
        public ActionResult<EventReadDto> GetEventById(int id)
        {
            _logger.LogInformation("Start : Getting Event by id: {id}", id);

            var APIItems = _repository.GetEventById(id);

            if (APIItems != null)
            {
                _logger.LogInformation("Completed : Item details for name: {id}", id, APIItems);
                return Ok(_mapper.Map<EventReadDto>(APIItems));
            }

            _logger.LogInformation("Not found");
            return NotFound();
        }
    }
}
