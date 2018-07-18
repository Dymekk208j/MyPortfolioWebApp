namespace MyPortfolioWebApp.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "IsIcon", c => c.Boolean(nullable: false));
            AddColumn("dbo.TempProjects", "IsIcon", c => c.Boolean(nullable: false));
            DropColumn("dbo.Projects", "ImageLink");
            DropColumn("dbo.TempProjects", "ImageLink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TempProjects", "ImageLink", c => c.String());
            AddColumn("dbo.Projects", "ImageLink", c => c.String());
            DropColumn("dbo.TempProjects", "IsIcon");
            DropColumn("dbo.Projects", "IsIcon");
        }
    }
}
