using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Options {
    public class TableConfiguration {
        public String Name { get; set; }
        public String Schema { get; set; }

        public TableConfiguration(String name, String schema = null) {
            Name = name;
            Schema = schema;
        }

    }
}
