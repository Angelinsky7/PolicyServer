using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PolicyServer1.EntityFramework.Storage.Entities;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class PermissionMappers {
        internal static IMapper Mapper { get; }

        static PermissionMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PermissionMapperProfile>()).CreateMapper();

        //TODO(demarco): Rebuild this after the changes !
        public static Models.Permission ToModel(this Entities.Permission entity) => Mapper.Map<Models.Permission>(entity);
        public static Entities.Permission ToEntity(this Models.Permission model) => Mapper.Map<Entities.Permission>(model);
        //public static Entities.Permission ToEntity(this Models.PolicyPermission model, Entities.Permission entity) {
        //    return Mapper.Map(model, entity);
        //}
    }

    public class PermissionMapperProfile : Profile {
        public PermissionMapperProfile() {

            CreateMap<Entities.Permission, Models.Permission>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
                .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
                .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
                .AfterMap((model, entity) => {
                    foreach (Entities.MmPermissionPolicy item in entity.Policies) { item.Permission = entity; item.PermissionId = entity.Id; }
                });

            CreateMap<Entities.MmPermissionPolicy, Models.Policy>()
                .ReverseMap()
                .ForMember(p => p.PolicyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Policy, opt => opt.MapFrom(src => src.ToEntity()));

            CreateMap<Entities.ScopePermission, Models.ScopePermission>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Base.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Base.Description))
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Base, opt => opt.MapFrom(src => new Permission {
                    Id = src.Id,
                    Name = src.Name,
                    Description = src.Description,
                }))
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .AfterMap((model, entity) => {
                    foreach (MmScopePermissionScope item in entity.Scopes) { item.ScopePermission = entity; item.ScopePermissionId = entity.Id; }
                });

            CreateMap<Entities.MmScopePermissionScope, Models.Scope>()
                .ReverseMap()
                .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Scope, opt => opt.MapFrom(src => src.ToEntity()));

            CreateMap<Entities.ResourcePermission, Models.ResourcePermission>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Base.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Base.Description))
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.ResouceType, opt => opt.MapFrom(src => src.ResouceType))
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Base, opt => opt.MapFrom(src => new Permission {
                    Id = src.Id,
                    Name = src.Name,
                    Description = src.Description,
                }))
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.ResouceType, opt => opt.MapFrom(src => src.ResouceType));
        }
    }
}
