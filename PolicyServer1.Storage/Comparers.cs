using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PolicyServer1.Models;

namespace PolicyServer1 {
    public class ResourceComparer : IEqualityComparer<Resource> {

        private static Lazy<ResourceComparer> s_Instance = new Lazy<ResourceComparer>(() => new ResourceComparer());
        public static ResourceComparer Instance => s_Instance.Value;

        private ResourceComparer() { }

        public Boolean Equals(Resource x, Resource y) => x.Id == y.Id;
        public Int32 GetHashCode(Resource obj) => obj.Id.GetHashCode();
    }
}
