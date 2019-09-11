using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.Models {
    public class EvaluationItem {

        public Guid RsId { get; set; }
        public String RsName { get; set; }

        public ICollection<String> Scopes { get; set; } = new HashSet<String>();

    }

}
