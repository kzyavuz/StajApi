
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DTO.DTOs.EmployeeDTO
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Ad alanı zorunludur!")]
        [MinLength(3, ErrorMessage = "Personel adı en az 3  karekter olmalıdır!")]
        [MaxLength(50, ErrorMessage = "Personel adı en fazla 50  karekter olmalıdır!")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur!")]
        [MinLength(3, ErrorMessage = "Personel soyadı en az 3  karekter olmalıdır!")]
        [MaxLength(50, ErrorMessage = "Personel soyadı en fazla 50  karekter olmalıdır!")]
        public string EmployeeSurName { get; set; }

        [Required(ErrorMessage ="TCkimlik alanı zorunludur!")]
        [MinLength(11, ErrorMessage = "TC kimlik numarası 11 haneli olmalı!")]
        [MaxLength(11, ErrorMessage = "TC kimlik numarası 11 haneli olmalı!")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage ="Telefon numarası zorunludur!")]
        [MinLength(11, ErrorMessage = "Telefon numarası  11 haneli olmalı!")]
        [MaxLength(11, ErrorMessage = "Telefon numarası  11 haneli olmalı!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Doğum tarihi alanı zorunludur!")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Cinsiyet alanı zorunludur!")]
        public bool Gender { get; set; }

        public int? Age { get; set; }

        [MaxLength(50, ErrorMessage = "Kullanıcı adı en fazla 50  karekter olmalıdır!")]
        [MinLength(5, ErrorMessage = "Kullanıcı adı en az 5  karekter olmalıdır!")]
        [Required(ErrorMessage = "Kulanıcı adı alanı zorunludur!")]
        public string UserName { get; set; }

        public bool Status { get; set; }

        [Required(ErrorMessage = "E-Posta alanı zorunludur!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birim alanı zorunludur!")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Sınıf alanı zorunludur!")]
        public string Class { get; set; }

        [Required(ErrorMessage = "Görev alanı zorunludur!")]
        public string Task { get; set; }

        public int RoleID { get; set; }

        [Required(ErrorMessage = "Resim alanı zorunludur!")]
        public IFormFile? ProfilePicture { get; set; }

        [Required(ErrorMessage = "Sifre alanı zorunludur!")]
        [MinLength(6, ErrorMessage = "Sifre en az 6  karekter olmalıdır!")]
        [MaxLength(50, ErrorMessage = "Sifre en fazla 50  karekter olmalıdır!")]
        public string Password { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime ActiveDateTime { get; set; }
    }
}
