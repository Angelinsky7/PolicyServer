using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class ResourceComparer : IEqualityComparer<Resource> {

        private static Lazy<ResourceComparer> s_Instance = new Lazy<ResourceComparer>(() => new ResourceComparer());
        public static ResourceComparer Instance => s_Instance.Value;

        private ResourceComparer() { }

        public Boolean Equals(Resource x, Resource y) => x.Id == y.Id;
        public Int32 GetHashCode(Resource obj) => obj.Id.GetHashCode();
    }

    public class RoleComparer : IEqualityComparer<Role> {

        private static Lazy<RoleComparer> s_Instance = new Lazy<RoleComparer>(() => new RoleComparer());
        public static RoleComparer Instance => s_Instance.Value;

        private RoleComparer() { }

        public Boolean Equals(Role x, Role y) => x.Id == y.Id;
        public Int32 GetHashCode(Role obj) => obj.Id.GetHashCode();
    }

    public class ScopeComparer : IEqualityComparer<Scope> {

        private static Lazy<ScopeComparer> s_Instance = new Lazy<ScopeComparer>(() => new ScopeComparer());
        public static ScopeComparer Instance => s_Instance.Value;

        private ScopeComparer() { }

        public Boolean Equals(Scope x, Scope y) => x.Id == y.Id;
        public Int32 GetHashCode(Scope obj) => obj.Id.GetHashCode();
    }

    public class PolicyComparer : IEqualityComparer<Policy> {

        private static Lazy<PolicyComparer> s_Instance = new Lazy<PolicyComparer>(() => new PolicyComparer());
        public static PolicyComparer Instance => s_Instance.Value;

        private PolicyComparer() { }

        public Boolean Equals(Policy x, Policy y) => x.Id == y.Id;
        public Int32 GetHashCode(Policy obj) => obj.Id.GetHashCode();
    }

}
