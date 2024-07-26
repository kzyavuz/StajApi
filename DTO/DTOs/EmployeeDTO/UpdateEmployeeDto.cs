namespace DTO.DTOs.EmployeeDTO
{
    public class UpdateEmployeeDto
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
