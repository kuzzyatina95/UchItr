namespace UchItr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPostViewModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditPostViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EditPostViewModels", "CategoryID", "dbo.Categories");
            DropIndex("dbo.EditPostViewModels", new[] { "CategoryID" });
            DropTable("dbo.EditPostViewModels");
        }
    }
}
