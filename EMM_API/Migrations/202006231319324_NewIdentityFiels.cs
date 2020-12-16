namespace EMM_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewIdentityFiels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rate", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "Position", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Position");
            DropColumn("dbo.AspNetUsers", "Rate");
        }
    }
}
