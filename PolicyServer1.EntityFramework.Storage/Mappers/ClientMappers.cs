using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class ClientMappers {

        internal static IMapper Mapper { get; }

        static ClientMappers() {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<ClientMapperProfile>();
                cfg.AddProfile<ScopeMapperProfile>();
                cfg.AddProfile<ResourceMapperProfile>();
                cfg.AddProfile<RoleMapperProfile>();
                cfg.AddProfile<PolicyMapperProfile>();
                cfg.AddProfile<PermissionMapperProfile>();
            });

//#if DEBUG
//            configuration.AssertConfigurationIsValid();
//#endif

            Mapper = configuration.CreateMapper();
        }

        //public static Models.Client ToModel(this Entities.Client entity) {
        //    return Mapper.Map<Models.Client>(entity);
        //}

        //public static Models.Client Reload(this Models.Client model, Models.Client updated) {
        //    return Mapper.Map(updated, model);
        //}

        //public static Entities.Client ToEntity(this Models.Client model) {
        //    return Mapper.Map<Entities.Client>(model);
        //}

        //public static Entities.Client ToEntity(this Models.Client model, Entities.Client entity) {
        //    return Mapper.Map(model, entity);
        //}

        public static Models.Client ToModel(this Entities.Client entity) => Mapper.Map<Models.Client>(entity);

        public static Entities.Client ToEntity(this Models.Client model) => Mapper.Map<Entities.Client>(model);

    }

    public class ClientMapperProfile : Profile {
        public ClientMapperProfile() {

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
                    foreach (Entities.MmClientSecret item in entity.Secrets) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientResource item in entity.Resources) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientScope item in entity.Scopes) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientRole item in entity.Roles) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientPolicy item in entity.Policies) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientPermission item in entity.Permissions) { item.Client = entity; item.ClientId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Models.Secret, Entities.MmClientSecret>()
               .ForMember(p => p.Secret, opt => opt.MapFrom(src => src));

            CreateMap<Models.Secret, Entities.Secret>(MemberList.Destination);

            CreateMap<Models.Resource, Entities.MmClientResource>()
                .ForMember(p => p.ResourceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Models.Scope, Entities.MmClientScope>()
                .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Scope, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Models.Role, Entities.MmClientRole>()
                .ForMember(p => p.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Role, opt => opt.MapFrom(src => src));

            CreateMap<Models.Policy, Entities.MmClientPolicy>()
                .ForMember(p => p.PolicyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Policy, opt => opt.MapFrom(src => src));

            CreateMap<Models.Permission, Entities.MmClientPermission>()
                .ForMember(p => p.PermissionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Permission, opt => opt.MapFrom(src => src));

            //CreateMap<Entities.Client, Models.Client>()
            //    .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Enabled, opt => opt.MapFrom(src => src.Enabled))
            //    .ForMember(p => p.ClientId, opt => opt.MapFrom(src => src.ClientId))
            //    .ForMember(p => p.Secrets, opt => opt.MapFrom(src => src.Secrets.Select(a => a.Secret)))
            //    .ForMember(p => p.RequireClientSecret, opt => opt.MapFrom(src => src.RequireClientSecret))
            //    .ForMember(p => p.ClientName, opt => opt.MapFrom(src => src.ClientName))
            //    .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
            //    .ForMember(p => p.ClientUri, opt => opt.MapFrom(src => src.ClientUri))
            //    .ForPath(p => p.Options.PolicyEnforcement, opt => opt.MapFrom(src => src.PolicyEnforcement))
            //    .ForPath(p => p.Options.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
            //    .ForPath(p => p.Options.AnalyseModeEnabled, opt => opt.MapFrom(src => src.AnalyseModeEnabled))
            //    .ForPath(p => p.Options.PermissionSplitter, opt => opt.MapFrom(src => src.PermissionSplitter))
            //    .ForMember(p => p.Resources, opt => opt.MapFrom(src => src.Resources.Select(a => a.Resource)))
            //    .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes.Select(a => a.Scope)))
            //    .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles.Select(a => a.Role)))
            //    .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies.Select(a => a.Policy)))
            //    .ForMember(p => p.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(a => a.Permission)))
            //    .ReverseMap()
            //    .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Enabled, opt => opt.MapFrom(src => src.Enabled))
            //    .ForMember(p => p.ClientId, opt => opt.MapFrom(src => src.ClientId))
            //    .ForMember(p => p.Secrets, opt => opt.MapFrom(src => src.Secrets))
            //    .ForMember(p => p.RequireClientSecret, opt => opt.MapFrom(src => src.RequireClientSecret))
            //    .ForMember(p => p.ClientName, opt => opt.MapFrom(src => src.ClientName))
            //    .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
            //    .ForMember(p => p.ClientUri, opt => opt.MapFrom(src => src.ClientUri))
            //    .ForMember(p => p.PolicyEnforcement, opt => opt.MapFrom(src => src.Options.PolicyEnforcement))
            //    .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.Options.DecisionStrategy))
            //    .ForMember(p => p.AnalyseModeEnabled, opt => opt.MapFrom(src => src.Options.AnalyseModeEnabled))
            //    .ForMember(p => p.PermissionSplitter, opt => opt.MapFrom(src => src.Options.PermissionSplitter))
            //    .ForMember(p => p.Resources, opt => opt.MapFrom(src => src.Resources))
            //    .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
            //    .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
            //    .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
            //    .ForMember(p => p.Permissions, opt => opt.MapFrom(src => src.Permissions))
            //    .AfterMap((model, entity) => {
            //        foreach (Entities.MmClientSecret item in entity.Secrets) { item.Client = entity; item.ClientId = entity.Id; }
            //        foreach (Entities.MmClientResource item in entity.Resources) { item.Client = entity; item.ClientId = entity.Id; }
            //        foreach (Entities.MmClientScope item in entity.Scopes) { item.Client = entity; item.ClientId = entity.Id; }
            //        foreach (Entities.MmClientRole item in entity.Roles) { item.Client = entity; item.ClientId = entity.Id; }
            //        foreach (Entities.MmClientPolicy item in entity.Policies) { item.Client = entity; item.ClientId = entity.Id; }
            //        foreach (Entities.MmClientPermission item in entity.Permissions) { item.Client = entity; item.ClientId = entity.Id; }
            //    })
            //    .PreserveReferences();

            //CreateMap<Entities.MmClientSecret, Models.Secret>()
            //    .ReverseMap()
            //    .ForMember(p => p.Secret, opt => opt.MapFrom(src => src));

            //CreateMap<Entities.Secret, Models.Secret>(MemberList.Destination)
            //    .ReverseMap();

            //CreateMap<Entities.MmClientResource, Models.Resource>()
            //    .ReverseMap()
            //    .ForMember(p => p.ResourceId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Resource, opt => opt.MapFrom(src => src))
            //    .PreserveReferences();

            //CreateMap<Entities.Resource, Models.Resource>(MemberList.Destination)
            //    .ReverseMap()
            //    .PreserveReferences();

            //CreateMap<Entities.MmClientScope, Models.Scope>()
            //    .ReverseMap()
            //    .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Scope, opt => opt.MapFrom(src => src))
            //    .PreserveReferences();

            //CreateMap<Entities.Scope, Models.Scope>(MemberList.Destination)
            //    .ReverseMap();

            //CreateMap<Entities.MmClientRole, Models.Role>()
            //    .ReverseMap()
            //    .ForMember(p => p.RoleId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Role, opt => opt.MapFrom(src => src.ToEntity()));

            //CreateMap<Entities.Role, Models.Role>(MemberList.Destination)
            //    .ReverseMap();

            //CreateMap<Entities.MmClientPolicy, Models.Policy>()
            //    .ReverseMap()
            //    .ForMember(p => p.PolicyId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Policy, opt => opt.MapFrom(src => src.ToEntity()));

            //CreateMap<Entities.Policy, Models.Policy>(MemberList.Destination)
            //    .ReverseMap();

            //CreateMap<Entities.MmClientPermission, Models.Permission>()
            //    .ReverseMap()
            //    .ForMember(p => p.PermissionId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Permission, opt => opt.MapFrom(src => src.ToEntity()));

            //CreateMap<Entities.Permission, Models.Permission>(MemberList.Destination)
            //    .ReverseMap();

        }
    }

}
