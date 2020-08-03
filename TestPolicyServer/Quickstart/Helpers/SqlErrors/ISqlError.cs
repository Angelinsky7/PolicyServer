using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TestPolicyServer.Quickstart {
    public interface ISqlError {

        public Int32 SqlServerErrorNumber { get; }
        public String Formatter(SqlException ex, IReadOnlyList<EntityEntry> entitiesNotSaved);

    }
}
