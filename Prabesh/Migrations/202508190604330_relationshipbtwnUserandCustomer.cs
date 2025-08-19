namespace Prabesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationshipbtwnUserandCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Birthdate", c => c.DateTime());
            CreateIndex("dbo.Customers", "UserId");
            AddForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "Birthdate");
            DropColumn("dbo.Customers", "UserId");
        }
    }
}
