using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class UserDepartmentEntity
    {
        public int id { get; set; }

        public Nullable<int> userid { get; set; }

        public Nullable<int> deptid { get; set; }

        public Nullable<int> createdby { get; set; }

        public Nullable<System.DateTime> createdon { get; set; }

        public Nullable<int> modifiedby { get; set; }

        public Nullable<System.DateTime> modifiedon { get; set; }

        public bool deleted { get; set; }



        public  DepartmentEntity tbldepartment { get; set; }

        public  UserEntity tbluser { get; set; }
    }
}
