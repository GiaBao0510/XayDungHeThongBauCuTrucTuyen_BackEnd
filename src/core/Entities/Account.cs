using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.core.Entities
{
    [Table("TaiKhoan")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required string TaiKhoan { get; set; }
        [Required]
        [DisplayName("PassWord")]
        public required string MatKhau { get; set; }
        [StringLength(1)]
        [DisplayName("Looked")]
        public string BiKhoa { get; set; } = "0";
        [StringLength(100)]
        [DisplayName("Reason for lock")]
        public string? LyDoKhoa { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date created")]
        public DateTime NgayTao { get; set; } = DateTime.Now;
        [DisplayName("Use")]
        public int SuDung { get; set; } = 1;

        //Khóa ngoại
        public int? RoleID { set; get; }
        [ForeignKey("RoleID")]
        public Roles role {set;get;}

        //Truy xuất ngược
        public virtual ICollection<LoginHistory> loginHistories {set;get;}

        //Tạo hàm khởi tạo với mục đích cho các đối trượng truy xuất ngược trả về mảng rỗng
        public Account(){
            loginHistories = new List<LoginHistory>();
        }
    }
}
