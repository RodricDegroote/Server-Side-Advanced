namespace nmct.ba.webshop.context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Aantal = c.Int(nullable: false),
                        UserID = c.String(),
                        ProductID = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Deleted = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Aankoopprijs = c.Double(nullable: false),
                        Huurprijs = c.Double(nullable: false),
                        Aantal = c.Int(nullable: false),
                        Image = c.String(),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Frameworks",
                c => new
                    {
                        FrameworkId = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.FrameworkId);
            
            CreateTable(
                "dbo.OS",
                c => new
                    {
                        OSId = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.OSId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TotaalPrijsOrder = c.Double(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Deleted = c.Int(nullable: false),
                        Gebruiker_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.Gebruiker_ID)
                .Index(t => t.Gebruiker_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Naam = c.String(),
                        Voornaam = c.String(),
                        Adres = c.String(),
                        Postcode = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BasketID = c.Int(nullable: false),
                        TotaalPrijs = c.Double(nullable: false),
                        UserID = c.String(),
                        Order_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.BasketID)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FirstName = c.String(),
                        Address = c.String(),
                        Zipcode = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FrameworkProducts",
                c => new
                    {
                        Framework_FrameworkId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Framework_FrameworkId, t.Product_ProductId })
                .ForeignKey("dbo.Frameworks", t => t.Framework_FrameworkId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.Framework_FrameworkId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.OSProducts",
                c => new
                    {
                        OS_OSId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OS_OSId, t.Product_ProductId })
                .ForeignKey("dbo.OS", t => t.OS_OSId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.OS_OSId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderLines", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.OrderLines", "BasketID", "dbo.Baskets");
            DropForeignKey("dbo.Orders", "Gebruiker_ID", "dbo.Users");
            DropForeignKey("dbo.Baskets", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OSProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.OSProducts", "OS_OSId", "dbo.OS");
            DropForeignKey("dbo.FrameworkProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.FrameworkProducts", "Framework_FrameworkId", "dbo.Frameworks");
            DropIndex("dbo.OSProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.OSProducts", new[] { "OS_OSId" });
            DropIndex("dbo.FrameworkProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.FrameworkProducts", new[] { "Framework_FrameworkId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OrderLines", new[] { "Order_ID" });
            DropIndex("dbo.OrderLines", new[] { "BasketID" });
            DropIndex("dbo.Orders", new[] { "Gebruiker_ID" });
            DropIndex("dbo.Baskets", new[] { "ProductID" });
            DropTable("dbo.OSProducts");
            DropTable("dbo.FrameworkProducts");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.OS");
            DropTable("dbo.Frameworks");
            DropTable("dbo.Products");
            DropTable("dbo.Baskets");
        }
    }
}
