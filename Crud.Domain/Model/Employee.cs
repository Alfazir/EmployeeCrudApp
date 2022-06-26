using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crud.Domain.Model
{
    /* Сведения о сотруднике должны включать в себя: фамилию, имя, отчество,
      должность, дату рождения, номер телефона, адрес электронной почты и название отдела компании.*/
    public class Employee: IDataErrorInfo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указаны Ф.И.О")]
        public string? EmployeeName { get; set; }= default(string);
        public string? EmployeeSurname { get; set; }
        public string? EmployeePatronymic { get; set; }
        public string? EmployeePosition { get; set; }
        public DateTime? EmployeeDate { get; set; }
        public string? EmployeePhone { get; set; }
        public string? EmployeeEmail { get; set; }
        public string? EmployeeDepartment { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string this [string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "EmployeeDate":
                        if (EmployeeName != null)
                        {
                            error = EmployeeName;
                        }
                        break;
                    case "EmployeeName":
                        if (EmployeeName != null )
                        {
                           if (EmployeeName.Length < 1) 
                                error = "Имя не заполнено!";
                            
                        }
                        else
                        {
                            

                            error = "Имя не заполнено!";
                        }

                        break;
                    case "EmployeeSurname":
                        if (EmployeeSurname != null )
                        {
                            if (EmployeeSurname.Length < 1)
                                error = "Фамилия не заполнена!";
                            
                        }
                        else
                        {
                            error = "Фамилия не заполнена!";
                        }
                        break;
                    case "EmployeePatronymic":
                        if (EmployeePatronymic != null )
                        {
                            if (EmployeePatronymic.Length < 1)
                                error = "Отчество не заполнено!";
                            
                        }
                        else
                        {
                            error = "Отчество не заполнено!";
                        }
                        break;
                    case "EmployeePhone":
                        string pattern = @"(^\+\d{1,2})?((\(\d{3}\))|(\-?\d{3}\-)|(\d{3}))((\d{3}\-\d{4})|(\d{3}\-\d\d\  
-\d\d)|(\d{7})|(\d{3}\-\d\-\d{3}))";
                        if (EmployeePhone!=null )
                        {
                            if (Regex.IsMatch(EmployeePhone, pattern) != true)
                                error = "Неверный номер!";
                        }
                        else
                        {
                            error = "Неверный номер!";
                        }

                        break;
                }
                return error;

            }
            
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

    }
}
