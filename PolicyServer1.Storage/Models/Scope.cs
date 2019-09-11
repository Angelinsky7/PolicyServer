using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class Scope {

        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public String DisplayName { get; set; }
        public Uri IconUri { get; set; }

    }
}