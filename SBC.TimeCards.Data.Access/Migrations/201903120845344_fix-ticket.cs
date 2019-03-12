namespace SBC.TimeCards.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixticket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tickets", "TicketTitleIndex");
            DropIndex("dbo.Tickets", new[] { "ProjectId" });
            AlterColumn("dbo.Tickets", "Title", c => c.String(maxLength: 256));
            AlterColumn("dbo.Tickets", "ProjectId", c => c.Int());
            CreateIndex("dbo.Tickets", "ProjectId");
            AddForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Tickets", new[] { "ProjectId" });
            AlterColumn("dbo.Tickets", "ProjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "Title", c => c.String(nullable: false, maxLength: 256));
            CreateIndex("dbo.Tickets", "ProjectId");
            CreateIndex("dbo.Tickets", "Title", name: "TicketTitleIndex");
            AddForeignKey("dbo.Tickets", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
