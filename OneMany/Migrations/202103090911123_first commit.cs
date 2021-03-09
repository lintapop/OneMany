namespace OneMany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstcommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        dpname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 30),
                        name = c.String(nullable: false),
                        password = c.String(nullable: false, maxLength: 50),
                        tel = c.String(nullable: false, maxLength: 50),
                        departmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.departmentID, cascadeDelete: true)
                .Index(t => t.departmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "departmentID", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "departmentID" });
            DropTable("dbo.Users");
            DropTable("dbo.Departments");
        }
    }
}
