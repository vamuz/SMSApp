namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecontituencytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Constituencies", "CountyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Constituencies", "CountyId");
        }
    }
}
