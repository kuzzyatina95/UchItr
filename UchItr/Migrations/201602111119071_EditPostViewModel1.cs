namespace UchItr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditPostViewModel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EditPostViewModels", "CategoryID", "dbo.Categories");
            DropIndex("dbo.EditPostViewModels", new[] { "CategoryID" });
            DropTable("dbo.EditPostViewModels");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.EditPostViewModels", "CategoryID");
            AddForeignKey("dbo.EditPostViewModels", "CategoryID", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
