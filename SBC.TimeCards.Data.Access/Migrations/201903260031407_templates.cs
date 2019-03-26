namespace SBC.TimeCards.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class templates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        TemplateTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TemplateType", t => t.TemplateTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => t.TemplateTypeId);
            
            CreateTable(
                "dbo.DeviceTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Type = c.String(),
                        Location = c.String(),
                        Name = c.String(),
                        Ip = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplates", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.NetworkTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Ip = c.String(),
                        Subnet = c.String(),
                        DefaultGateway = c.String(),
                        Zone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplates", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ServerTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Ram = c.String(),
                        Cpu = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplates", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ServerDiskTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServerTemplateId = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServerTemplates", t => t.ServerTemplateId, cascadeDelete: true)
                .Index(t => t.ServerTemplateId);
            
            CreateTable(
                "dbo.ServerNetworkTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServerTemplateId = c.Int(nullable: false),
                        Ip = c.String(),
                        Subnet = c.String(),
                        DefaultGateway = c.String(),
                        Zone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServerTemplates", t => t.ServerTemplateId, cascadeDelete: true)
                .Index(t => t.ServerTemplateId);
            
            CreateTable(
                "dbo.TemplateType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "AttachmentTitleIndex");
            
            CreateTable(
                "dbo.UserTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        ExpirationDate = c.String(),
                        OU = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTemplates", t => t.Id)
                .Index(t => t.Id);
            
            AlterColumn("dbo.Comments", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTemplates", "Id", "dbo.TicketTemplates");
            DropForeignKey("dbo.TicketTemplates", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.TicketTemplates", "TemplateTypeId", "dbo.TemplateType");
            DropForeignKey("dbo.ServerTemplates", "Id", "dbo.TicketTemplates");
            DropForeignKey("dbo.ServerNetworkTemplates", "ServerTemplateId", "dbo.ServerTemplates");
            DropForeignKey("dbo.ServerDiskTemplates", "ServerTemplateId", "dbo.ServerTemplates");
            DropForeignKey("dbo.NetworkTemplates", "Id", "dbo.TicketTemplates");
            DropForeignKey("dbo.DeviceTemplates", "Id", "dbo.TicketTemplates");
            DropIndex("dbo.UserTemplates", new[] { "Id" });
            DropIndex("dbo.TemplateType", "AttachmentTitleIndex");
            DropIndex("dbo.ServerNetworkTemplates", new[] { "ServerTemplateId" });
            DropIndex("dbo.ServerDiskTemplates", new[] { "ServerTemplateId" });
            DropIndex("dbo.ServerTemplates", new[] { "Id" });
            DropIndex("dbo.NetworkTemplates", new[] { "Id" });
            DropIndex("dbo.DeviceTemplates", new[] { "Id" });
            DropIndex("dbo.TicketTemplates", new[] { "TemplateTypeId" });
            DropIndex("dbo.TicketTemplates", new[] { "TicketId" });
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false));
            DropTable("dbo.UserTemplates");
            DropTable("dbo.TemplateType");
            DropTable("dbo.ServerNetworkTemplates");
            DropTable("dbo.ServerDiskTemplates");
            DropTable("dbo.ServerTemplates");
            DropTable("dbo.NetworkTemplates");
            DropTable("dbo.DeviceTemplates");
            DropTable("dbo.TicketTemplates");
        }
    }
}
