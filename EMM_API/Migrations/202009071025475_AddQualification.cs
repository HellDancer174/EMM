namespace EMM_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQualification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "QualificationClass", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "QualificationClass");
        }
    }
}
