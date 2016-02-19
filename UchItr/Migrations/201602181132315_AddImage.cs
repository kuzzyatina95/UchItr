namespace UchItr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkImg = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "IdImg", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "Image_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Image_Id");
            AddForeignKey("dbo.Posts", "Image_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Image_Id", "dbo.Images");
            DropIndex("dbo.Posts", new[] { "Image_Id" });
            DropColumn("dbo.Posts", "Image_Id");
            DropColumn("dbo.Posts", "IdImg");
            DropTable("dbo.Images");
        }
    }
}
