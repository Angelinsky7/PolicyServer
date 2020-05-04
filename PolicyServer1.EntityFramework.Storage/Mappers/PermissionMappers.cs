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

            CreateMap<Models.Policy, Entities.MmPermissionPolicy>()
                .ForMember(p => p.PolicyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Policy, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Models.ScopePermission, Entities.ScopePermission>()
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .AfterMap((model, entity) => {
                    foreach (MmScopePermissionScope item in entity.Scopes) { item.ScopePermission = entity; item.ScopePermissionId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Models.Scope, Entities.MmScopePermissionScope>()
                .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Scope, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Models.ResourcePermission, Entities.ResourcePermission>()
                .ForMember(p => p.Resource, opt => opt.MapFrom(src => src.Resource))
                .ForMember(p => p.ResouceType, opt => opt.MapFrom(src => src.ResouceType))
                .PreserveReferences();

        }
    }
}
