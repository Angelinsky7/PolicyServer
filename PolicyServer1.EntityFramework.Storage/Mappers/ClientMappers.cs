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
            MapperConfiguration configuration = new MapperConfiguration(cfg => {
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

        public static Models.Client ToModel(this Entities.Client entity) => Mapper.Map<Models.Client>(entity);

        public static Entities.Client ToEntity(this Models.Client model) => Mapper.Map<Entities.Client>(model);

        public static void UpdateEntity(this Models.Client model, Entities.Client entity) => Mapper.Map(model, entity);

        public static IQueryable<Models.Client> ToModel(this IQueryable<Entities.Client> source) => source.ProjectTo<Models.Client>(Mapper.ConfigurationProvider);

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
                    foreach (Entities.ClientSecret item in entity.Secrets) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientResource item in entity.Resources) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientScope item in entity.Scopes) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientRole item in entity.Roles) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientPolicy item in entity.Policies) { item.Client = entity; item.ClientId = entity.Id; }
                    foreach (Entities.MmClientPermission item in entity.Permissions) { item.Client = entity; item.ClientId = entity.Id; }
                })
                .PreserveReferences();

            //CreateMap<Models.Secret, Entities.Secret>(MemberList.Destination);
            //TODO(demarco): It's missing
            CreateMap<Models.Secret, Entities.ClientSecret>();

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

        }
    }

}
