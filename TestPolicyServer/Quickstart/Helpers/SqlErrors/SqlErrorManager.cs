using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TestPolicyServer.Quickstart {

    public class SqlErrorManager {

        private static readonly Lazy<SqlErrorManager> s_Instance = new Lazy<SqlErrorManager>(() => new SqlErrorManager());
        public static SqlErrorManager Instance => s_Instance.Value;

        private readonly Dictionary<Int32, ISqlError> _SqlErrorByErrors = new Dictionary<Int32, ISqlError>();

        private SqlErrorManager() {
            PopulateSqlErrorLookup();
        }

        public String FormatError(Exception ex) {
            DbUpdateException dbUpdateEx = ex as DbUpdateException;
            SqlException sqlEx = dbUpdateEx?.InnerException as SqlException;
            if (!_SqlErrorByErrors.ContainsKey(sqlEx.Number)) {
#if DEBUG
                Console.WriteLine($"Cannot handle this error number: {sqlEx.Number}");
#endif
                return null;
            }
            return _SqlErrorByErrors[sqlEx.Number].Formatter(sqlEx, dbUpdateEx.Entries);
        }

        private void PopulateSqlErrorLookup() {
            foreach (TypeInfo typeInfo in typeof(SqlErrorManager).Assembly.DefinedTypes) {
                if (typeInfo.ImplementedInterfaces.Contains(typeof(ISqlError))) {
                    ISqlError handler = (ISqlError)Activator.CreateInstance(typeInfo.AsType());
                    _SqlErrorByErrors.Add(handler.SqlServerErrorNumber, handler);
                }
            }
        }
    }
}
