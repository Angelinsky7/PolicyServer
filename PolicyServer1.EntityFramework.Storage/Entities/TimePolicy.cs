using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyServer1.EntityFramework.Storage.Entities {
    public class TimePolicy : Policy {

        public Nullable<DateTime> NotBefore { get; set; }
        public Nullable<DateTime> NotOnOrAfter { get; set; }

        public Nullable<Int64> DayOfMonthId { get; set; }
        public TimePolicyRange DayOfMonth { get; set; }

        public Nullable<Int64> MonthId { get; set; }
        public TimePolicyRange Month { get; set; }

        public Nullable<Int64> YearId { get; set; }
        public TimePolicyRange Year { get; set; }

        public Nullable<Int64> HourId { get; set; }
        public TimePolicyRange Hour { get; set; }

        public Nullable<Int64> MinuteId{ get; set; }
        public TimePolicyRange Minute { get; set; }

    }
}
