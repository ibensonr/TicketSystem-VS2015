using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class TicketHistoryEntity
    {
        public int id { get; set; }

        public Nullable<int> ticketid { get; set; }

        public Nullable<int> assignedtoid { get; set; }

        public string ticketstatusid { get; set; }

        public Nullable<int> status { get; set; }

        public Nullable<int> createdby { get; set; }

        public Nullable<System.DateTime> createdon { get; set; }

        public Nullable<int> modifiedby { get; set; }

        public Nullable<System.DateTime> modifiedon { get; set; }

        public bool deleted { get; set; }

        public Nullable<int> deptid { get; set; }

        public string subject { get; set; }

        public string description { get; set; }

        public string comment { get; set; }

        public UserEntity tbluser { get; set; }
    }
}
