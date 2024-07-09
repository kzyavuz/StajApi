namespace StajApi.DTOs
{
    public class CreateWorkDto
    {
        public int WorkID { get; set; }
        public string WorkName { get; set; }
        public int EmployeID { get; set; }
        public int DealerID { get; set; }
        public bool Status { get; set; }

    }
}
