namespace StajApi.DTOs.DealerDTO
{
    public class UpdateDealerDto
    {
        public int DealerID { get; set; }
        public string DealerName { get; set; }
        public string DealerVariant { get; set; }
        public bool Status { get; set; }
    }
}
