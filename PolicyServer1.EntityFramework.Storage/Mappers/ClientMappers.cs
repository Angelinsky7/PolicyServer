using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PolicyServer1.EntityFramework.Storage.Datas;
using PolicyServer1.Models;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class ClientMappers {

        internal static IMapper Mapper { get; }

        static ClientMappers() {
            MapperConfiguration configuration = new MapperConfiguration(cfg => {
                //cfg.AddCollectionMappers();
                //cfg.UseEntityFrameworkCoreModel<PolicyDbContext>();

                //cfg.AddProfile<ClientMapperProfile>();
                //cfg.AddProfile<ScopeMapperProfile>();
                //cfg.AddProfile<ResourceMapperProfile>();
                //cfg.AddProfile<RoleMapperProfile>();
                //cfg.AddProfile<PolicyMapperProfile>();
                //cfg.AddProfile<PermissionMapperProfile>();
            });

            //#if DEBUG
            //            configuration.AssertConfigurationIsValid();
            //#endif

            Mapper = configuration.CreateMapper();
        }

        public static Models.Client ToModel(this Entities.Client entity) => Mapper.Map<Models.Client>(entity);

        public static Entities.Client ToEntity(this Models.Client model) => Mapper.Map<Entities.Client>(model);

        public static void UpdateEntity(this Models.Client model, Entities.Client entity) => Mapper.Map(model, entity);

        //public static IQueryable<Models.Client> ToModel(this IQueryable<Entities.Client> source) => source.ProjectTo<Models.Client>(Mapper.ConfigurationProvider);
        public static Expression<Func<Entities.Client, Models.Client>> Projection => entity => Mapper.Map<Models.Client>(entity);

    }

    public class ClientMapperProfile : Profile {
        public ClientMapperProfile() {

            #region Client

            CreateMap<Models.Client, Entities.Client>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Enabled, opt => opt.MapFrom(src => src.Enabled))
                .ForMember(p => p.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(p => p.Secrets, opt => opt.MapFrom(src => src.Secrets))
                .ForMember(p => p.RequireClientSecret, opt => opt.MapFrom(src => src.RequireClientSecret))
                .ForMember(p => p.ClientName, opt => opt.MapFrom(src => src.ClientName))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.ClientUri, opt => opt.MapFrom(src => src.ClientUri))
                .ForMember(p => p.PolicyEnforcement, opt => opt.MapFrom(src => src.Options.PolicyEnforcement))
                .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.Options.DecisionStrategy))
                .ForMember(p => p.AnalyseModeEnabled, opt => opt.MapFrom(src => src.Options.AnalyseModeEnabled))
                .ForMember(p => p.PermissionSplitter, opt => opt.MapFrom(src => src.Options.PermissionSplitter))
                .ForMember(p => p.Resources, opt => opt.MapFrom(src => src.Resources))
                .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
                .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
                .ForMember(p => p.Permissions, opt => opt.MapFrom(src => src.Permissions))
                .AfterMap((model, entity) => {
                    foreach (Entities.ClientSecret item in entity.Secrets) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientResource item in entity.Resources) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientScope item in entity.Scopes) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientRole item in entity.Roles) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientPolicy item in entity.Policies) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientPermission item in entity.Permissions) { item.Client = entity; item.ClientId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.Client, Models.Client>()
               .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(p => p.Enabled, opt => opt.MapFrom(src => src.Enabled))
               .ForMember(p => p.ClientId, opt => opt.MapFrom(src => src.ClientId))
               .ForMember(p => p.Secrets, opt => opt.MapFrom(src => src.Secrets))
               .ForMember(p => p.RequireClientSecret, opt => opt.MapFrom(src => src.RequireClientSecret))
               .ForMember(p => p.ClientName, opt => opt.MapFrom(src => src.ClientName))
               .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(p => p.ClientUri, opt => opt.MapFrom(src => src.ClientUri))
               .ForMember(p => p.Options, opt => opt.MapFrom(src => new ClientOption {
                   AnalyseModeEnabled = src.AnalyseModeEnabled,
                   DecisionStrategy = src.DecisionStrategy,
                   PermissionSplitter = src.PermissionSplitter,
                   PolicyEnforcement = src.PolicyEnforcement
               }))
               .ForMember(p => p.Resources, opt => opt.MapFrom(src => src.Resources))
               .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
               .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
               .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
               .ForMember(p => p.Permissions, opt => opt.MapFrom(src => src.Permissions))
               .PreserveReferences();

            #endregion

            CreateMap<Models.Secret, Entities.ClientSecret>()
                .ReverseMap();

            #region Resource

            CreateMap<Models.Resource, Entities.MmClientResource>()
                .ForMember(p => p.ResourceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmClientResource, Models.Resource>()
                //.ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Resource>(p.Resource));
                .ConstructUsing((p, ctx) => ResourceMappers.Resource.ToModel(p.Resource));

            #endregion

            #region Scope

            CreateMap<Models.Scope, Entities.MmClientScope>()
                .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Scope, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmClientScope, Models.Scope>()
                //.ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Scope>(p.Scope));
                .ConstructUsing((p, ctx) => ScopeMappers.Scope.ToModel(p.Scope));

            #endregion

            #region Role

            CreateMap<Models.Role, Entities.MmClientRole>()
                .ForMember(p => p.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Role, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmClientRole, Models.Role>()
                .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Role>(p.Role));
                //.ConstructUsing((p, ctx) => RoleMappers.Role.ToModel(p.Role));


            #endregion

            #region Policy

            CreateMap<Models.Policy, Entities.MmClientPolicy>()
                .ForMember(p => p.PolicyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Policy, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmClientPolicy, Models.Policy>()
                .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Policy>(p.Policy));

            #endregion

            #region Permission

            CreateMap<Models.Permission, Entities.MmClientPermission>()
                .ForMember(p => p.PermissionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Permission, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmClientPermission, Models.Permission>()
                //.ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Permission>(p.Permission));
                .ConstructUsing((p, ctx) => PermissionMappers.Permission.ToModel(p.Permission));
            //.ForMember(p => p.Id, opt => opt.MapFrom(src => src.))

            #endregion

        }
    }

}
