namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesmsapptable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SMSAppRegistrations", "PhoneNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SMSAppRegistrations", "PhoneNo", c => c.Long(nullable: false));
        }
    }
}
