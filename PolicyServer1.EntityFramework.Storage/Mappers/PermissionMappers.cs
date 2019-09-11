using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class PermissionMappers {
        internal static IMapper Mapper { get; }

        static PermissionMappers() {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PermissionMapperProfile>()).CreateMapper();
        }

        //TODO(demarco): Rebuild this after the changes !
        //public static Models.PolicyPermission ToModel(this Entities.Permission entity) {
        //    return Mapper.Map<Models.PolicyPermission>(entity);
        //}
        //public static Entities.Permission ToEntity(this Models.PolicyPermission model) {
        //    return Mapper.Map<Entities.Permission>(model);
        //}
        //public static Entities.Permission ToEntity(this Models.PolicyPermission model, Entities.Permission entity) {
        //    return Mapper.Map(model, entity);
        //}
    }

    public class PermissionMapperProfile : Profile {
        public PermissionMapperProfile() {

            //TODO(demarco): Rebuild this after the changes !
            //CreateMap<Entities.Permission, Models.Permission>()
            //    .ReverseMap();

            //CreateMap<Entities.Permission, Models.PolicyPermission>()
            //    .ReverseMap();

            //CreateMap<Entities.ClientScope, String>()
            //    .ConstructUsing(src => src.Scope)
            //    .ReverseMap()
            //    .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

            //CreateMap<Entities.ClientSecret, Models.Secret>(MemberList.Destination)
            //    .ReverseMap();

        }
    }
}
