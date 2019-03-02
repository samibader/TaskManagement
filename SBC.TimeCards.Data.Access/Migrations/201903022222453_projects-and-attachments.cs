namespace SBC.TimeCards.Data.Access.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectsandattachments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProjects", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserProjects", "Project_Id", "dbo.Projects");
            DropIndex("dbo.UserProjects", new[] { "User_Id" });
            DropIndex("dbo.UserProjects", new[] { "Project_Id" });
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 512),
                        Description = c.String(),
                        FileName = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.Title, name: "AttachmentTitleIndex")
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Projects", "Description", c => c.String());
            AddColumn("dbo.Projects", "IsArchived", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "Color", c => c.String());
            AddColumn("dbo.Projects", "ArchiveDate", c => c.DateTime());
            AddColumn("dbo.Projects", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "UserId");
            AddForeignKey("dbo.Projects", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropTable("dbo.UserProjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProjects",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Project_Id });
            
            DropForeignKey("dbo.Attachments", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "UserId", "dbo.Users");
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropIndex("dbo.Attachments", new[] { "ProjectId" });
            DropIndex("dbo.Attachments", "AttachmentTitleIndex");
            DropColumn("dbo.Projects", "UserId");
            DropColumn("dbo.Projects", "ArchiveDate");
            DropColumn("dbo.Projects", "Color");
            DropColumn("dbo.Projects", "IsArchived");
            DropColumn("dbo.Projects", "Description");
            DropTable("dbo.Attachments");
            CreateIndex("dbo.UserProjects", "Project_Id");
            CreateIndex("dbo.UserProjects", "User_Id");
            AddForeignKey("dbo.UserProjects", "Project_Id", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserProjects", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
