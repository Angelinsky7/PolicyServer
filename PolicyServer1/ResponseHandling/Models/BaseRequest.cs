using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.ResponseHandling.Models {
    public class BaseRequest {

        public Boolean IsError { get; set; }
        public String Error { get; set; }
        public String ErrorDescription { get; set; }

    }
}
