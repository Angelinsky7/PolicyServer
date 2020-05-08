using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models.Evalutation {
    public class ResourceResult {

        public String RsId { get; set; }
        public String RsName { get; set; }
        public IEnumerable<String> Scopes { get; set; } = new HashSet<String>();

    }
}
