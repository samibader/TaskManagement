namespace SBC.TimeCards.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        CreatoionDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(),
                        OwnerId = c.Int(nullable: false),
                        AssigneeId = c.Int(),
                        StateId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        ParentTicketId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AssigneeId)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .ForeignKey("dbo.Tickets", t => t.ParentTicketId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.TicketStates", t => t.StateId)
                .Index(t => t.Title, name: "TicketTitleIndex")
                .Index(t => t.OwnerId)
                .Index(t => t.AssigneeId)
                .Index(t => t.StateId)
                .Index(t => t.ProjectId)
                .Index(t => t.ParentTicketId);
            
            CreateTable(
                "dbo.TicketStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "TicketStateTitleIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "StateId", "dbo.TicketStates");
            DropForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Tickets", "ParentTicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "AssigneeId", "dbo.Users");
            DropIndex("dbo.TicketStates", "TicketStateTitleIndex");
            DropIndex("dbo.Tickets", new[] { "ParentTicketId" });
            DropIndex("dbo.Tickets", new[] { "ProjectId" });
            DropIndex("dbo.Tickets", new[] { "StateId" });
            DropIndex("dbo.Tickets", new[] { "AssigneeId" });
            DropIndex("dbo.Tickets", new[] { "OwnerId" });
            DropIndex("dbo.Tickets", "TicketTitleIndex");
            DropTable("dbo.TicketStates");
            DropTable("dbo.Tickets");
        }
    }
}
