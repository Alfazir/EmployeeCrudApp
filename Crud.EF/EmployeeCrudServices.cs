using Crud.Domain.Model;
using Crud.Domain.Services;
using Crud.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crud.EF
{
    public class EmployeeCrudServices : ICrud
    {
        private readonly IDataService<Employee> _crudServices;

        public EmployeeCrudServices()
        {
            _crudServices = new GenericDataService<Employee>(new EmployeerContextFactory());
        }

        public async  Task<Employee> AddEmployee(string EmployeeName, string EmployeeSurname,
            string EmployeePatronymic, string? EmployeePosition = default, string? EmployeeDate = default
            , string? EmployeePhone = default, string? EmployeeEmail = default, string? EmployeeDepartment = default)
        {


            try
            {
                if (EmployeeName == string.Empty || EmployeeSurname == string.Empty || EmployeePatronymic ==string.Empty)
                {
                    throw new Exception("Обязательные поля должны быть заполнены!");
                }

                else
                {
                    if (EmployeeDate == "")
                    {
                        EmployeeDate = "01.01.0001";
                    }

                    Employee employee = new Employee
                    {
                        EmployeeName = EmployeeName,
                        EmployeeSurname = EmployeeSurname,
                        EmployeePatronymic = EmployeePatronymic,
                        EmployeePosition= EmployeePosition,
                        EmployeeDate= DateTime.Parse(EmployeeDate),
                       EmployeePhone = EmployeePhone,
                       EmployeeEmail = EmployeeEmail,
                       EmployeeDepartment= EmployeeDepartment
                    };
                    return await _crudServices.Create(employee);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                
            }
        }

        public Task<Employee> SearchEmployeeID(int ID)
        {
            try
            {
                return _crudServices.Get(ID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                Employee delete = await SearchEmployeeID(id);
                return await _crudServices.Delete(delete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public async Task<ICollection<Employee>> ListEmployees ()
        {
            try 
            {
                return (ICollection<Employee>)await _crudServices.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Employee>> SearchEmployeeByName (string EmployeeName)
        {
            try
            {
                var listemployees = await ListEmployees();
                return listemployees.Where(x => x.EmployeeName.StartsWith(EmployeeName)).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task <Employee> UpdateEmployee (int id, string EmployeeName)
        {
            try
            {
                Employee emp = await SearchEmployeeID(id);
                emp.EmployeeName = EmployeeName;
                return await _crudServices.Update(emp);

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }



    }
}
