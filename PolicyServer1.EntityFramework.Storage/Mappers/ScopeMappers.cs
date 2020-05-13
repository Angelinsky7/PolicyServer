using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class ScopeMappers {

        internal static IMapper Mapper { get; }

        static ScopeMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ScopeMapperProfile>()).CreateMapper();

        public static Models.Scope ToModel(this Entities.Scope entity) => Mapper.Map<Models.Scope>(entity);
        public static Entities.Scope ToEntity(this Models.Scope model) => Mapper.Map<Entities.Scope>(model);
        public static IQueryable<Models.Scope> ToModel(this IQueryable<Entities.Scope> source) => source.ProjectTo<Models.Scope>(Mapper.ConfigurationProvider);
        public static void UpdateEntity(this Models.Scope model, Entities.Scope entity) => Mapper.Map(model, entity);
    }

    public class ScopeMapperProfile : Profile {
        public ScopeMapperProfile() {

            #region Scope

            CreateMap<Models.Scope, Entities.Scope>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
                .PreserveReferences();

            CreateMap<Entities.Scope, Models.Scope>(MemberList.Destination)
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
                .PreserveReferences();

            #endregion

        }
    }

}
