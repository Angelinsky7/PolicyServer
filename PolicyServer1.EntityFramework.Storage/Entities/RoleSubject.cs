using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class RoleSubject {
        public Int32 Id { get; set; }
        public String Subject { get; set; }

        public Int32 RoleId { get; set; }
        public Role Role { get; set; }
    }
}
