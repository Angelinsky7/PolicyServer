using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class Scope {

        public Guid Id { get; set; } = Guid.NewGuid();

        public String Name { get; set; }
        public String DisplayName { get; set; }
        public String IconUri { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

        //public Boolean NonEditable { get; set; }

        //public Boolean Equals(Scope other) {
        //    if (other == null) { return false; }
        //    return Id == other.Id;
        //}
        //public override Boolean Equals(Object obj) => Equals(obj as Scope);
        //public override Int32 GetHashCode() => HashCode.Combine(Id);


        //public static Boolean operator ==(Scope a, Scope b) {
        //    if (ReferenceEquals(a, b)) { return true; }
        //    if (a is null) { return false; }
        //    if (b is null) { return false; }

        //    return a.Equals(b);
        //}
        //public static Boolean operator !=(Scope a, Scope b) => !(a == b);

    }
}
