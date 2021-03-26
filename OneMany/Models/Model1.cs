using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace OneMany.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<User> Users { get; set; }

        //        -------------------view model---------------------

        //public virtual DbSet<LoginViewModel> LoginViewModels { get; set; }
        //建所有驗證功能的畫面  因為只要關聯一次畫面 之後不需要再接資料庫便可以取消關聯

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}