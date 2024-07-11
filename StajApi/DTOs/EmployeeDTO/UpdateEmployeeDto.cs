namespace StajApi.DTOs.EMployeeDTO
{
    public class UpdateEmployeeDto
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurName { get; set; }
        public bool Status { get; set; }
    }
}
