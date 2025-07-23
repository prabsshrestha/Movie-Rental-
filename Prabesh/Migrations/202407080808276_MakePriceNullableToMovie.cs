namespace Prabesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePriceNullableToMovie : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Price", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Price", c => c.Int(nullable: false));
        }
    }
}
