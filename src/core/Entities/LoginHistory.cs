using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.core.Entities
{
    [Table("LichSuDangNhap")]
    public class LoginHistory
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Time")]
        public DateTime ThoiDiem { get; set; }
        [DisplayName("IP Address")]
        public required string DiaChiIP { get; set; }
        
        //khóa ngoại
        public string? TaiKhoan { get; set; }
        [ForeignKey("TaiKhoan")]
        public Account _account {set;get;}
    }
}
