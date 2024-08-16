using Microsoft.AspNetCore.Http;

namespace DTO.DTOs.EmployeeDTO
{
    public class ResultEmployeeDto
    {
        public int EmployeeID { get; set; }
        public int RoleID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string Age { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public bool Status2 { get; set; }
        public string Unit { get; set; }
        public string Class { get; set; }
        public string Task { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImage { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ActiveDateTime { get; set; }
        public DateTime PassiveDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
