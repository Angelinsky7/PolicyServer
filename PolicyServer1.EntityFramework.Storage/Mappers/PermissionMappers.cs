using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PolicyServer1.EntityFramework.Storage.Entities;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class PermissionMappers {
        //internal static IMapper Mapper { get; }

        //static PermissionMappers() => Mapper = new MapperConfiguration(cfg => {
        //    cfg.AddProfile<PolicyMapperProfile>();
        //    cfg.AddProfile<ResourceMapperProfile>();
        //    cfg.AddProfile<PermissionMapperProfile>();
        //    cfg.AddProfile<ScopeMapperProfile>();
        //}).CreateMapper();

        //public static Models.Permission ToModel(this Entities.Permission entity) => Mapper.Map<Models.Permission>(entity);
        //public static Entities.Permission ToEntity(this Models.Permission model) => Mapper.Map<Entities.Permission>(model);
        //public static IQueryable<Models.Permission> ToModel(this IQueryable<Entities.Permission> source) => source.ProjectTo<Models.Permission>(Mapper.ConfigurationProvider);
        //public static IQueryable<T> ToModel<T, W>(this IQueryable<W> source) where T : Models.Permission where W : Entities.Permission => source.ProjectTo<T>(Mapper.ConfigurationProvider);
        //public static Expression<Func<Entities.Permission, Models.Permission>> Projection => entity => Mapper.Map<Models.Permission>(entity);
        //public static void UpdateEntity(this Models.Permission model, Entities.Permission entity) => Mapper.Map(model, entity);

        public static Models.Permission ToModel(this Entities.Permission entity) => Permission.ToModel(entity);
        public static Entities.Permission ToEntity(this Models.Permission model) {
            Entities.Permission entity = Permission.ToEntity(model);
            foreach (Entities.MmPermissionPolicy item in entity.Policies) { item.Permission = entity; item.PermissionId = entity.Id; }
            return entity;
        }
        public static void UpdateEntity(this Models.Permission model, Entities.Permission entity) {
            Entities.Permission entityUpdate = Permission.ToEntity(model);
            if (entity.Id != entityUpdate.Id) { throw new ArgumentOutOfRangeException(nameof(Entities.Permission.Id)); }
            if (entity.Name != entityUpdate.Name) { entity.Name = entityUpdate.Name; }
            if (entity.Description != entityUpdate.Description) { entity.Description = entityUpdate.Description; }
            if (entity.DecisionStrategy != entityUpdate.DecisionStrategy) { entity.DecisionStrategy = entityUpdate.DecisionStrategy; }
            foreach (Entities.MmPermissionPolicy policy in entity.Policies.Where(p => !entityUpdate.Policies.Any(a => a.PermissionId == p.PermissionId && a.PolicyId == p.PolicyId)).ToList()) {
                entity.Policies.Remove(policy);
            }
            foreach (Entities.MmPermissionPolicy policy in entityUpdate.Policies.Where(p => !entity.Policies.Any(a => a.PermissionId == p.PermissionId && a.PolicyId == p.PolicyId)).ToList()) {
                entity.Policies.Add(policy);
            }

            if (entity is ScopePermission scopePermission) {
                if (scopePermission.ResourceId != (model as Models.ScopePermission).Resource.Id) {
                    scopePermission.ResourceId = (model as Models.ScopePermission).Resource.Id;
                    scopePermission.Resource = ResourceMappers.ToEntity((model as Models.ScopePermission).Resource);
                }
                foreach (Entities.MmScopePermissionScope scope in scopePermission.Scopes.Where(p => !(entityUpdate as Entities.ScopePermission).Scopes.Any(a => a.ScopePermissionId == p.ScopePermissionId && a.ScopeId == p.ScopeId)).ToList()) {
                    scopePermission.Scopes.Remove(scope);
                }
                foreach (Entities.MmScopePermissionScope scope in (entityUpdate as Entities.ScopePermission).Scopes.Where(p => !scopePermission.Scopes.Any(a => a.ScopePermissionId == p.ScopePermissionId && a.ScopeId == p.ScopeId)).ToList()) {
                    scopePermission.Scopes.Add(scope);
                }
            } else if (entity is ResourcePermission resourcePermission) {
                if (resourcePermission.ResourceId != (model as Models.ResourcePermission).Resource?.Id) {
                    resourcePermission.ResourceId = (model as Models.ResourcePermission).Resource?.Id;
                    resourcePermission.Resource = ResourceMappers.ToEntity((model as Models.ResourcePermission).Resource);
                }
                if (resourcePermission.ResouceType != (model as Models.ResourcePermission).ResouceType) {
                    resourcePermission.ResouceType = (model as Models.ResourcePermission).ResouceType;
                }
            }
        }

        internal class Permission {
            internal static Expression<Func<Entities.Permission, Models.Permission>> Projection => entity => (entity as ScopePermission) != null ? new Models.ScopePermission {
                Id = entity.Id,
                Name = entity.Name,
                DecisionStrategy = entity.DecisionStrategy,
                Description = entity.Description,
                Policies = new Collection<Models.Policy>(entity.Policies.Select(p => MmPermissionPolicy.ToModel(p)).ToList()),
                Resource = (entity as ScopePermission).Resource != null ? ResourceMappers.ToModel((entity as ScopePermission).Resource) : null,
                Scopes = new Collection<Models.Scope>((entity as ScopePermission).Scopes.Select(a => new Models.Scope {
                    Id = a.Scope.Id,
                    DisplayName = a.Scope.DisplayName,
                    Name = a.Scope.Name,
                    IconUri = a.Scope.IconUri
                }).ToList())
            } : (entity as ResourcePermission) != null ? (Models.Permission)new Models.ResourcePermission {
                Id = entity.Id,
                Name = entity.Name,
                DecisionStrategy = entity.DecisionStrategy,
                Description = entity.Description,
                Policies = new Collection<Models.Policy>(entity.Policies.Select(p => MmPermissionPolicy.ToModel(p)).ToList()),
                ResouceType = (entity as ResourcePermission).ResouceType,
                Resource = (entity as ResourcePermission).Resource != null ? ResourceMappers.ToModel((entity as ResourcePermission).Resource) : null,
            } : null;
            internal static Expression<Func<Models.Permission, Entities.Permission>> Transform => model => (model as Models.ScopePermission) != null ? new Entities.ScopePermission {
                Id = model.Id,
                Name = model.Name,
                DecisionStrategy = model.DecisionStrategy,
                Description = model.Description,
                Policies = new Collection<Entities.MmPermissionPolicy>(model.Policies.Select(p => MmPermissionPolicy.ToEntity(p)).ToList()),
                ResourceId = (model as Models.ScopePermission).Resource.Id,
                Resource = ResourceMappers.ToEntity((model as Models.ScopePermission).Resource),
                Scopes = new Collection<Entities.MmScopePermissionScope>((model as Models.ScopePermission).Scopes.Select(a => new Entities.MmScopePermissionScope {
                    ScopeId = a.Id,
                    ScopePermissionId = model.Id,
                }).ToList())
            } : (model as Models.ResourcePermission) != null ? (Entities.Permission)new Entities.ResourcePermission {
                Id = model.Id,
                Name = model.Name,
                DecisionStrategy = model.DecisionStrategy,
                Description = model.Description,
                Policies = new Collection<Entities.MmPermissionPolicy>(model.Policies.Select(p => MmPermissionPolicy.ToEntity(p)).ToList()),
                Resource = ResourceMappers.ToEntity((model as Models.ResourcePermission).Resource),
                ResouceType = (model as Models.ResourcePermission).ResouceType
            } : null;
            internal static Entities.Permission ToEntity(Models.Permission entity) => Transform.Compile().Invoke(entity);
            internal static Models.Permission ToModel(Entities.Permission entity) => Projection.Compile().Invoke(entity);
        }

        internal static class MmPermissionPolicy {
            internal static Expression<Func<Entities.MmPermissionPolicy, Models.Policy>> Projection => entity => new Models.Policy {
                Id = entity.Policy.Id,
                Description = entity.Policy.Description,
                Name = entity.Policy.Name,
                Logic = entity.Policy.Logic
            };
            internal static Expression<Func<Models.Policy, Entities.MmPermissionPolicy>> Transform => model => new Entities.MmPermissionPolicy {
                PolicyId = model.Id,
                Policy = PolicyMappers.ToEntity(model)
            };

            internal static Entities.MmPermissionPolicy ToEntity(Models.Policy entity) => Transform.Compile().Invoke(entity);
            //[ReplaceWithExpression(PropertyName = nameof(Projection))]
            internal static Models.Policy ToModel(Entities.MmPermissionPolicy entity) => Projection.Compile().Invoke(entity);
        }

        //public class PermissionMapperProfile : Profile {
        //    public PermissionMapperProfile() {

        //        #region Permission

        //        CreateMap<Models.Permission, Entities.Permission>()
        //            .Include<Models.ScopePermission, Entities.ScopePermission>()
        //            .Include<Models.ResourcePermission, Entities.ResourcePermission>()

        //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
        //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
        //            .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
        //            .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
        //            .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
        //            .AfterMap((model, entity) => {
        //                foreach (Entities.MmPermissionPolicy item in entity.Policies) { item.Permission = entity; item.PermissionId = entity.Id; }
        //            })
        //            .PreserveReferences();

        //        CreateMap<Entities.Permission, Models.Permission>()
        //            .Include<Entities.ScopePermission, Models.ScopePermission>()
        //            .Include<Entities.ResourcePermission, Models.ResourcePermission>()

        //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
        //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
        //            .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
        //            .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
        //            .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
        //            .PreserveReferences();

        //        #endregion

        //        #region MmPermissionPolicy

        //        CreateMap<Models.Policy, Entities.MmPermissionPolicy>()
        //            .ForMember(p => p.PolicyId, opt => opt.MapFrom(src => src.Id))
        //            .ForMember(p => p.Policy, opt => opt.MapFrom(src => src));

        //        CreateMap<Entities.MmPermissionPolicy, Models.Policy>()
        //            .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Policy>(p.Policy));

        //        #endregion

        //        #region ScopePermission

        //        CreateMap<Models.ScopePermission, Entities.ScopePermission>()
        //            .IncludeBase<Models.Permission, Entities.Permission>()
        //            .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
        //            .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
        //            .AfterMap((model, entity) => {
        //                foreach (MmScopePermissionScope item in entity.Scopes) { item.ScopePermission = entity; item.ScopePermissionId = entity.Id; }
        //            })
        //            .PreserveReferences();

        //        CreateMap<Entities.ScopePermission, Models.ScopePermission>()
        //            .IncludeBase<Entities.Permission, Models.Permission>()
        //            .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
        //            .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
        //            .PreserveReferences();

        //        #endregion

        //        #region Scope

        //        CreateMap<Models.Scope, Entities.MmScopePermissionScope>()
        //            .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
        //            .ForMember(p => p.Scope, opt => opt.MapFrom(src => src));

        //        CreateMap<Entities.MmScopePermissionScope, Models.Scope>()
        //            //.ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Scope>(p.Scope));
        //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Scope.Id))
        //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Scope.Name))
        //            .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.Scope.DisplayName))
        //            .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.Scope.IconUri));

        //        #endregion

        //        #region ResourcePermission

        //        CreateMap<Models.ResourcePermission, Entities.ResourcePermission>()
        //            .IncludeBase<Models.Permission, Entities.Permission>()
        //            .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
        //            .ForMember(p => p.ResouceType, opt => opt.MapFrom(src => src.ResouceType))
        //            .PreserveReferences();

        //        CreateMap<Entities.ResourcePermission, Models.ResourcePermission>()
        //            .IncludeBase<Entities.Permission, Models.Permission>()
        //            .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
        //            .ForMember(p => p.ResouceType, opt => opt.MapFrom(src => src.ResouceType))
        //            .PreserveReferences();

        //        #endregion

        //    }
        //}
    }
}
