using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class RoleMappers {
        internal static IMapper Mapper { get; }

        static RoleMappers() {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<RoleMapperProfile>()).CreateMapper();
        }

        public static Models.PolicyRole ToModel(this Entities.Role entity) {
            return Mapper.Map<Models.PolicyRole>(entity);
        }
        public static Entities.Role ToEntity(this Models.PolicyRole model) {
            return Mapper.Map<Entities.Role>(model);
        }
        public static Entities.Role ToEntity(this Models.PolicyRole model, Entities.Role entity) {
            return Mapper.Map(model, entity);
        }
    }

    public class RoleMapperProfile : Profile {
        public RoleMapperProfile() {

            CreateMap<Entities.Role, Models.Role>()
                .ReverseMap();

            CreateMap<Entities.Role, Models.PolicyRole>()
                .ReverseMap();

            CreateMap<Entities.RoleIdentity, String>()
                .ConstructUsing(src => src.Identity)
                .ReverseMap()
                .ForMember(dest => dest.Identity, opt => opt.MapFrom(src => src));

            CreateMap<Entities.RoleSubject, String>()
                .ConstructUsing(src => src.Subject)
                .ReverseMap()
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src));

            CreateMap<Entities.RolePermission, Models.Permission>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PermissionId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Permission.Name))
                .ReverseMap();

            CreateMap<Entities.RoleRole, Models.Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Parent.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Parent.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Parent.Description))
                .ForMember(dest => dest.PolicyId, opt => opt.MapFrom(src => src.Parent.PolicyId))
                .ForMember(dest => dest.IdentityRoles, opt => opt.MapFrom(src => src.Parent.IdentityRoles))
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.Parent.Subjects))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Parent.Permissions))
                .ReverseMap()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Id));

        }
    }
}
