namespace taskman.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_support : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "user_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Task", "user_id");
        }
    }
}
