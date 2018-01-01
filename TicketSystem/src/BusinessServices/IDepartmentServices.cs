using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IDepartmentServices
    {
        DepartmentEntity GetDepartmentById(int departmentId);
        IEnumerable<DepartmentEntity> GetAllDepartments();
        int CreateDepartment(DepartmentEntity departmentEntity);
        bool UpdateDepartment(int departmentId, DepartmentEntity departmentEntity);
        bool DeleteDepartment(int departmentId);
    }
}
