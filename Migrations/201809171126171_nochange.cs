namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nochange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SMSAppRegistrations", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SMSAppRegistrations", "ImagePath", c => c.String());
        }
    }
}
