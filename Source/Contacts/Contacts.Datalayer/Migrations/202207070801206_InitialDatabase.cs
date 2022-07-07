namespace Contacts.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Family = c.String(nullable: false, maxLength: 30),
                        Mobile = c.String(maxLength: 11),
                        Phone = c.String(maxLength: 11),
                        Email = c.String(maxLength: 255),
                        Age = c.Int(nullable: false),
                        Description = c.String(maxLength: 400),
                        Type = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
