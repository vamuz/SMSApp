namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsmsapptables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Constituencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConstituencyName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Counties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountyName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenderType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaritalStatusType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PWDCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PWDCategoryType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SMSAppRegistrations",
                c => new
                    {
                        SMSAppRegistrationId = c.Int(nullable: false, identity: true),
                        FullNames = c.String(),
                        NationalIDNo = c.Int(nullable: false),
                        YearofBirth = c.Int(nullable: false),
                        PhoneNo = c.Int(nullable: false),
                        MaritalStatusId = c.Int(nullable: false),
                        GenderId = c.Int(nullable: false),
                        Occupation = c.String(),
                        CountyId = c.Int(nullable: false),
                        ConstituencyId = c.Int(nullable: false),
                        Location = c.String(),
                        PWDCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SMSAppRegistrationId)
                .ForeignKey("dbo.Constituencies", t => t.ConstituencyId, cascadeDelete: true)
                .ForeignKey("dbo.Counties", t => t.CountyId, cascadeDelete: true)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .ForeignKey("dbo.MaritalStatus", t => t.MaritalStatusId, cascadeDelete: true)
                .ForeignKey("dbo.PWDCategories", t => t.PWDCategoryId, cascadeDelete: true)
                .Index(t => t.MaritalStatusId)
                .Index(t => t.GenderId)
                .Index(t => t.CountyId)
                .Index(t => t.ConstituencyId)
                .Index(t => t.PWDCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SMSAppRegistrations", "PWDCategoryId", "dbo.PWDCategories");
            DropForeignKey("dbo.SMSAppRegistrations", "MaritalStatusId", "dbo.MaritalStatus");
            DropForeignKey("dbo.SMSAppRegistrations", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.SMSAppRegistrations", "CountyId", "dbo.Counties");
            DropForeignKey("dbo.SMSAppRegistrations", "ConstituencyId", "dbo.Constituencies");
            DropIndex("dbo.SMSAppRegistrations", new[] { "PWDCategoryId" });
            DropIndex("dbo.SMSAppRegistrations", new[] { "ConstituencyId" });
            DropIndex("dbo.SMSAppRegistrations", new[] { "CountyId" });
            DropIndex("dbo.SMSAppRegistrations", new[] { "GenderId" });
            DropIndex("dbo.SMSAppRegistrations", new[] { "MaritalStatusId" });
            DropTable("dbo.SMSAppRegistrations");
            DropTable("dbo.PWDCategories");
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.Genders");
            DropTable("dbo.Counties");
            DropTable("dbo.Constituencies");
        }
    }
}
