using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.WorkDTO
{
    public class UpdateWorkDto
    {
        public int WorkID { get; set; }

        [Required(ErrorMessage = "İş adı zorunludur!")]
        [MinLength(5, ErrorMessage = "İş adı en az 5  karekter olmalıdır!")]
        [MaxLength(50, ErrorMessage = "İş adı en fazla 50  karekter olmalıdır!")]
        public string WorkName { get; set; }

        [Required(ErrorMessage = "İş açıklaması zorunludur!")]
        [MinLength(50, ErrorMessage = "İş açıklaması en az 50  karekter olmalıdır!")]
        public string WorkDescription { get; set; }

        [Required(ErrorMessage = "Fiyat alanı zorunludur!")]
        [Range(1000, 1000000, ErrorMessage = "Fiyet 1.000 TL ile 1.000.000 TL arasında olmalıdır!")]
        public int WorkPrice { get; set; }

        [Required(ErrorMessage = "İlçe bilgisi zorunludur!")]
        [MinLength(2, ErrorMessage = "İlçe en az 2  karekter olmalıdır!")]
        [MaxLength(16, ErrorMessage = "İlçe en fazla 16  karekter olmalıdır!")]
        public string District { get; set; }

        [Required(ErrorMessage = "İl bilgisi zorunludur!")]
        [MinLength(3, ErrorMessage = "İl en az 3  karekter olmalıdır!")]
        [MaxLength(14, ErrorMessage = "İl en fazla 14  karekter olmalıdır!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Açık konum bilgisi zorunludur!")]
        public string WorkLocal { get; set; }

        public int? EmployeeID { get; set; }
        public bool Status { get; set; }
        public DateTime UpdateDateTime { get; set; }

    }
}
