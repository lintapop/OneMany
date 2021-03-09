using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OneMany.Models
{
    public class User
    {
        [Required]
        [Display(Name = "Customer email")]  //給後端開發者看
        [StringLength(30, ErrorMessage = "enter email address")]
        [EmailAddress(ErrorMessage = "not legit email format")]
        public string email { get; set; }

        [Key] //設定主key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //識別規格 可以遞增
        public int Id { get; set; }

        [Required] //必填欄位
        [Display(Name = "Customer name")]  //給後端開發者看
        public string name { get; set; }

        [Required]
        [Display(Name = "Customer password")]  //給後端開發者看
        [StringLength(50, ErrorMessage = "超過", MinimumLength = 6)] //nvarchar 限制密碼長度
        public string password { get; set; }

        [Required]
        [Display(Name = "Customer telephone")]  //給後端開發者看
        [StringLength(50, ErrorMessage = "超過", MinimumLength = 9)]
        public string tel { get; set; }

        public int departmentID { get; set; } //對應到下面的foreignkey

        //先設一個外部關聯的key 在把key傳到這個表單的另一個prop
        [ForeignKey("departmentID")]
        public virtual Department Department { get; set; }
    }
}