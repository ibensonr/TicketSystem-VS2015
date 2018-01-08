using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using AutoMapper;
using DataModel;

namespace BusinessServices
{
    public class DepartmentServices:IDepartmentServices
    {
        private readonly UnitOfWork _unitOfWork;

        public DepartmentServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int CreateDepartment(DepartmentEntity departmentEntity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentEntity> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll().ToList();
            if (departments.Any())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tbldepartment, DepartmentEntity>();
                    
                });
                var mapper = config.CreateMapper();
                var departmentsModel = mapper.Map<List<tbldepartment>, List<DepartmentEntity>>(departments);
                return departmentsModel;
            }
            return null;
        }

        public DepartmentEntity GetDepartmentById(int departmentId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDepartment(int departmentId, DepartmentEntity departmentEntity)
        {
            throw new NotImplementedException();
        }
    }
}
