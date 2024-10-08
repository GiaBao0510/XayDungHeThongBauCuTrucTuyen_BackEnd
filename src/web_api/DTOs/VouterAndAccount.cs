using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.core.Interfaces;

namespace BackEnd.src.web_api.DTOs
{
    public class VouterAndAccount
    {
        [DisplayName("Identify candidates")]
        public string? ID_CuTri { set; get; }
        [Required]
        [DisplayName("Name")]
        [StringLength(50)]
        public required string HoTen { set; get; }
        [Required]
        [DisplayName("Sex")]
        [StringLength(1)]
        public required string GioiTinh { set; get; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of birth")]
        public DateTime NgaySinh { set; get; }
        [Required]
        [DisplayName("Address")]
        [StringLength(100)]
        public required string DiaChiLienLac { set; get; }
        [Required]
        [DisplayName("Citizen identication")]
        [StringLength(12)]
        public required string CCCD { set; get; }
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public required string SDT { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }
        [DisplayName("avatar")]
        [StringLength(12)]
        public string HinhAnh { set; get; } = "null";
        [ForeignKey("Roles")]
        public int RoleID { get; set; }
        public required string TaiKhoan { get; set; }
        [Required]
        [DisplayName("PassWord")]
        public required string MatKhau { get; set; }
        [DisplayName("OldPassword")]
        public string? MatKhauCu { get; set; }
        [StringLength(1)]
        [DisplayName("Looked")]
        public string BiKhoa { get; set; } = "1";
        [StringLength(100)]
        [DisplayName("Reason for lock")]
        public string LyDoKhoa { get; set; } = "null";
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date created")]
        public DateTime NgayTao { get; set; } = DateTime.Now;
        [DisplayName("Use")]
        public int SuDung { get; set; } = 1;
    }
}