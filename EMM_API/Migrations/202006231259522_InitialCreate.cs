namespace EMM_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Rate");
            DropColumn("dbo.AspNetUsers", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Position", c => c.String());
            AddColumn("dbo.AspNetUsers", "Rate", c => c.Double());
        }
    }
}
