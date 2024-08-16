using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.EmployeeDTO
{
    public class UpdtaeMyPasswordDto
    {
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Sifre alanı zorunludur!")]
        [MinLength(6, ErrorMessage = "Sifre en az 6  karekter olmalıdır!")]
        [MaxLength(50, ErrorMessage = "Sifre en fazla 50  karekter olmalıdır!")]
        public string Password { get; set; }
    }
}
