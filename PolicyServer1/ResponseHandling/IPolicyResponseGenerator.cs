﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PolicyServer1.ResponseHandling.Models;

namespace PolicyServer1.ResponseHandling {
    public interface IPolicyResponseGenerator {

        Task<Dictionary<String, Object>> ProcessAsync(PolicyRequest policyRequest);

    }
}
