namespace SBC.TimeCards.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixcasecadingtempaltes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeviceTemplates", "Id", "dbo.TicketTemplates");
            DropForeignKey("dbo.NetworkTemplates", "Id", "dbo.TicketTemplates");
            DropForeignKey("dbo.ServerTemplates", "Id", "dbo.TicketTemplates");
            DropForeignKey("dbo.UserTemplates", "Id", "dbo.TicketTemplates");
            AddForeignKey("dbo.DeviceTemplates", "Id", "dbo.TicketTemplates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.NetworkTemplates", "Id", "dbo.TicketTemplates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ServerTemplates", "Id", "dbo.TicketTemplates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserTemplates", "Id", "dbo.TicketTemplates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTemplates", "Id", "dbo.TicketTemplates");
            DropForeignKey("dbo.ServerTemplates", "Id", "dbo.TicketTemplates");
            DropForeignKey("dbo.NetworkTemplates", "Id", "dbo.TicketTemplates");
            DropForeignKey("dbo.DeviceTemplates", "Id", "dbo.TicketTemplates");
            AddForeignKey("dbo.UserTemplates", "Id", "dbo.TicketTemplates", "Id");
            AddForeignKey("dbo.ServerTemplates", "Id", "dbo.TicketTemplates", "Id");
            AddForeignKey("dbo.NetworkTemplates", "Id", "dbo.TicketTemplates", "Id");
            AddForeignKey("dbo.DeviceTemplates", "Id", "dbo.TicketTemplates", "Id");
        }
    }
}
