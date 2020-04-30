using AutoMapper;
using PolicyServer1.EntityFramework.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class PolicyMappers {
        internal static IMapper Mapper { get; }

        static PolicyMappers() {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PolicyMapperProfile>()).CreateMapper();
        }

        public static Models.Policy ToModel(this Entities.Policy entity) {
            return Mapper.Map<Models.Policy>(entity);
        }
        public static Entities.Policy ToEntity(this Models.Policy model) {
            return Mapper.Map<Entities.Policy>(model);
        }

        //public static Entities.Policy ToEntity(this Models.Policy model, Entities.Policy entity) {
        //    return Mapper.Map(model, entity);
        //}
    }

    public class PolicyMapperProfile : Profile {
        public PolicyMapperProfile() {

            CreateMap<Entities.Policy, Models.Policy>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.Logic, opt => opt.MapFrom(src => src.Logic))
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.Logic, opt => opt.MapFrom(src => src.Logic));

            CreateMap<Entities.RolePolicy, Models.RolePolicy>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Base.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Base.Description))
                .ForMember(p => p.Logic, opt => opt.MapFrom(src => src.Base.Logic))
                .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Base, opt => opt.MapFrom(src => new Policy {
                    Id = src.Id,
                    Name = src.Name,
                    Description = src.Description,
                    Logic = src.Logic
                }))
                .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
                .AfterMap((model, entity) => {
                    foreach (Entities.MmRolePolicyRole item in entity.Roles) { item.RolePolicy = entity; item.RolePolicyId = entity.Id; }
                });

            CreateMap<Entities.MmRolePolicyRole, Models.Role>()
                .ReverseMap()
                .ForMember(p => p.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Role, opt => opt.MapFrom(src => src.ToEntity()));

            CreateMap<Entities.TimePolicy, Models.TimePolicy>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Base.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Base.Description))
                .ForMember(p => p.Logic, opt => opt.MapFrom(src => src.Base.Logic))
                .ForMember(p => p.NotBefore, opt => opt.MapFrom(src => src.NotBefore))
                .ForMember(p => p.NotOnOrAfter, opt => opt.MapFrom(src => src.NotOnOrAfter))
                .ForMember(p => p.DayOfMonth, opt => opt.MapFrom(src => src.DayOfMonth))
                .ForMember(p => p.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(p => p.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(p => p.Hour, opt => opt.MapFrom(src => src.Hour))
                .ForMember(p => p.Minute, opt => opt.MapFrom(src => src.Minute))
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Base, opt => opt.MapFrom(src => new Policy {
                    Id = src.Id,
                    Name = src.Name,
                    Description = src.Description,
                    Logic = src.Logic
                }))
                .ForMember(p => p.NotBefore, opt => opt.MapFrom(src => src.NotBefore))
                .ForMember(p => p.NotOnOrAfter, opt => opt.MapFrom(src => src.NotOnOrAfter))
                .ForMember(p => p.DayOfMonth, opt => opt.MapFrom(src => src.DayOfMonth))
                .ForMember(p => p.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(p => p.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(p => p.Hour, opt => opt.MapFrom(src => src.Hour))
                .ForMember(p => p.Minute, opt => opt.MapFrom(src => src.Minute));

            CreateMap<Entities.TimePolicyRange, Models.TimePolicyRange>()
                .ForMember(p => p.From, opt => opt.MapFrom(src => src.From))
                .ForMember(p => p.To, opt => opt.MapFrom(src => src.To))
                .ReverseMap()
                .ForMember(p => p.From, opt => opt.MapFrom(src => src.From))
                .ForMember(p => p.To, opt => opt.MapFrom(src => src.To));

        }
    }
}
