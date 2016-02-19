namespace UchItr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "IdImg");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "IdImg", c => c.Int(nullable: false));
        }
    }
}
