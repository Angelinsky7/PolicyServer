﻿#define DEBUG_MAPPING

using AutoMapper;
using PolicyServer1.EntityFramework.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Mappers {
    public static class PolicyMappers {
        //internal static IMapper Mapper { get; }

        //static PolicyMappers() => Mapper = new MapperConfiguration(cfg => {
        //    cfg.AddProfile<RoleMapperProfile>();
        //    cfg.AddProfile<PolicyMapperProfile>();
        //}).CreateMapper();

        //public static Models.Policy ToModel(this Entities.Policy entity) {
        //    return Mapper.Map<Models.Policy>(entity);
        //}
        //public static Entities.Policy ToEntity(this Models.Policy model) {
        //    return Mapper.Map<Entities.Policy>(model);
        //}

        //public static Entities.Policy ToEntity(this Models.Policy model, Entities.Policy entity) {
        //    return Mapper.Map(model, entity);
        //}

        public static Models.Policy ToModel(this Entities.Policy entity) => Policy.ToModel(entity);
        public static Entities.Policy ToEntity(this Models.Policy model) => Policy.ToEntity(model);
        public static void UpdateEntity(this Models.Policy model, Entities.Policy entity) {

        }

        internal class Policy {
            internal static Expression<Func<Entities.Policy, Models.Policy>> Projection => entity => (entity as RolePolicy) != null ? new Models.RolePolicy {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Logic = entity.Logic,
                Roles = (entity as RolePolicy).Roles.AsQueryable().Select(MmRolePolicyRole.Projection).ToList()
            } : (entity as TimePolicy) != null ? (Models.Policy)new Models.TimePolicy {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Logic = entity.Logic,
                NotBefore = (entity as TimePolicy).NotBefore,
                NotOnOrAfter = (entity as TimePolicy).NotOnOrAfter,
                DayOfMonth = (entity as TimePolicy).DayOfMonth != null ? TimePolicyRange.ToModel((entity as TimePolicy).DayOfMonth) : null,
                Month = (entity as TimePolicy).Month != null ? TimePolicyRange.ToModel((entity as TimePolicy).Month) : null,
                Year = (entity as TimePolicy).Year != null ? TimePolicyRange.ToModel((entity as TimePolicy).Year) : null,
                Hour = (entity as TimePolicy).Hour != null ? TimePolicyRange.ToModel((entity as TimePolicy).Hour) : null,
                Minute = (entity as TimePolicy).Minute != null ? TimePolicyRange.ToModel((entity as TimePolicy).Minute) : null
            } : (entity as ClientPolicy) != null ? (Models.Policy)new Models.ClientPolicy {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Logic = entity.Logic,
                Clients = (entity as ClientPolicy).Clients.Select(p => p.Client).ToList()
            } : (entity as UserPolicy) != null ? (Models.Policy)new Models.UserPolicy {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Logic = entity.Logic,
                Users = (entity as UserPolicy).Users.Select(p => p.User).ToList()
            } : (entity as GroupPolicy) != null ? (Models.Policy)new Models.GroupPolicy {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Logic = entity.Logic,
                Groups = (entity as GroupPolicy).Groups.Select(p => p.Group).ToList()
            } : (entity as AggregatedPolicy) != null ? (Models.Policy)new Models.AggregatedPolicy {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Logic = entity.Logic,
                DecisionStrategy = (entity as AggregatedPolicy).DecisionStrategy,
                Policies = (entity as AggregatedPolicy).Policies.AsQueryable().Select(MmAggregatedPolicyPolicy.Projection).ToList()
            } : null;
            internal static Expression<Func<Models.Policy, Entities.Policy>> Transform => model => (model as Models.RolePolicy) != null ? new Entities.RolePolicy {
            } : (model as Models.TimePolicy) != null ? (Entities.Policy)new Entities.TimePolicy {
            } : (model as Models.ClientPolicy) != null ? (Entities.Policy)new Entities.ClientPolicy {
            } : (model as Models.UserPolicy) != null ? (Entities.Policy)new Entities.UserPolicy {
            } : (model as Models.GroupPolicy) != null ? (Entities.Policy)new Entities.GroupPolicy {
            } : (model as Models.AggregatedPolicy) != null ? (Entities.Policy)new Entities.AggregatedPolicy {
            } : null;
            internal static Entities.Policy ToEntity(Models.Policy entity) => Transform.Compile().Invoke(entity);
            internal static Models.Policy ToModel(Entities.Policy entity) => Projection.Compile().Invoke(entity);
        }

        internal class MmRolePolicyRole {
            internal static Expression<Func<Entities.MmRolePolicyRole, Models.Role>> Projection => entity => RoleMappers.Role.ToModel(entity.Role);
        }

        internal class MmAggregatedPolicyPolicy {
            internal static Expression<Func<Entities.MmAggregatedPolicyPolicy, Models.Policy>> Projection => entity => PolicyMappers.Policy.ToModel(entity.Policy);
        }

        //internal class TimePolicy {
        //    internal static Expression<Func<Models.TimePolicy, Entities.TimePolicy>> Transform => entity => new Entities.TimePolicy {
        //        Id = entity.Id,
        //        NotBefore = entity.NotBefore,
        //        NotOnOrAfter = entity.NotOnOrAfter,
        //        DayOfMonth = TimePolicyRange.ToEntity(entity.DayOfMonth),
        //        Month = TimePolicyRange.ToEntity(entity.Month),
        //        Year = TimePolicyRange.ToEntity(entity.Year),
        //        Hour = TimePolicyRange.ToEntity(entity.Hour),
        //        Minute = TimePolicyRange.ToEntity(entity.Minute)
        //    };
        //    internal static Expression<Func<Entities.TimePolicy, Models.TimePolicy>> Projection => entity => new Models.TimePolicy {
        //        Id = entity.Id,
        //        NotBefore = entity.NotBefore,
        //        NotOnOrAfter = entity.NotOnOrAfter,
        //        DayOfMonth = TimePolicyRange.ToModel(entity.DayOfMonth),
        //        Month = TimePolicyRange.ToModel(entity.Month),
        //        Year = TimePolicyRange.ToModel(entity.Year),
        //        Hour = TimePolicyRange.ToModel(entity.Hour),
        //        Minute = TimePolicyRange.ToModel(entity.Minute)
        //    };
        //    internal static Entities.TimePolicy ToEntity(Models.TimePolicy entity) => Transform.Compile().Invoke(entity);
        //    internal static Models.TimePolicy ToModel(Entities.TimePolicy entity) => Projection.Compile().Invoke(entity);
        //}

        internal class TimePolicyRange {
            internal static Expression<Func<Models.TimePolicyRange, Entities.TimePolicyRange>> Transform => entity => new Entities.TimePolicyRange {
                From = entity.From,
                To = entity.To
            };
            internal static Expression<Func<Entities.TimePolicyRange, Models.TimePolicyRange>> Projection => entity => new Models.TimePolicyRange {
                From = entity.From,
                To = entity.To
            };
            internal static Entities.TimePolicyRange ToEntity(Models.TimePolicyRange entity) => Transform.Compile().Invoke(entity);
            internal static Models.TimePolicyRange ToModel(Entities.TimePolicyRange entity) => Projection.Compile().Invoke(entity);
        }

    }

    public class PolicyMapperProfile : Profile {
        public PolicyMapperProfile() {

            #region Policy

            CreateMap<Models.Policy, Entities.Policy>()
                .Include<Models.RolePolicy, Entities.RolePolicy>()
                .Include<Models.TimePolicy, Entities.TimePolicy>()
                .Include<Models.ClientPolicy, Entities.ClientPolicy>()
                .Include<Models.UserPolicy, Entities.UserPolicy>()
                .Include<Models.GroupPolicy, Entities.GroupPolicy>()
                .Include<Models.AggregatedPolicy, Entities.AggregatedPolicy>()

                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.Logic, opt => opt.MapFrom(src => src.Logic))
                .PreserveReferences();

            CreateMap<Entities.Policy, Models.Policy>()
                .Include<Entities.RolePolicy, Models.RolePolicy>()
                .Include<Entities.TimePolicy, Models.TimePolicy>()
                .Include<Entities.ClientPolicy, Models.ClientPolicy>()
                .Include<Entities.UserPolicy, Models.UserPolicy>()
                .Include<Entities.GroupPolicy, Models.GroupPolicy>()
                .Include<Entities.AggregatedPolicy, Models.AggregatedPolicy>()

                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(p => p.Logic, opt => opt.MapFrom(src => src.Logic))
                .PreserveReferences();

            #endregion

            #region RolePolicy

            CreateMap<Models.RolePolicy, Entities.RolePolicy>()
                .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
                .AfterMap((model, entity) => {
                    foreach (Entities.MmRolePolicyRole item in entity.Roles) { item.RolePolicy = entity; item.RolePolicyId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.RolePolicy, Models.RolePolicy>()
                .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
                .PreserveReferences();

            #endregion

            #region Role

            CreateMap<Models.Role, Entities.MmRolePolicyRole>()
                .ForMember(p => p.RoleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Role, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmRolePolicyRole, Models.Role>()
                .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Role>(p.Role));

            #endregion

            #region TimePolicy

            CreateMap<Models.TimePolicy, Entities.TimePolicy>()
                .ForMember(p => p.NotBefore, opt => opt.MapFrom(src => src.NotBefore))
                .ForMember(p => p.NotOnOrAfter, opt => opt.MapFrom(src => src.NotOnOrAfter))
                .ForMember(p => p.DayOfMonth, opt => opt.MapFrom(src => src.DayOfMonth))
                .ForMember(p => p.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(p => p.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(p => p.Hour, opt => opt.MapFrom(src => src.Hour))
                .ForMember(p => p.Minute, opt => opt.MapFrom(src => src.Minute))
                .PreserveReferences();

            CreateMap<Entities.TimePolicy, Models.TimePolicy>()
                .ForMember(p => p.NotBefore, opt => opt.MapFrom(src => src.NotBefore))
                .ForMember(p => p.NotOnOrAfter, opt => opt.MapFrom(src => src.NotOnOrAfter))
                .ForMember(p => p.DayOfMonth, opt => opt.MapFrom(src => src.DayOfMonth))
                .ForMember(p => p.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(p => p.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(p => p.Hour, opt => opt.MapFrom(src => src.Hour))
                .ForMember(p => p.Minute, opt => opt.MapFrom(src => src.Minute))
                .PreserveReferences();

            #endregion

            #region TimePolicyRange

            CreateMap<Models.TimePolicyRange, Entities.TimePolicyRange>()
                .ForMember(p => p.From, opt => opt.MapFrom(src => src.From))
                .ForMember(p => p.To, opt => opt.MapFrom(src => src.To))
                .PreserveReferences();

            CreateMap<Entities.TimePolicyRange, Models.TimePolicyRange>()
                .ConstructUsing(p => new Models.TimePolicyRange {
                    From = p.From,
                    To = p.To
                })
                .PreserveReferences();

            #endregion

            #region ClientPolicy

            CreateMap<Models.ClientPolicy, Entities.ClientPolicy>()
                .ForMember(p => p.Clients, opt => opt.MapFrom(src => src.Clients))
                .AfterMap((model, entity) => {
                    foreach (var item in entity.Clients) { item.ClientPolicy = entity; item.ClientPolicyId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.ClientPolicy, Models.ClientPolicy>()
                .ForMember(p => p.Clients, opt => opt.MapFrom(src => src.Clients));

            #endregion

            #region ClientPolicyClient

            CreateMap<String, Entities.ClientPolicyClient>()
                .ForMember(p => p.Client, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Entities.ClientPolicyClient, String>()
                .ConstructUsing(p => p.Client);

            #endregion

            #region UserPolicy

            CreateMap<Models.UserPolicy, Entities.UserPolicy>()
                .ForMember(p => p.Users, opt => opt.MapFrom(src => src.Users))
                .AfterMap((model, entity) => {
                    foreach (var item in entity.Users) { item.UserPolicy = entity; item.UserPolicyId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.UserPolicy, Models.UserPolicy>()
                .ForMember(p => p.Users, opt => opt.MapFrom(src => src.Users));

            #endregion

            #region UserPolicyUser

            CreateMap<String, Entities.UserPolicyUser>()
                .ForMember(p => p.User, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Entities.UserPolicyUser, String>()
                .ConstructUsing(p => p.User);

            #endregion

            #region GroupPolicy

            CreateMap<Models.GroupPolicy, Entities.GroupPolicy>()
                .ForMember(p => p.Groups, opt => opt.MapFrom(src => src.Groups))
                .AfterMap((model, entity) => {
                    foreach (var item in entity.Groups) { item.GroupPolicy = entity; item.GroupPolicyId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.GroupPolicy, Models.GroupPolicy>()
                .ForMember(p => p.Groups, opt => opt.MapFrom(src => src.Groups));

            #endregion

            #region GroupPolicyGroup

            CreateMap<String, Entities.GroupPolicyGroup>()
                .ForMember(p => p.Group, opt => opt.MapFrom(src => src))
                .PreserveReferences();

            CreateMap<Entities.GroupPolicyGroup, String>()
                .ConstructUsing(p => p.Group);

            #endregion

            #region AggregatedPolicy

            CreateMap<Models.AggregatedPolicy, Entities.AggregatedPolicy>()
                .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
                .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
                .AfterMap((model, entity) => {
                    foreach (var item in entity.Policies) { item.AggregatedPolicy = entity; item.AggregatedPolicyId = entity.Id; }
                })
                .PreserveReferences();

            CreateMap<Entities.AggregatedPolicy, Models.AggregatedPolicy>()
                .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
                .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies));

            #endregion

            #region Policy

            CreateMap<Models.Policy, Entities.MmAggregatedPolicyPolicy>()
                .ForMember(p => p.PolicyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Policy, opt => opt.MapFrom(src => src));

            CreateMap<Entities.MmAggregatedPolicyPolicy, Models.Policy>()
                .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Policy>(p.Policy));

            #endregion

        }
    }

}
