using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class ClientMappers {

        internal static IMapper Mapper { get; }

        static ClientMappers() {
            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<ClientMapperProfile>(); }).CreateMapper();
        }

        public static Models.Client ToModel(this Entities.Client entity) {
            return Mapper.Map<Models.Client>(entity);
        }

        public static Models.Client Reload(this Models.Client model, Models.Client updated) {
            return Mapper.Map(updated, model);
        }

        public static Entities.Client ToEntity(this Models.Client model) {
            return Mapper.Map<Entities.Client>(model);
        }

        public static Entities.Client ToEntity(this Models.Client model, Entities.Client entity) {
            return Mapper.Map(model, entity);
        }

    }

    public class ClientMapperProfile : Profile {
        public ClientMapperProfile() {

            CreateMap<Entities.Client, Models.Client>()
                .ReverseMap();

            CreateMap<Entities.Policy, Models.Policy>()
               .ReverseMap();

            CreateMap<Entities.Permission, Models.Permission>()
                .ReverseMap();

            CreateMap<Entities.Role, Models.Role>()
                .ReverseMap();

            //CreateMap<Entities.ClientScope, String>()
            //    .ConstructUsing(src => src.Scope)
            //    .ReverseMap()
            //    .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

            //CreateMap<Entities.ClientSecret, Models.Secret>(MemberList.Destination)
            //    .ReverseMap();

        }
    }

}
