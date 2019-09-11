using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyServer1.Models {

    public abstract class Policy {

        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public String Description { get; set; }

        public PolicyLogic Logic { get; set; }

        public abstract Task<Boolean> EvaluateAsync(IEvaluatorRequest request);

    }

}