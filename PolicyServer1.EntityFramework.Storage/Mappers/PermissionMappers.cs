using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PolicyServer1.EntityFramework.Storage.Entities;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class PermissionMappers {
        internal static IMapper Mapper { get; }

        static PermissionMappers() => Mapper = new MapperConfiguration(cfg => {
            cfg.AddProfile<PolicyMapperProfile>();
            cfg.AddProfile<ResourceMapperProfile>();
            cfg.AddProfile<PermissionMapperProfile>();
            cfg.AddProfile<ScopeMapperProfile>();
        }).CreateMapper();

        public static Models.Permission ToModel(this Entities.Permission entity) => Mapper.Map<Models.Permission>(entity);
        public static Entities.Permission ToEntity(this Models.Permission model) => Mapper.Map<Entities.Permission>(model);
        public static IQueryable<Models.Permission> ToModel(this IQueryable<Entities.Permission> source) => source.ProjectTo<Models.Permission>(Mapper.ConfigurationProvider);
        public static IQueryable<T> ToModel<T, W>(this IQueryable<W> source) where T : Models.Permission where W : Entities.Permission => source.ProjectTo<T>(Mapper.ConfigurationProvider);
        public static void UpdateEntity(this Models.Permission model, Entities.Permission entity) => Mapper.Map(model, entity);
    }
     
    public class PermissionMapperProfile : Profile {
        public PermissionMapperProfile() {

            #region Permission

            CreateMap<Models.Permission, Entities.Permission>()
                .Include<Models.ScopePermission, Entities.ScopePermission>()
                .Include<Models.ResourcePermission, Entities.ResourcePermission>()

                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
                .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
                .AfterMap((model, entity) => {
                    foreach (Entities.MmPermissionPolicy item in entity.Policies) { item.Permission = entity; item.PermissionId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.Permission, Models.Permission>()
                .Include<Entities.ScopePermission, Models.ScopePermission>()
                .Include<Entities.ResourcePermission, Models.ResourcePermission>()

                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
                .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
                .PreserveReferences();

            #endregion

            #region MmPermissionPolicy

            CreateMap<Models.Policy, Entities.MmPermissionPolicy>()
                .ForMember(p => p.PolicyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Policy, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmPermissionPolicy, Models.Policy>()
                .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Policy>(p.Policy));

            #endregion

            #region ScopePermission

            CreateMap<Models.ScopePermission, Entities.ScopePermission>()
                .IncludeBase<Models.Permission, Entities.Permission>()
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .AfterMap((model, entity) => {
                    foreach (MmScopePermissionScope item in entity.Scopes) { item.ScopePermission = entity; item.ScopePermissionId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.ScopePermission, Models.ScopePermission>()
                .IncludeBase<Entities.Permission, Models.Permission>()
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .PreserveReferences();

            #endregion

            #region Scope

            CreateMap<Models.Scope, Entities.MmScopePermissionScope>()
                .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Scope, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmScopePermissionScope, Models.Scope>()
                .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Scope>(p.Scope));

            #endregion

            #region ResourcePermission

            CreateMap<Models.ResourcePermission, Entities.ResourcePermission>()
                .IncludeBase<Models.Permission, Entities.Permission>()
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.ResouceType, opt => opt.MapFrom(src => src.ResouceType))
                .PreserveReferences();

            CreateMap<Entities.ResourcePermission, Models.ResourcePermission>()
                .IncludeBase<Entities.Permission, Models.Permission>()
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.ResouceType, opt => opt.MapFrom(src => src.ResouceType))
                .PreserveReferences();

            #endregion

        }
    }
}
