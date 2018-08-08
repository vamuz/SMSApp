namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecountytable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Constituencies", "ConstituencyName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Constituencies", "ConstituencyName", c => c.String());
        }
    }
}
