namespace EducationEfMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedphoneandemailtostudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Phone", c => c.String());
            AddColumn("dbo.Students", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Email");
            DropColumn("dbo.Students", "Phone");
        }
    }
}
