namespace SBC.TimeCards.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfavoriteremovecasecadeondeleteuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "UserId", "dbo.Users");
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.UserId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            AddForeignKey("dbo.Projects", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.Favorites", new[] { "ProjectId" });
            DropTable("dbo.Favorites");
            AddForeignKey("dbo.Projects", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
