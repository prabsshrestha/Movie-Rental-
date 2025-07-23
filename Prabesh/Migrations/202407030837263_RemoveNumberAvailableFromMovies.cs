namespace Prabesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNumberAvailableFromMovies : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "NumberAvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Byte(nullable: false));
        }
    }
}
