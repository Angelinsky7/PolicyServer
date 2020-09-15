using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.QueryableExtensions;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class ResourceMappers {

        internal static IMapper Mapper { get; }

        static ResourceMappers() {
            Mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<ScopeMapperProfile>();
                cfg.AddProfile<ResourceMapperProfile>();
            }).CreateMapper();
        }

        public static Models.Resource ToModel(this Entities.Resource entity) => Mapper.Map<Models.Resource>(entity);
        public static Entities.Resource ToEntity(this Models.Resource model) => Mapper.Map<Entities.Resource>(model);
        public static IQueryable<Models.Resource> ToModel(this IQueryable<Entities.Resource> source) => source.ProjectTo<Models.Resource>(Mapper.ConfigurationProvider);
        public static Expression<Func<Entities.Resource, Models.Resource>> Projection => entity => Mapper.Map<Models.Resource>(entity);
        public static void UpdateEntity(this Models.Resource model, Entities.Resource entity) => Mapper.Map(model, entity);

    }

    public class ResourceMapperProfile : Profile {
        public ResourceMapperProfile() {

            #region Resource

            CreateMap<Models.Resource, Entities.Resource>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(p => p.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(p => p.Uris, opt => opt.MapFrom(src => src.Uris))
                .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
                .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .AfterMap((model, entity) => {
                    foreach (Entities.ResourceUri item in entity.Uris) { item.Resource = entity; }
                    foreach (Entities.MmResourceScope item in entity.Scopes) { item.Resource = entity; item.ResourceId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.Resource, Models.Resource>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(p => p.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(p => p.Uris, opt => opt.MapFrom(src => src.Uris))
                .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
                .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .PreserveReferences();

            #endregion

            #region Uri

            CreateMap<String, Entities.ResourceUri>()
                .ForMember(p => p.Uri, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Entities.ResourceUri, String>()
                .ConvertUsing(src => src.Uri);

            #endregion

            #region Scope

            CreateMap<Models.Scope, Entities.MmResourceScope>()
                .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Scope, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmResourceScope, Models.Scope>()
                //.ConvertUsing((src, dest, ctx) => ctx.Mapper.Map<Models.Scope>(src.Scope));
                //.ConstructUsing((p, ctx) => p.Scope.ToModel());
                //.ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Scope>(p.Scope));
                //TODO(demarco): beaware that we should not do that....
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Scope.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Scope.Name))
                .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.Scope.DisplayName))
                .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.Scope.IconUri));

            #endregion

        }
    }

}
