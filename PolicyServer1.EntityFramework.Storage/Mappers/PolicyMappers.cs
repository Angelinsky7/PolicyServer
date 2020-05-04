#define DEBUG_MAPPING

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

            CreateMap<Models.Policy, Entities.Policy>()
                .Include<Models.RolePolicy, Entities.RolePolicy>()
                .Include<Models.TimePolicy, Entities.TimePolicy>()

                //TODO(demarco): Missing all the others configurations
                //.Include<Models.ClientPolicy, Entities.ClientPolicy>()
                //.Include<Models.UserPolicy, Entities.UserPolicy>()
                //.Include<Models.GroupPolicy, Entities.GroupPolicy>()
                //.Include<Models.AggregatedPolicy, Entities.AggregatedPolicy>()

                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.Logic, opt => opt.MapFrom(src => src.Logic))
#if DEBUG_MAPPING
                .AfterMap((model, entity) => {
                    if (true) { }
                })
#endif
                .PreserveReferences();

            CreateMap<Models.RolePolicy, Entities.RolePolicy>()
                .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
                .AfterMap((model, entity) => {
                    foreach (Entities.MmRolePolicyRole item in entity.Roles) { item.RolePolicy = entity; item.RolePolicyId = entity.Id; }
#if DEBUG_MAPPING
                    if(true){}
#endif
                })
                .PreserveReferences();

            CreateMap<Models.Role, Entities.MmRolePolicyRole>()
                .ForMember(p => p.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Role, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Models.TimePolicy, Entities.TimePolicy>()
                .ForMember(p => p.NotBefore, opt => opt.MapFrom(src => src.NotBefore))
                .ForMember(p => p.NotOnOrAfter, opt => opt.MapFrom(src => src.NotOnOrAfter))
                .ForMember(p => p.DayOfMonth, opt => opt.MapFrom(src => src.DayOfMonth))
                .ForMember(p => p.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(p => p.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(p => p.Hour, opt => opt.MapFrom(src => src.Hour))
                .ForMember(p => p.Minute, opt => opt.MapFrom(src => src.Minute))
#if DEBUG_MAPPING
                .AfterMap((model, entity) => {
                    if(true){}
                })
#endif
                .PreserveReferences();

            CreateMap<Models.TimePolicyRange, Entities.TimePolicyRange>()
                .ForMember(p => p.From, opt => opt.MapFrom(src => src.From))
                .ForMember(p => p.To, opt => opt.MapFrom(src => src.To))
                .PreserveReferences();

            //TODO(demarco): Missing all the others configurations

        }
    }
}
