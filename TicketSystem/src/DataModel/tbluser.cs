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
    
    public partial class tbluser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbluser()
        {
            this.tbluserdepartments = new HashSet<tbluserdepartment>();
            this.tbluserinfoes = new HashSet<tbluserinfo>();
        }
    
        public int id { get; set; }
        public Nullable<int> managerid { get; set; }
        public string uname { get; set; }
        public string password { get; set; }
        public Nullable<int> createdby { get; set; }
        public Nullable<System.DateTime> createdon { get; set; }
        public Nullable<int> modifiedby { get; set; }
        public Nullable<System.DateTime> modifiedon { get; set; }
        public bool deleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbluserdepartment> tbluserdepartments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbluserinfo> tbluserinfoes { get; set; }
    }
}
