using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.core.Entities
{
    [Table("QuanHuyen")]
    public class District
    {
        [Key]
        [DisplayName("ID_District")]
        public int ID_QH { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("District name")]
        public required string TenQH { get; set; }

        //Khóa ngoại
        [ForeignKey("Province")]
        public int STT { get; set; }

        //Truy xuất ngược
        public virtual ICollection<TemporaryAddress> temporaryAddress {set;get;}
        public virtual ICollection<PermanentAddress> permanentAddress {set;get;}
        public virtual ICollection<Constituency> constituency {set;get;}

        //Cho các lớp truy xuất ngược trả về mảng rỗng
        public District(){
            temporaryAddress = new List<TemporaryAddress>();
            permanentAddress = new List<PermanentAddress>();
            constituency = new List<Constituency>();
        }
    }
}