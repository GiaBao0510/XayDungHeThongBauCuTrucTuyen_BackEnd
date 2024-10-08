using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.core.Entities
{
    [Table("ChiTietThongBaoCuTri")]
    public class VoterNoticeDetails
    {
        public int ID_ThongBao { set; get; }
        [ForeignKey("ID_ThongBao")]
        public Notifications notifications{get; set;}
        public string? ID_CuTri { set; get; }
        [ForeignKey("ID_CuTri")]
        public Voter voter {set;get;}
    }
}
