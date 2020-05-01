using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class ResourceMappers {

        internal static IMapper Mapper { get; }

        static ResourceMappers() {
            Mapper = new MapperConfiguration(cfg => {
                cfg.AddProfile<ScopeMapperProfile>();
                cfg.AddProfile<ResourceMapperProfile>();
            }).CreateMapper();
        }

        public static Models.Resource ToModel(this Entities.Resource entity) => Mapper.Map<Models.Resource>(entity);
        public static Entities.Resource ToEntity(this Models.Resource model) => Mapper.Map<Entities.Resource>(model);

    }

    public class ResourceMapperProfile : Profile {
        public ResourceMapperProfile() {
            CreateMap<Models.Resource, Entities.Resource>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(p => p.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(p => p.Uris, opt => opt.MapFrom(src => src.Uris))
                .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
                .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .AfterMap((model, entity) => {
                    foreach (Entities.ResourceUri item in entity.Uris) { item.Resource = entity; }
                    foreach (Entities.MmResourceScope item in entity.Scopes) { item.Resource = entity; item.ResourceId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Uri, Entities.ResourceUri>()
                .ForMember(p => p.Uri, opt => opt.MapFrom(src => src.AbsoluteUri))
                .PreserveReferences()
                .ReverseMap()
                .ConstructUsing(src => new Uri(src.Uri));

            CreateMap<Models.Scope, Entities.MmResourceScope>()
                .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Scope, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            //CreateMap<Entities.Resource, Models.Resource>()
            //    .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
            //    .ForMember(p => p.Type, opt => opt.MapFrom(src => src.Type))
            //    .ForMember(p => p.Uris, opt => opt.MapFrom(src => src.Uris))
            //    .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
            //    .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
            //    .ReverseMap()
            //    .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(p => p.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
            //    .ForMember(p => p.Type, opt => opt.MapFrom(src => src.Type))
            //    .ForMember(p => p.Uris, opt => opt.MapFrom(src => src.Uris))
            //    .ForMember(p => p.IconUri, opt => opt.MapFrom(src => src.IconUri))
            //    .ForMember(p => p.Scopes, opt => opt.MapFrom(src => src.Scopes))
            //    .AfterMap((model, entity) => {
            //        foreach (Entities.ResourceUri item in entity.Uris) { item.Resource = entity; }
            //        foreach (Entities.MmResourceScope item in entity.Scopes) { item.Resource = entity; item.ResourceId = entity.Id; }
            //    });
            //    //.PreserveReferences();

            //CreateMap<Entities.ResourceUri, Uri>()
            //    .ConstructUsing(src => new Uri(src.Uri))
            //    .ReverseMap()
            //    .ForMember(p => p.Uri, opt => opt.MapFrom(src => src.AbsoluteUri));

            //CreateMap<Entities.MmResourceScope, Models.Scope>()
            //    .ReverseMap()
            //    .ForMember(p => p.ScopeId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(p => p.Scope, opt => opt.MapFrom(src => src.ToEntity()));
            //    //.PreserveReferences();

        }
    }

}
