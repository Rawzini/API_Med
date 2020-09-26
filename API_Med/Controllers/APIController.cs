﻿using API_Med.Data;
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

        [HttpPut("bind/{evId}/{apId}")]
        public ActionResult<Event> BindAppointmentToEvent(int evId, int apId)
        {
            var eventToBind = _repository.FindEventById(evId);
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

            return eventToBind;
        }

        [HttpGet("event/{id}")]
        public ActionResult<Event> FindEventById(int id)
        {
            var APIItems = _repository.FindEventById(id);

            if (APIItems != null)
            {
                return Ok(APIItems);
            }
            return NotFound();
        }
    }
}
