namespace IntroAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTables : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.News", "CatId");
            AddForeignKey("dbo.News", "CatId", "dbo.Catagories", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "CatId", "dbo.Catagories");
            DropIndex("dbo.News", new[] { "CatId" });
        }
    }
}
