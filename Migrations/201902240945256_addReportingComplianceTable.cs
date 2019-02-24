namespace SMSApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReportingComplianceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.pArticle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleName = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.pReportingCompliance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TreatyId = c.Int(nullable: false),
                        SupervisoryBodyId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        InitialReportDueDate = c.String(nullable: false),
                        PeriodicReportDueDate = c.Int(nullable: false),
                        GeneralComments = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.pArticle", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.pSupervisoryBody", t => t.SupervisoryBodyId, cascadeDelete: true)
                .ForeignKey("dbo.pTreaty", t => t.TreatyId, cascadeDelete: true)
                .Index(t => t.TreatyId)
                .Index(t => t.SupervisoryBodyId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.pSupervisoryBody",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupervisoryBodyName = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.pTreaty",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TreatyName = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.pSubmissionDueDate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DueDate = c.DateTime(nullable: false),
                        ReportingComplianceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.pReportingCompliance", t => t.ReportingComplianceId, cascadeDelete: true)
                .Index(t => t.ReportingComplianceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.pSubmissionDueDate", "ReportingComplianceId", "dbo.pReportingCompliance");
            DropForeignKey("dbo.pReportingCompliance", "TreatyId", "dbo.pTreaty");
            DropForeignKey("dbo.pReportingCompliance", "SupervisoryBodyId", "dbo.pSupervisoryBody");
            DropForeignKey("dbo.pReportingCompliance", "ArticleId", "dbo.pArticle");
            DropIndex("dbo.pSubmissionDueDate", new[] { "ReportingComplianceId" });
            DropIndex("dbo.pReportingCompliance", new[] { "ArticleId" });
            DropIndex("dbo.pReportingCompliance", new[] { "SupervisoryBodyId" });
            DropIndex("dbo.pReportingCompliance", new[] { "TreatyId" });
            DropTable("dbo.pSubmissionDueDate");
            DropTable("dbo.pTreaty");
            DropTable("dbo.pSupervisoryBody");
            DropTable("dbo.pReportingCompliance");
            DropTable("dbo.pArticle");
        }
    }
}
