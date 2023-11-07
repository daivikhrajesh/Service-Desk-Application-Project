namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assign_Ticket",
                c => new
                    {
                        Assign_Id = c.Int(nullable: false, identity: true),
                        Ticket_Id = c.Int(nullable: false),
                        Dept_Id = c.Int(nullable: false),
                        Assigned_By = c.String(),
                        Group_Id = c.Int(nullable: false),
                        Assigned_To = c.String(),
                        Assign_Status = c.String(),
                    })
                .PrimaryKey(t => t.Assign_Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Dept_Id = c.Int(nullable: false, identity: true),
                        Dept_Name = c.String(),
                    })
                .PrimaryKey(t => t.Dept_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Emp_Id = c.Int(nullable: false, identity: true),
                        Emp_Name = c.String(),
                        Email_Id = c.String(),
                        Emp_Phone = c.Int(nullable: false),
                        Emp_Password = c.String(),
                        Department_Dept_Id = c.Int(),
                        Group_Group_Id = c.Int(),
                        Role_Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Emp_Id)
                .ForeignKey("dbo.Departments", t => t.Department_Dept_Id)
                .ForeignKey("dbo.Groups", t => t.Group_Group_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Role_Id)
                .Index(t => t.Department_Dept_Id)
                .Index(t => t.Group_Group_Id)
                .Index(t => t.Role_Role_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Group_Id = c.Int(nullable: false, identity: true),
                        Group_Name = c.String(),
                    })
                .PrimaryKey(t => t.Group_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Role_Id = c.Int(nullable: false, identity: true),
                        Role_Name = c.String(),
                    })
                .PrimaryKey(t => t.Role_Id);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        Response_Id = c.Int(nullable: false, identity: true),
                        Response_Msg = c.String(),
                        Response_status = c.String(),
                        Response_Date = c.DateTime(nullable: false),
                        Ticket_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Response_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Ticket_Id = c.Int(nullable: false, identity: true),
                        Ticket_Desc = c.String(),
                        Ticket_Status = c.String(),
                        Ticket_Priority = c.String(),
                        Ticket_By_Emp_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Ticket_Id)
                .ForeignKey("dbo.Employees", t => t.Ticket_By_Emp_Id)
                .Index(t => t.Ticket_By_Emp_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "Ticket_By_Emp_Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Role_Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Employees", "Group_Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Employees", "Department_Dept_Id", "dbo.Departments");
            DropIndex("dbo.Tickets", new[] { "Ticket_By_Emp_Id" });
            DropIndex("dbo.Employees", new[] { "Role_Role_Id" });
            DropIndex("dbo.Employees", new[] { "Group_Group_Id" });
            DropIndex("dbo.Employees", new[] { "Department_Dept_Id" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Responses");
            DropTable("dbo.Roles");
            DropTable("dbo.Groups");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
            DropTable("dbo.Assign_Ticket");
        }
    }
}
