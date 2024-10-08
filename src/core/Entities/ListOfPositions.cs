using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;

namespace BackEnd.core.Entities
{
    [Table("DanhMucUngCu")]
    public class ListOfPositions
    {
        [Key]
        public int? ID_Cap { get; set; }
        [Required]
        [DisplayName("Position name")]
        [StringLength(50)]
        public required string TenCapUngCu { get; set; }
        
        //Khóa ngoại
        public int? ID_DonViBauCu { get; set; }
        [ForeignKey("ID_DonViBauCu")]
        public Constituency consistency { get; set;}

        //Truy xuất ngược
        public virtual ICollection<ElectionResults> electionResults{set;get;}
        
        public virtual ICollection<Vote> vote{set;get;}

        //Cho các lớp truy xuất ngược trả về mảng rỗng khi mảng rỗng
        public ListOfPositions(){
            vote = new List<Vote>();
            electionResults = new List<ElectionResults>();
        }
        
    }
}
 
