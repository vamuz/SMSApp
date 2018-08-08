namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesmsappregistrationtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SMSAppRegistrations", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SMSAppRegistrations", "EmailAddress");
        }
    }
}
