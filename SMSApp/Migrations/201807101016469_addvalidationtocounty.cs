namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvalidationtocounty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Counties", "CountyName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Counties", "CountyName", c => c.String());
        }
    }
}
