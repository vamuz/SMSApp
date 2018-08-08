namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addextravalidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SMSAppRegistrations", "EmailAddress", c => c.String(maxLength: 150));
            AlterColumn("dbo.SMSAppRegistrations", "Occupation", c => c.String(maxLength: 150));
            AlterColumn("dbo.SMSAppRegistrations", "Location", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SMSAppRegistrations", "Location", c => c.String());
            AlterColumn("dbo.SMSAppRegistrations", "Occupation", c => c.String());
            AlterColumn("dbo.SMSAppRegistrations", "EmailAddress", c => c.String());
        }
    }
}
