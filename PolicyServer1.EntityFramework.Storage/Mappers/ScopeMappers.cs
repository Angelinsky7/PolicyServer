using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.QueryableExtensions;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class ScopeMappers {

        //internal static IMapper Mapper { get; }

        //static ScopeMappers() => Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ScopeMapperProfile>()).CreateMapper();

        //public static Models.Scope ToModel(this Entities.Scope entity) => Mapper.Map<Models.Scope>(entity);
        //public static Entities.Scope ToEntity(this Models.Scope model) => Mapper.Map<Entities.Scope>(model);
        ////public static IQueryable<Models.Scope> ToModel(this IQueryable<Entities.Scope> source) => source.ProjectTo<Models.Scope>(Mapper.ConfigurationProvider);
        ////public static Expression<Func<Entities.Scope, Models.Scope>> Projection => entity => Mapper.Map<Models.Scope>(entity);
        //public static void UpdateEntity(this Models.Scope model, Entities.Scope entity) => Mapper.Map(model, entity);

        //TODO(demarco): We must use a cache to REUSE the entity (ef core issue with same entity that are not the same instance)

        //[ReplaceWithExpression(PropertyName = nameof(Projection))]
        public static Models.Scope ToModel(this Entities.Scope entity) => Scope.ToModel(entity);
        public static Entities.Scope ToEntity(this Models.Scope model) => Scope.ToEntity(model);
        public static void UpdateEntity(this Models.Scope model, Entities.Scope entity) {
            Entities.Scope entityUpdate = Scope.ToEntity(model);
            if (entity.Id != entityUpdate.Id) { throw new ArgumentOutOfRangeException(nameof(Entities.Scope.Id)); }
            if (entity.Name != entityUpdate.Name) { entity.Name = entityUpdate.Name; }
            if (entity.DisplayName != entityUpdate.DisplayName) { entity.DisplayName = entityUpdate.DisplayName; }
            if (entity.IconUri != entityUpdate.IconUri) { entity.IconUri = entityUpdate.IconUri; }
        }

        internal class Scope {
            internal static Expression<Func<Entities.Scope, Models.Scope>> Projection => entity => new Models.Scope {
                Id = entity.Id,
                Name = entity.Name,
                DisplayName = entity.DisplayName,
                IconUri = entity.IconUri
            };
            internal static Expression<Func<Models.Scope, Entities.Scope>> Transform => model => new Entities.Scope {
                Id = model.Id,
                Name = model.Name,
                DisplayName = model.DisplayName,
                IconUri = model.IconUri
            };

            //TODO(demarco): We must use a cache for pergfoprtmance....
            internal static Entities.Scope ToEntity(Models.Scope entity) => Transform.Compile().Invoke(entity);
            internal static Models.Scope ToModel(Entities.Scope entity) => Projection.Compile().Invoke(entity);
        }

    }

    //public class ScopeMapperProfile : Profile {
    //    public ScopeMapperProfile() {

    //        #region Scope

    //        CreateMap<Models.Scope, Entities.Scope>()
    //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
    //            .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
    //            .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
    //            .PreserveReferences();

    //        CreateMap<Entities.Scope, Models.Scope>(MemberList.Destination)
    //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
    //            .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
    //            .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
    //            .PreserveReferences();

    //        #endregion

    //        //#region Uri

    //        //CreateMap<Uri, String>()
    //        //    .ConstructUsing(src => src.AbsoluteUri);

    //        //CreateMap<String, Uri>()
    //        //    .ConvertUsing(src => !String.IsNullOrEmpty(src) ? new Uri(src) : null);

    //        //#endregion

    //    }
    //}

}
