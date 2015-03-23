namespace taskman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "login", c => c.String(nullable: false, maxLength: 64));
            CreateIndex("dbo.User", "login", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "login" });
            AlterColumn("dbo.User", "login", c => c.String(nullable: false));
        }
    }
}
