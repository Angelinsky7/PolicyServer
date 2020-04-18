using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class TimePolicy {

        public Guid Id { get; set; }
        public Policy Base { get; set; }

        public Nullable<DateTime> NotBefore { get; set; }
        public Nullable<DateTime> NotOnOrAfter { get; set; }

        public Int64 DayOfMonthId { get; set; }
        public TimePolicyRange DayOfMonth { get; set; }

        public Int64 MonthId { get; set; }
        public TimePolicyRange Month { get; set; }

        public Int64 YearId { get; set; }
        public TimePolicyRange Year { get; set; }

        public Int64 HourId { get; set; }
        public TimePolicyRange Hour { get; set; }

        public Int64 MinuteId{ get; set; }
        public TimePolicyRange Minute { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }

    }
}
