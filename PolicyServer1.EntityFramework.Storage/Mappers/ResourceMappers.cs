using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PolicyServer1.EntityFramework.Storage.Entities;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class ResourceMappers {

        //internal static IMapper Mapper { get; }

        //static ResourceMappers() {
        //    Mapper = new MapperConfiguration(cfg => {
        //        //cfg.AddProfile<ScopeMapperProfile>();
        //        cfg.AddProfile<ResourceMapperProfile>();
        //        cfg.AddExpressionMapping();
        //    }).CreateMapper();
        //}

        //public static Models.Resource ToModel(this Entities.Resource entity) => Mapper.Map<Models.Resource>(entity);
        //public static Entities.Resource ToEntity(this Models.Resource model) => Mapper.Map<Entities.Resource>(model);
        //public static IQueryable<Models.Resource> ToModel(this IQueryable<Entities.Resource> source) => source.ProjectTo<Models.Resource>(Mapper.ConfigurationProvider);
        //public static Expression<Func<Entities.Resource, Models.Resource>> Projection => entity => Mapper.Map<Models.Resource>(entity);
        //public static void UpdateEntity(this Models.Resource model, Entities.Resource entity) => Mapper.Map(model, entity);

        public static Models.Resource ToModel(this Entities.Resource entity) => Resource.ToModel(entity);
        public static Entities.Resource ToEntity(this Models.Resource model) => Resource.ToEntity(model);
        public static void UpdateEntity(this Models.Resource model, Entities.Resource entity) {
            Entities.Resource entityUpdate = Resource.ToEntity(model);
            if (entity.Id != entityUpdate.Id) { throw new ArgumentOutOfRangeException(nameof(Entities.Scope.Id)); }
            if (entity.Name != entityUpdate.Name) { entity.Name = entityUpdate.Name; }
            if (entity.DisplayName != entityUpdate.DisplayName) { entity.DisplayName = entityUpdate.DisplayName; }
            if (entity.IconUri != entityUpdate.IconUri) { entity.IconUri = entityUpdate.IconUri; }

            #region Uris
            foreach (Entities.ResourceUri uri in entity.Uris.Where(p => !entityUpdate.Uris.Any(a => a.Id == p.Id && a.ResourceId == p.ResourceId)).ToList()) {
                entity.Uris.Remove(uri);
            }
            foreach (Entities.ResourceUri uri in entity.Uris.Where(p => entityUpdate.Uris.Any(a => a.Id == p.Id && a.ResourceId == p.ResourceId && a.Uri != p.Uri)).ToList()) {
                uri.Uri = entityUpdate.Uris.Single(p => p.Id == uri.Id && p.ResourceId == uri.ResourceId).Uri;
            }
            foreach (Entities.ResourceUri uri in entityUpdate.Uris.Where(p => !entity.Uris.Any(a => a.Id == p.Id && a.ResourceId == p.ResourceId)).ToList()) {
                entity.Uris.Add(uri);
            }
            #endregion

            if (entity.Type != entityUpdate.Type) { entity.Type = entityUpdate.Type; }

            #region Scopes
            foreach (Entities.MmResourceScope scope in entity.Scopes.Where(p => !entityUpdate.Scopes.Any(a => a.ScopeId == p.ScopeId && a.ResourceId == p.ResourceId)).ToList()) {
                entity.Scopes.Remove(scope);
            }
            foreach (Entities.MmResourceScope scope in entityUpdate.Scopes.Where(p => !entity.Scopes.Any(a => a.ScopeId == p.ScopeId && a.ResourceId == p.ResourceId)).ToList()) {
                entity.Scopes.Add(scope);
            }
            #endregion
        }

        internal class Resource {
            internal static Expression<Func<Entities.Resource, Models.Resource>> Projection => entity => new Models.Resource {
                Id = entity.Id,
                Name = entity.Name,
                DisplayName = entity.DisplayName,
                IconUri = entity.IconUri,
                Uris = entity.Uris.Select(p => p.Uri).ToList(),
                Type = entity.Type,
                Scopes = entity.Scopes.Select(p => p.Scope.ToModel()).ToList()
            };
            internal static Expression<Func<Models.Resource, Entities.Resource>> Transform => model => new Entities.Resource {
                Id = model.Id,
                Name = model.Name,
                DisplayName = model.DisplayName,
                IconUri = model.IconUri,
                Uris = model.Uris.Select(p => new ResourceUri {
                    ResourceId = model.Id,
                    Uri = p
                }).ToList(),
                Type = model.Type,
                Scopes = model.Scopes.Select(p => new MmResourceScope {
                    ScopeId = p.Id,
                    ResourceId = model.Id
                }).ToList()
            };

            //TODO(demarco): We must use a cache for pergfoprtmance....
            internal static Entities.Resource ToEntity(Models.Resource entity) => Transform.Compile().Invoke(entity);
            internal static Models.Resource ToModel(Entities.Resource entity) => Projection.Compile().Invoke(entity);
        }

    }

    //public class ResourceMapperProfile : Profile {
    //    public ResourceMapperProfile() {

    //        #region Resource

    //        CreateMap<Models.Resource, Entities.Resource>()
    //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
    //            .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
    //            .ForMember(p => p.Type, opt => opt.MapFrom(src => src.Type))
    //            .ForMember(p => p.Uris, opt => opt.MapFrom(src => src.Uris))
    //            .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
    //            .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
    //            .AfterMap((model, entity) => {
    //                foreach (Entities.ResourceUri item in entity.Uris) { item.Resource = entity; }
    //                foreach (Entities.MmResourceScope item in entity.Scopes) { item.Resource = entity; item.ResourceId = entity.Id; }
    //            })
    //            .PreserveReferences();

    //        CreateMap<Entities.Resource, Models.Resource>()
    //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
    //            .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
    //            .ForMember(p => p.Type, opt => opt.MapFrom(src => src.Type))
    //            .ForMember(p => p.Uris, opt => opt.MapFrom(src => src.Uris))
    //            .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
    //            .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
    //            .PreserveReferences();

    //        #endregion

    //        #region Uri

    //        CreateMap<String, Entities.ResourceUri>()
    //            .ForMember(p => p.Uri, opt => opt.MapFrom(src => src))
    //            .PreserveReferences();

    //        CreateMap<Entities.ResourceUri, String>()
    //            .ConvertUsing(src => src.Uri);

    //        #endregion

    //        #region Scope

    //        CreateMap<Models.Scope, Entities.MmResourceScope>()
    //            .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(p => p.Scope, opt => opt.MapFrom(src => src));

    //        CreateMap<Entities.MmResourceScope, Models.Scope>()
    //            //.ConvertUsing((src, dest, ctx) => ctx.Mapper.Map<Models.Scope>(src.Scope));
    //            //.ConstructUsing((p, ctx) => p.Scope.ToModel());
    //            //.ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Scope>(p.Scope));
    //            //.ConvertUsing((src, dest) => ScopeMappers.Mapper.Map<Models.Scope>(src.Scope));
    //            //TODO(demarco): beaware that we should not do that....
    //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Scope.Id))
    //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Scope.Name))
    //            .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.Scope.DisplayName))
    //            .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.Scope.IconUri));

    //        #endregion

    //    }
}
