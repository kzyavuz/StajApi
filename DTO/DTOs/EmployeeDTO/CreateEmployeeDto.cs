
namespace DTO.DTOs.EmployeeDTO
{
    public class CreateEmployeeDto
    {
        public string EmployeeName { get; set; }
        public string EmployeeSurName { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
