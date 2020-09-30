using API_Med.Dtos;
using API_Med.Dtos.Appointment;
using API_Med.Dtos.Event;
using API_Med.Dtos.Patient;
using API_Med.Dtos.Service;
using API_Med.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Med.Profiles
{
    public class APIProfile : Profile
    {
        public APIProfile()
        {
            CreateMap<Appointment, AppointmentReadDto>().IncludeMembers(m => m.Service, m => m.Patient);
            CreateMap<Service, AppointmentReadDto>(MemberList.None).ForMember(m => m.ServiceId, o => o.MapFrom(x => x.Id));
            CreateMap<Patient, AppointmentReadDto>(MemberList.None).ForMember(m => m.PatientId, o => o.MapFrom(x => x.Id));

            CreateMap<Event, EventReadDto>().IncludeMembers(m => m.Appointment, m => m.Service);
            CreateMap<Appointment, EventReadDto>(MemberList.None).ForMember(m => m.AppointmentId, o => o.MapFrom(x => x.Id));
            CreateMap<Service, EventReadDto>(MemberList.None).ForMember(m => m.ServiceId, o => o.MapFrom(x => x.Id));

            CreateMap<Patient, PatientReadDto>();
            CreateMap<Service, ServiceReadDto>();

            CreateMap<ClosestDateView, ClosestDateViewReadDto>().ForMember(m => m.Events, o => o.MapFrom(e => e.Events));
        }
    }
}
