using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class RoleMappers {
        //internal static IMapper Mapper { get; }

        //static RoleMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<RoleMapperProfile>()).CreateMapper();

        //public static Models.Role ToModel(this Entities.Role entity) => Mapper.Map<Models.Role>(entity);
        //public static Entities.Role ToEntity(this Models.Role model) => Mapper.Map<Entities.Role>(model);

        public static Models.Role ToModel(this Entities.Role entity) => Role.ToModel(entity);
        public static Entities.Role ToEntity(this Models.Role model) {
            Entities.Role entity = Role.ToEntity(model);
            foreach (Entities.MmRoleRole item in entity.Parents) { item.Parent = entity; item.ParentId = entity.Id; }
            return entity;
        }

        public static void UpdateEntity(this Models.Role model, Entities.Role entity) {
            Entities.Role entityUpdate = Role.ToEntity(model);
            if (entity.Id != entityUpdate.Id) { throw new ArgumentOutOfRangeException(nameof(Entities.Scope.Id)); }
            if (entity.Name != entityUpdate.Name) { entity.Name = entityUpdate.Name; }
            if (entity.Description != entityUpdate.Description) { entity.Description = entityUpdate.Description; }

            foreach (Entities.MmRoleRole role in entity.Parents.Where(p => !entityUpdate.Parents.Any(a => a.ParentId == p.ParentId && a.RoleId == p.RoleId)).ToList()) {
                entity.Parents.Remove(role);
            }
            foreach (Entities.MmRoleRole role in entityUpdate.Parents.Where(p => !entity.Parents.Any(a => a.ParentId == p.ParentId && a.RoleId == p.RoleId)).ToList()) {
                entity.Parents.Add(role);
            }
        }

        internal class Role {
            internal static Expression<Func<Entities.Role, Models.Role>> Projection => entity => new Models.Role {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Parents = entity.Parents.Select(p => MmRoleRole.ToModel(p)).ToList()
            };
            internal static Expression<Func<Models.Role, Entities.Role>> Transform => model => new Entities.Role {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Parents = model.Parents.Select(p => MmRoleRole.ToEntity(p)).ToList()
            };

            //TODO(demarco): We must use a cache for pergfoprtmance....
            internal static Entities.Role ToEntity(Models.Role entity) => Transform.Compile().Invoke(entity);
            internal static Models.Role ToModel(Entities.Role entity) => Projection.Compile().Invoke(entity);
        }

        internal class MmRoleRole {
            internal static Expression<Func<Entities.MmRoleRole, Models.Role>> Projection => entity => new Models.Role {
                Id = entity.Parent.Id,
                Name = entity.Parent.Name,
                Description = entity.Parent.Description,
                Parents = entity.Parent.Parents.Select(p => MmRoleRole.ToModel(p)).ToList()
            };
            internal static Expression<Func<Models.Role, Entities.MmRoleRole>> Transform => model => new Entities.MmRoleRole {
                ParentId = model.Id,
                Parent = Role.ToEntity(model)
            };

            //TODO(demarco): We must use a cache for pergfoprtmance....
            internal static Entities.MmRoleRole ToEntity(Models.Role entity) => Transform.Compile().Invoke(entity);
            internal static Models.Role ToModel(Entities.MmRoleRole entity) => Projection.Compile().Invoke(entity);
        }

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
                .ForMember(p => p.Parents, opt => opt.MapFrom(src => src.Parents))
                .PreserveReferences();

            #endregion

            #region Role

            CreateMap<Models.Role, Entities.MmRoleRole>()
                .ForMember(p => p.ParentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Parent, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmRoleRole, Models.Role>()
                .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Role>(p.Parent));

            #endregion

        }
    }
}
