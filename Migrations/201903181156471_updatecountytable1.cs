namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecountytable1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Constituencies", "CountyId");
            AddForeignKey("dbo.Constituencies", "CountyId",
                "dbo.Counties", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Constituencies", "CountyId", "dbo.Counties");
            DropIndex("dbo.Constituencies", new[] { "CountyId" });
        }
    }
}
