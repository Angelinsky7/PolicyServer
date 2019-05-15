using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public abstract class Secret {

        public Int32 Id { get; set; }
        public String Value { get; set; }

    }
}
