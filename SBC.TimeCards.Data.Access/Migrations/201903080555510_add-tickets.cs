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
                        Title = c.String(),
                        Description = c.String(),
                        CreatoionDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(),
                        OwnerId = c.Int(nullable: false),
                        AssigneeId = c.Int(),
                        StateId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        ParentTicketId = c.Int(nullable: false),
                        Parent_Id = c.Int(),
                        TicketState_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AssigneeId)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .ForeignKey("dbo.Tickets", t => t.Parent_Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.TicketStates", t => t.TicketState_Id)
                .Index(t => t.OwnerId)
                .Index(t => t.AssigneeId)
                .Index(t => t.ProjectId)
                .Index(t => t.Parent_Id)
                .Index(t => t.TicketState_Id);
            
            CreateTable(
                "dbo.TicketStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketState_Id", "dbo.TicketStates");
            DropForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Tickets", "Parent_Id", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "AssigneeId", "dbo.Users");
            DropIndex("dbo.Tickets", new[] { "TicketState_Id" });
            DropIndex("dbo.Tickets", new[] { "Parent_Id" });
            DropIndex("dbo.Tickets", new[] { "ProjectId" });
            DropIndex("dbo.Tickets", new[] { "AssigneeId" });
            DropIndex("dbo.Tickets", new[] { "OwnerId" });
            DropTable("dbo.TicketStates");
            DropTable("dbo.Tickets");
        }
    }
}
