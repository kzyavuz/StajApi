
using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.DealerDTO
{
    public class CreateDealerDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Bayi Adı en az 5  karekter olmalıdır.")]
        [MaxLength(100, ErrorMessage = "Bayi Adı en fazla 100  karekter olmalıdır.")]
        public string DealerName { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Bayi Açıklaması en az 5  karekter olmalıdır.")]
        [MaxLength(1000, ErrorMessage = "Bayi Açıklaması en fazla 1000  karekter olmalıdır.")]
        public string DealerVariant { get; set; }
    }
}
