using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class Secret {

        public String Value { get; set; }

        public Secret(String value) {
            Value = value;
        }

    }
}
