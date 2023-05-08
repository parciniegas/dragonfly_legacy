namespace Dragonfly.Core.Security.SecurityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ActionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActionId)
                .ForeignKey("dbo.Options", t => t.OptionId, cascadeDelete: true)
                .Index(t => t.OptionId);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        OptionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ModuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OptionId)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ModuleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ModuleId);
            
            CreateTable(
                "dbo.AuthenticationMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Method = c.String(),
                        Assembly = c.String(),
                        Class = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NetworkSegments",
                c => new
                    {
                        NetworkSegmentId = c.Int(nullable: false, identity: true),
                        StartAddress = c.String(),
                        StopAddress = c.String(),
                        Restrictions_RestrictionsId = c.Int(),
                    })
                .PrimaryKey(t => t.NetworkSegmentId)
                .ForeignKey("dbo.Restrictions", t => t.Restrictions_RestrictionsId)
                .Index(t => t.Restrictions_RestrictionsId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        ActionId = c.Int(nullable: false),
                        PermissionType = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.PermissionId)
                .ForeignKey("dbo.Actions", t => t.ActionId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.ActionId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        LoginName = c.String(),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IsOnline = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        LockReason = c.String(),
                        Enabled = c.Int(nullable: false),
                        EnabledStateDate = c.DateTime(nullable: false),
                        MustChangePassword = c.Boolean(nullable: false),
                        LastLoginDate = c.DateTime(),
                        LastActivityDate = c.DateTime(),
                        LastPasswordChangeDate = c.DateTime(),
                        LastLockoutDate = c.DateTime(),
                        ConnectionTries = c.Int(nullable: false),
                        LastConnectionTryDate = c.DateTime(),
                        AuthenticationMethodId = c.Int(nullable: false),
                        Photo = c.Binary(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AuthenticationMethods", t => t.AuthenticationMethodId, cascadeDelete: true)
                .Index(t => t.AuthenticationMethodId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionId = c.String(nullable: false, maxLength: 128),
                        HostAddress = c.String(),
                        HostName = c.String(),
                        Browser = c.String(),
                        Platform = c.String(),
                        UserAgent = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Restrictions",
                c => new
                    {
                        RestrictionsId = c.Int(nullable: false, identity: true),
                        MaxConnectedUsers = c.Int(nullable: false),
                        MaxSessionsPerUser = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.RestrictionsId);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityName = c.String(),
                        EntityKey = c.String(),
                        Action = c.String(),
                        DataLog = c.String(),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Role_RoleId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NetworkSegments", "Restrictions_RestrictionsId", "dbo.Restrictions");
            DropForeignKey("dbo.Sessions", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AuthenticationMethodId", "dbo.AuthenticationMethods");
            DropForeignKey("dbo.Permissions", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Permissions", "ActionId", "dbo.Actions");
            DropForeignKey("dbo.Actions", "OptionId", "dbo.Options");
            DropForeignKey("dbo.Options", "ModuleId", "dbo.Modules");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropIndex("dbo.Sessions", new[] { "User_UserId" });
            DropIndex("dbo.Users", new[] { "AuthenticationMethodId" });
            DropIndex("dbo.Permissions", new[] { "RoleId" });
            DropIndex("dbo.Permissions", new[] { "ActionId" });
            DropIndex("dbo.NetworkSegments", new[] { "Restrictions_RestrictionsId" });
            DropIndex("dbo.Options", new[] { "ModuleId" });
            DropIndex("dbo.Actions", new[] { "OptionId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Tracks");
            DropTable("dbo.Restrictions");
            DropTable("dbo.Sessions");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            DropTable("dbo.NetworkSegments");
            DropTable("dbo.AuthenticationMethods");
            DropTable("dbo.Modules");
            DropTable("dbo.Options");
            DropTable("dbo.Actions");
        }
    }
}
