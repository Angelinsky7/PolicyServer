using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class ScopeMappers {

        internal static IMapper Mapper { get; }

        static ScopeMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ScopeMapperProfile>()).CreateMapper();

        public static Models.Scope ToModel(this Entities.Scope entity) => Mapper.Map<Models.Scope>(entity);
        public static Entities.Scope ToEntity(this Models.Scope model) => Mapper.Map<Entities.Scope>(model);

    }

    public class ScopeMapperProfile : Profile {
        public ScopeMapperProfile() {

            CreateMap<Entities.Scope, Models.Scope>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri));

        }
    }

}
