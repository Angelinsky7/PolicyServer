using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TestPolicyServer.Quickstart {
    public class SqlErrorDuplicateKeyRow : ISqlError {

        private static readonly Regex _testConstraintRegex = new Regex(@"Cannot insert duplicate key row in object '([a-zA-Z0-9_\.]+)' with unique index '([a-zA-Z0-9_]+)'. The duplicate key value is \(([^)]+)\).", RegexOptions.Compiled);

        public Int32 SqlServerErrorNumber => 2601;

        public String Formatter(SqlException ex, IReadOnlyList<EntityEntry> entitiesNotSaved) {
            String message = ex.Errors[0].Message;
            MatchCollection matches = _testConstraintRegex.Matches(message);

            if (matches.Count == 0) { return null; }

            //currently the entitiesNotSaved is empty for unique constraints - see https://github.com/aspnet/EntityFrameworkCore/issues/7829
            String entityDisplayName = entitiesNotSaved.Count == 1
                ? entitiesNotSaved.Single().Entity.GetType().Name
                : matches[0].Groups[1].Value;

            entityDisplayName = entityDisplayName.RemoveSchema();

            String keyname = matches[0].Groups[3].Value;


            String returnError = $"Cannot create this new {entityDisplayName} because another {entityDisplayName} has already the same unique key: '{keyname}'.";

            //Int32 openingBadValue = message.IndexOf("(");
            //if (openingBadValue > 0) {
            //    String dupPart = message.Substring(openingBadValue + 1,
            //        message.Length - openingBadValue - 3);
            //    returnError += $" Duplicate value was '{dupPart}'.";
            //}

            return returnError;
        }

    }
}
