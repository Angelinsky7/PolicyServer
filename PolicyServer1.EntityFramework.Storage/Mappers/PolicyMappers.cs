using AutoMapper;
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

        public static Entities.Policy ToEntity(this Models.Policy model, Entities.Policy entity) {
            return Mapper.Map(model, entity);
        }
    }

    public class PolicyMapperProfile : Profile {
        public PolicyMapperProfile() {

            CreateMap<Entities.Policy, Models.Policy>()
                .ReverseMap();

            //CreateMap<Entities.ClientScope, String>()
            //    .ConstructUsing(src => src.Scope)
            //    .ReverseMap()
            //    .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

            //CreateMap<Entities.ClientSecret, Models.Secret>(MemberList.Destination)
            //    .ReverseMap();

        }
    }
}
