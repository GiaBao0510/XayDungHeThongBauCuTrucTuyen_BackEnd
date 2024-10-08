using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.src.core.Entities;

namespace BackEnd.core.Entities{

    [Table("DiaChiTamTru")]
    public class TemporaryAddress{
        public string? ID_user { set; get; }
        [ForeignKey("ID_user")]
        public Users user;
        public int? ID_QH { get; set; }
        [ForeignKey("ID_QH")]
        public District district{set;get;}
    }
}