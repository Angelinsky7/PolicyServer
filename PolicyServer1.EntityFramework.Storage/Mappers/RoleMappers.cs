using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class RoleMappers {
        internal static IMapper Mapper { get; }

        static RoleMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<RoleMapperProfile>()).CreateMapper();

        public static Models.Role ToModel(this Entities.Role entity) => Mapper.Map<Models.Role>(entity);
        public static Entities.Role ToEntity(this Models.Role model) => Mapper.Map<Entities.Role>(model);

    }

    public class RoleMapperProfile : Profile {
        public RoleMapperProfile() {

            #region Role

            CreateMap<Models.Role, Entities.Role>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.Parents, opt => opt.MapFrom(src => src.Parents))
                .AfterMap((model, entity) => {
                    foreach (Entities.MmRoleRole item in entity.Parents) { item.Role = entity; item.RoleId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.Role, Models.Role>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.Parents, opt => opt.MapFrom(src => src.Parents));

            #endregion

            #region Role

            CreateMap<Models.Role, Entities.MmRoleRole>()
                .ForMember(p => p.ParentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Parent, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Entities.MmRoleRole, Models.Role>()
                .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Role>(p.Parent));

            #endregion

        }
    }
}
