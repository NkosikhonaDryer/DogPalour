namespace DogPalour.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToNewBd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        IdNumber = c.String(nullable: false, maxLength: 13),
                        FirstName = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        EmailAddres = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(nullable: false),
                       
                        EmpStatus = c.String(),
                        ProfileImage = c.String(nullable: true),
                        Password = c.String(nullable: true),
                        DateCreated = c.DateTime(nullable: true),
                       

                    })
                .PrimaryKey(t => t.IdNumber);
            CreateTable(
               "dbo.EmployeeSkills",
               c => new
               {
                   
                   id = c.Int(nullable: false, identity: true),
                   JobOffer = c.String(nullable: false),
                   NumYrsExperience = c.Int(nullable: true, identity: false),
                   MarketImage = c.String(nullable: true),
                   EmailAddres =c.String(nullable:true),
                   Timing = c.Int(nullable:true),
                   

               }) 
               .PrimaryKey(t => t.id)
               .ForeignKey("dbo.Employees", t => t.EmailAddres)
               .Index(t => t.EmailAddres);
            CreateTable(
               "dbo.DogServices",
               c => new
               {
                   id = c.Int(nullable: false, identity: true),

                   EmailAddres =c.String(nullable:true),
                  
                   

               }) 
               
               .ForeignKey("dbo.Employees", t => t.EmailAddres)
               .Index(t => t.EmailAddres);
            CreateTable(
               "dbo.Services",
               c => new
               {

                   id = c.Int(nullable: false, identity: true),
                   AddState = c.String(nullable: true),



               })
               .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DogServices", "id", "dbo.Employees");
            DropForeignKey("dbo.EmployeeSkills", "id", "dbo.Employees");
            DropIndex("dbo.DogServices", new[] { "EmailAddres" });
            DropIndex("dbo.EmployeeSkills", new[] { "EmailAddres" });
            DropTable("dbo.Employees");
            DropTable("dbo.DogServices");
            DropTable("dbo.Services");

        }
    }
}
