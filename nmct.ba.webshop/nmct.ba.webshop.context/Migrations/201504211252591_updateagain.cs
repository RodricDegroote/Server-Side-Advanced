namespace nmct.ba.webshop.context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateagain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormPosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        OrderNr = c.Int(nullable: false),
                        FormType = c.String(),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FormPosts");
        }
    }
}
