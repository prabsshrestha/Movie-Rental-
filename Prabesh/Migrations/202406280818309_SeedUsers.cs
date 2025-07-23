namespace Prabesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'81d6d8e6-549a-4057-8824-21e600fe91ee', N'guest@prabesh.com', 0, N'AHGwyJgETpykgi6vQWeJgfuJ25inKi+R45khAxJd0VQXGqpm1GlFhxKqPzVUqpZxvA==', N'5311150e-6926-4b72-add4-8cef26aa9f5f', NULL, 0, 0, NULL, 1, 0, N'guest@prabesh.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bf34bb01-bd25-4989-8353-ef891c9a8a98', N'admin@prabesh.com', 0, N'AGHY1FyhqN2xxnt9ldFcL/WYsMHF58o7L2WyvnGT3ultxxWxR3BgZH87npRIXNNoSg==', N'f269ad9d-25cf-4135-9852-8f520063dd5d', NULL, 0, 0, NULL, 1, 0, N'admin@prabesh.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'bf59c360-e931-4bda-86d9-2faf0b0e871d', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bf34bb01-bd25-4989-8353-ef891c9a8a98', N'bf59c360-e931-4bda-86d9-2faf0b0e871d')

                ");
        }
        
        public override void Down()
        {
        }
    }
}
