using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UserEntity
    {
        public int id { get; set; }
        public Nullable<int> managerid { get; set; }
        public string uname { get; set; }
        public string password { get; set; }
        public Nullable<int> createdby { get; set; }
        public Nullable<System.DateTime> createdon { get; set; }
        public Nullable<int> modifiedby { get; set; }
        public Nullable<System.DateTime> modifiedon { get; set; }
        public bool deleted { get; set; }
        public ICollection<DepartmentEntity> tbldepartments { get; set; }
    }
}
