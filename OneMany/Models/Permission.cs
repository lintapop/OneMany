using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OneMany.Models
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "名稱")]
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]
        public string Name { get; set; }

        public int? Pid { get; set; }

        [Display(Name = "父權限")]
        [ForeignKey("Pid")] //uppercase
        public virtual Permission Permissions { get; set; }

        [Display(Name = "子權限")]
        public virtual ICollection<Permission> PermissionSon { get; set; } //擁有共同FK的集合

        [Display(Name = "權限代號")]
        public string PValue { get; set; }

        [Display(Name = "連結")]
        [Required(ErrorMessage = "{0}必填")]
        public string Url { get; set; }  //主選單 把網址放這邊
    }
}