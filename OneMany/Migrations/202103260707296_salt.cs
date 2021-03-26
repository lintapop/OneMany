namespace OneMany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordSalt", c => c.String(maxLength: 100));
            AddColumn("dbo.Users", "Authority", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Authority");
            DropColumn("dbo.Users", "PasswordSalt");
        }
    }
}
