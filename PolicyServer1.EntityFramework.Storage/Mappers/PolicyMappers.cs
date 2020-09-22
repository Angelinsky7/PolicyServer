#define DEBUG_MAPPING

using AutoMapper;
using PolicyServer1.EntityFramework.Storage.Entities;
using PolicyServer1.EntityFramework.Storage.Extensions;
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
            Entities.Policy entityUpdate = model.ToEntity();
            if (entity.Id != entityUpdate.Id) { throw new ArgumentOutOfRangeException(nameof(Entities.Policy.Id)); }

            ObjectExtensions.UpdateIfDifferent(entity.Name, entityUpdate.Name, p => entity.Name = p);
            ObjectExtensions.UpdateIfDifferent(entity.Description, entityUpdate.Description, p => entity.Description = p);
            ObjectExtensions.UpdateIfDifferent(entity.Logic, entityUpdate.Logic, p => entity.Logic = p);

            //if (entity.Name != entityUpdate.Name) { entity.Name = entityUpdate.Name; }
            //if (entity.Description != entityUpdate.Description) { entity.Description = entityUpdate.Description; }
            //if (entity.Logic != entityUpdate.Logic) { entity.Logic = entityUpdate.Logic; }

            if (entity is RolePolicy rolePolicy) {
                foreach (Entities.MmRolePolicyRole role in rolePolicy.Roles.Where(p => !(entityUpdate as Entities.RolePolicy).Roles.Any(a => a.RoleId == p.RoleId && a.RolePolicyId == p.RolePolicyId)).ToList()) {
                    rolePolicy.Roles.Remove(role);
                }
                foreach (Entities.MmRolePolicyRole role in (entityUpdate as Entities.RolePolicy).Roles.Where(p => !rolePolicy.Roles.Any(a => a.RoleId == p.RoleId && a.RolePolicyId == p.RolePolicyId)).ToList()) {
                    rolePolicy.Roles.Add(role);
                }
            } else if (entity is TimePolicy timePolicy) {
                if (timePolicy.NotBefore != (entityUpdate as TimePolicy).NotBefore) { timePolicy.NotBefore = (entityUpdate as TimePolicy).NotBefore; }
                if (timePolicy.NotOnOrAfter != (entityUpdate as TimePolicy).NotOnOrAfter) { timePolicy.NotOnOrAfter = (entityUpdate as TimePolicy).NotOnOrAfter; }

                UpdateTimePolicyRange(timePolicy.DayOfMonth, (entityUpdate as TimePolicy).DayOfMonth, p => timePolicy.DayOfMonth = p);
                UpdateTimePolicyRange(timePolicy.Month, (entityUpdate as TimePolicy).Month, p => timePolicy.Month = p);
            }

        }

        private static void UpdateTimePolicyRange(Entities.TimePolicyRange src, Entities.TimePolicyRange target, Action<Entities.TimePolicyRange> updateAction) {

            //TODO(demarco): i've got an issue because i don't manage the id...
            // now the question is: do i got get the correct value from the db or do i manage the id (or i find a better solution that this crap)
            if (src?.Id == null || target?.Id == null) {
                updateAction(target);
            } else {
                if (src.From != target.From) { src.From = target.From; src.Updated = DateTime.UtcNow; }
                if (src.To != target.To) { src.To = target.To; src.Updated = DateTime.UtcNow; }
            }
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
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Logic = model.Logic,
                Roles = (model as Models.RolePolicy).Roles.AsQueryable().Select(MmRolePolicyRole.Transform).ToList()
            } : (model as Models.TimePolicy) != null ? (Entities.Policy)new Entities.TimePolicy {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Logic = model.Logic,
                NotBefore = (model as Models.TimePolicy).NotBefore,
                NotOnOrAfter = (model as Models.TimePolicy).NotOnOrAfter,
                DayOfMonth = (model as Models.TimePolicy).DayOfMonth != null ? TimePolicyRange.ToEntity((model as Models.TimePolicy).DayOfMonth) : null,
                Month = (model as Models.TimePolicy).Month != null ? TimePolicyRange.ToEntity((model as Models.TimePolicy).Month) : null,
                Year = (model as Models.TimePolicy).Year != null ? TimePolicyRange.ToEntity((model as Models.TimePolicy).Year) : null,
                Hour = (model as Models.TimePolicy).Hour != null ? TimePolicyRange.ToEntity((model as Models.TimePolicy).Hour) : null,
                Minute = (model as Models.TimePolicy).Minute != null ? TimePolicyRange.ToEntity((model as Models.TimePolicy).Minute) : null
            } : (model as Models.ClientPolicy) != null ? (Entities.Policy)new Entities.ClientPolicy {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Logic = model.Logic,
                Clients = (model as Models.ClientPolicy).Clients.Select(p => new ClientPolicyClient {
                    Client = p
                }).ToList()
            } : (model as Models.UserPolicy) != null ? (Entities.Policy)new Entities.UserPolicy {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Logic = model.Logic,
                Users = (model as Models.UserPolicy).Users.Select(p => new UserPolicyUser {
                    User = p
                }).ToList()
            } : (model as Models.GroupPolicy) != null ? (Entities.Policy)new Entities.GroupPolicy {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Logic = model.Logic,
                Groups = (model as Models.GroupPolicy).Groups.Select(p => new GroupPolicyGroup {
                    Group = p
                }).ToList()
            } : (model as Models.AggregatedPolicy) != null ? (Entities.Policy)new Entities.AggregatedPolicy {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Logic = model.Logic,
                Policies = (model as Models.AggregatedPolicy).Policies.AsQueryable().Select(MmAggregatedPolicyPolicy.Transform).ToList()
            } : null;
            internal static Entities.Policy ToEntity(Models.Policy entity) => Transform.Compile().Invoke(entity);
            internal static Models.Policy ToModel(Entities.Policy entity) => Projection.Compile().Invoke(entity);
        }

        internal class MmRolePolicyRole {
            internal static Expression<Func<Entities.MmRolePolicyRole, Models.Role>> Projection => entity => RoleMappers.Role.ToModel(entity.Role);
            internal static Expression<Func<Models.Role, Entities.MmRolePolicyRole>> Transform => model => new Entities.MmRolePolicyRole {
                RoleId = model.Id,
                Role = RoleMappers.Role.ToEntity(model)
            };
        }

        internal class MmAggregatedPolicyPolicy {
            internal static Expression<Func<Entities.MmAggregatedPolicyPolicy, Models.Policy>> Projection => entity => PolicyMappers.Policy.ToModel(entity.Policy);
            internal static Expression<Func<Models.Policy, Entities.MmAggregatedPolicyPolicy>> Transform => model => new Entities.MmAggregatedPolicyPolicy {
                PolicyId = model.Id,
                Policy = PolicyMappers.Policy.ToEntity(model)
            };
        }

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

    //public class PolicyMapperProfile : Profile {
    //    public PolicyMapperProfile() {

    //        #region Policy

    //        CreateMap<Models.Policy, Entities.Policy>()
    //            .Include<Models.RolePolicy, Entities.RolePolicy>()
    //            .Include<Models.TimePolicy, Entities.TimePolicy>()
    //            .Include<Models.ClientPolicy, Entities.ClientPolicy>()
    //            .Include<Models.UserPolicy, Entities.UserPolicy>()
    //            .Include<Models.GroupPolicy, Entities.GroupPolicy>()
    //            .Include<Models.AggregatedPolicy, Entities.AggregatedPolicy>()

    //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
    //            .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
    //            .ForMember(p => p.Logic, opt => opt.MapFrom(src => src.Logic))
    //            .PreserveReferences();

    //        CreateMap<Entities.Policy, Models.Policy>()
    //            .Include<Entities.RolePolicy, Models.RolePolicy>()
    //            .Include<Entities.TimePolicy, Models.TimePolicy>()
    //            .Include<Entities.ClientPolicy, Models.ClientPolicy>()
    //            .Include<Entities.UserPolicy, Models.UserPolicy>()
    //            .Include<Entities.GroupPolicy, Models.GroupPolicy>()
    //            .Include<Entities.AggregatedPolicy, Models.AggregatedPolicy>()

    //            .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
    //            .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
    //            .ForMember(p => p.Logic, opt => opt.MapFrom(src => src.Logic))
    //            .PreserveReferences();

    //        #endregion

    //        #region RolePolicy

    //        CreateMap<Models.RolePolicy, Entities.RolePolicy>()
    //            .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
    //            .AfterMap((model, entity) => {
    //                foreach (Entities.MmRolePolicyRole item in entity.Roles) { item.RolePolicy = entity; item.RolePolicyId = entity.Id; }
    //            })
    //            .PreserveReferences();

    //        CreateMap<Entities.RolePolicy, Models.RolePolicy>()
    //            .ForMember(p => p.Roles, opt => opt.MapFrom(src => src.Roles))
    //            .PreserveReferences();

    //        #endregion

    //        #region Role

    //        CreateMap<Models.Role, Entities.MmRolePolicyRole>()
    //            .ForMember(p => p.RoleId, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(p => p.Role, opt => opt.MapFrom(src => src));

    //        CreateMap<Entities.MmRolePolicyRole, Models.Role>()
    //            .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Role>(p.Role));

    //        #endregion

    //        #region TimePolicy

    //        CreateMap<Models.TimePolicy, Entities.TimePolicy>()
    //            .ForMember(p => p.NotBefore, opt => opt.MapFrom(src => src.NotBefore))
    //            .ForMember(p => p.NotOnOrAfter, opt => opt.MapFrom(src => src.NotOnOrAfter))
    //            .ForMember(p => p.DayOfMonth, opt => opt.MapFrom(src => src.DayOfMonth))
    //            .ForMember(p => p.Month, opt => opt.MapFrom(src => src.Month))
    //            .ForMember(p => p.Year, opt => opt.MapFrom(src => src.Year))
    //            .ForMember(p => p.Hour, opt => opt.MapFrom(src => src.Hour))
    //            .ForMember(p => p.Minute, opt => opt.MapFrom(src => src.Minute))
    //            .PreserveReferences();

    //        CreateMap<Entities.TimePolicy, Models.TimePolicy>()
    //            .ForMember(p => p.NotBefore, opt => opt.MapFrom(src => src.NotBefore))
    //            .ForMember(p => p.NotOnOrAfter, opt => opt.MapFrom(src => src.NotOnOrAfter))
    //            .ForMember(p => p.DayOfMonth, opt => opt.MapFrom(src => src.DayOfMonth))
    //            .ForMember(p => p.Month, opt => opt.MapFrom(src => src.Month))
    //            .ForMember(p => p.Year, opt => opt.MapFrom(src => src.Year))
    //            .ForMember(p => p.Hour, opt => opt.MapFrom(src => src.Hour))
    //            .ForMember(p => p.Minute, opt => opt.MapFrom(src => src.Minute))
    //            .PreserveReferences();

    //        #endregion

    //        #region TimePolicyRange

    //        CreateMap<Models.TimePolicyRange, Entities.TimePolicyRange>()
    //            .ForMember(p => p.From, opt => opt.MapFrom(src => src.From))
    //            .ForMember(p => p.To, opt => opt.MapFrom(src => src.To))
    //            .PreserveReferences();

    //        CreateMap<Entities.TimePolicyRange, Models.TimePolicyRange>()
    //            .ConstructUsing(p => new Models.TimePolicyRange {
    //                From = p.From,
    //                To = p.To
    //            })
    //            .PreserveReferences();

    //        #endregion

    //        #region ClientPolicy

    //        CreateMap<Models.ClientPolicy, Entities.ClientPolicy>()
    //            .ForMember(p => p.Clients, opt => opt.MapFrom(src => src.Clients))
    //            .AfterMap((model, entity) => {
    //                foreach (var item in entity.Clients) { item.ClientPolicy = entity; item.ClientPolicyId = entity.Id; }
    //            })
    //            .PreserveReferences();

    //        CreateMap<Entities.ClientPolicy, Models.ClientPolicy>()
    //            .ForMember(p => p.Clients, opt => opt.MapFrom(src => src.Clients));

    //        #endregion

    //        #region ClientPolicyClient

    //        CreateMap<String, Entities.ClientPolicyClient>()
    //            .ForMember(p => p.Client, opt => opt.MapFrom(src => src))
    //            .PreserveReferences();

    //        CreateMap<Entities.ClientPolicyClient, String>()
    //            .ConstructUsing(p => p.Client);

    //        #endregion

    //        #region UserPolicy

    //        CreateMap<Models.UserPolicy, Entities.UserPolicy>()
    //            .ForMember(p => p.Users, opt => opt.MapFrom(src => src.Users))
    //            .AfterMap((model, entity) => {
    //                foreach (var item in entity.Users) { item.UserPolicy = entity; item.UserPolicyId = entity.Id; }
    //            })
    //            .PreserveReferences();

    //        CreateMap<Entities.UserPolicy, Models.UserPolicy>()
    //            .ForMember(p => p.Users, opt => opt.MapFrom(src => src.Users));

    //        #endregion

    //        #region UserPolicyUser

    //        CreateMap<String, Entities.UserPolicyUser>()
    //            .ForMember(p => p.User, opt => opt.MapFrom(src => src))
    //            .PreserveReferences();

    //        CreateMap<Entities.UserPolicyUser, String>()
    //            .ConstructUsing(p => p.User);

    //        #endregion

    //        #region GroupPolicy

    //        CreateMap<Models.GroupPolicy, Entities.GroupPolicy>()
    //            .ForMember(p => p.Groups, opt => opt.MapFrom(src => src.Groups))
    //            .AfterMap((model, entity) => {
    //                foreach (var item in entity.Groups) { item.GroupPolicy = entity; item.GroupPolicyId = entity.Id; }
    //            })
    //            .PreserveReferences();

    //        CreateMap<Entities.GroupPolicy, Models.GroupPolicy>()
    //            .ForMember(p => p.Groups, opt => opt.MapFrom(src => src.Groups));

    //        #endregion

    //        #region GroupPolicyGroup

    //        CreateMap<String, Entities.GroupPolicyGroup>()
    //            .ForMember(p => p.Group, opt => opt.MapFrom(src => src))
    //            .PreserveReferences();

    //        CreateMap<Entities.GroupPolicyGroup, String>()
    //            .ConstructUsing(p => p.Group);

    //        #endregion

    //        #region AggregatedPolicy

    //        CreateMap<Models.AggregatedPolicy, Entities.AggregatedPolicy>()
    //            .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
    //            .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies))
    //            .AfterMap((model, entity) => {
    //                foreach (var item in entity.Policies) { item.AggregatedPolicy = entity; item.AggregatedPolicyId = entity.Id; }
    //            })
    //            .PreserveReferences();

    //        CreateMap<Entities.AggregatedPolicy, Models.AggregatedPolicy>()
    //            .ForMember(p => p.DecisionStrategy, opt => opt.MapFrom(src => src.DecisionStrategy))
    //            .ForMember(p => p.Policies, opt => opt.MapFrom(src => src.Policies));

    //        #endregion

    //        #region Policy

    //        CreateMap<Models.Policy, Entities.MmAggregatedPolicyPolicy>()
    //            .ForMember(p => p.PolicyId, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(p => p.Policy, opt => opt.MapFrom(src => src));

    //        CreateMap<Entities.MmAggregatedPolicyPolicy, Models.Policy>()
    //            .ConstructUsing((p, ctx) => ctx.Mapper.Map<Models.Policy>(p.Policy));

    //        #endregion

    //    }

}
