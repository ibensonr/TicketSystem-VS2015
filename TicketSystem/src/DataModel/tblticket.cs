//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblticket
    {
        public int id { get; set; }
        public Nullable<int> deptid { get; set; }
        public Nullable<int> ticketstatusid { get; set; }
        public string ticketname { get; set; }
        public string ticketdesc { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> createdby { get; set; }
        public Nullable<System.DateTime> createdon { get; set; }
        public Nullable<int> modifiedby { get; set; }
        public Nullable<System.DateTime> modifiedon { get; set; }
        public bool deleted { get; set; }
    
        public virtual tbldepartment tbldepartment { get; set; }
        public virtual tblticketstatus tblticketstatus { get; set; }
    }
}
