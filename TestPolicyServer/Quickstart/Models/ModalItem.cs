using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPolicyServer.Quickstart {
    public class ModalItem {

        public String Title { get; set; }
        public String Caption { get; set; }

        public String OkButton { get; set; } = "Ok";
        public String CancelButton { get; set; } = "Cancel";

    }
}
