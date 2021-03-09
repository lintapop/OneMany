using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OneMany.Models
{
    public class Department
    {
        [Key] //設定主key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //識別規格 可以遞增
        public int Id { get; set; }

        [Required] //必填欄位
        [Display(Name = "Customer name")]  //給後端開發者看
        public string dpname { get; set; }
    }
}