using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneMany.Models
{
    public class LoginViewModel
    {
        [Key] //設定主key
        public int Id { get; set; }

        [Display(Name = "帳號")]
        [Required(ErrorMessage = "{0}必填")] //必填欄位
        [MaxLength(50)]
        public string Account { get; set; }

        [Display(Name = "密碼")]  //給後端開發者看
        [Required(ErrorMessage = "{0}必填")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0}長度必須介於6-100", MinimumLength = 6)] //nvarchar 限制密碼長度
        public string password { get; set; }
    }
}