namespace SmartASSWeb.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        ActivityType = c.String(maxLength: 200, storeType: "nvarchar"),
                        ActivityMessage = c.String(maxLength: 2000, storeType: "nvarchar"),
                        ActivityCreator = c.String(maxLength: 200, storeType: "nvarchar"),
                        ActivityCreatorImage = c.String(maxLength: 200, storeType: "nvarchar"),
                        ActivityTime = c.DateTime(nullable: false, precision: 0),
                        ActivityOwner = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        ActivityBusiness = c.String(maxLength: 200, storeType: "nvarchar"),
                        ActivityCreatorName = c.String(maxLength: 200, storeType: "nvarchar"),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityId);
            
            CreateTable(
                "dbo.AffiliateProfiles",
                c => new
                    {
                        AffiliateProfileId = c.Int(nullable: false, identity: true),
                        AffiliateCode = c.String(maxLength: 200, storeType: "nvarchar"),
                        Username = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Password = c.String(maxLength: 200, storeType: "nvarchar"),
                        VanityUrl = c.String(nullable: false, maxLength: 800, storeType: "nvarchar"),
                        FirstName = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        LastName = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Phone = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        EmailAddress = c.String(nullable: false, maxLength: 500, storeType: "nvarchar"),
                        Website = c.String(maxLength: 200, storeType: "nvarchar"),
                        AffiliateAccountStatus = c.String(maxLength: 200, storeType: "nvarchar"),
                        ConfirmPassword = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AffiliateProfileId);
            
            CreateTable(
                "dbo.AppSettings",
                c => new
                    {
                        AppSettingsId = c.Int(nullable: false, identity: true),
                        SettingName = c.String(maxLength: 200, storeType: "nvarchar"),
                        SettingValue = c.String(maxLength: 200, storeType: "nvarchar"),
                        UserId = c.String(maxLength: 200, storeType: "nvarchar"),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AppSettingsId);
            
            CreateTable(
                "dbo.BusinessCards",
                c => new
                    {
                        BusinessCardId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500, storeType: "nvarchar"),
                        Slogan = c.String(maxLength: 800, storeType: "nvarchar"),
                        Overview = c.String(nullable: false, maxLength: 500, storeType: "nvarchar"),
                        Address1 = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Address2 = c.String(maxLength: 200, storeType: "nvarchar"),
                        City = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        CompanyType = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Country = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        PostalCode = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Province = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Phone1 = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Phone2 = c.String(maxLength: 200, storeType: "nvarchar"),
                        Industry = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Title = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        JobLevel = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        UserId = c.String(maxLength: 200, storeType: "nvarchar"),
                        Website = c.String(maxLength: 200, storeType: "nvarchar"),
                        TagList = c.String(unicode: false),
                        ProductServiceList = c.String(unicode: false),
                        ConnectedUserList = c.String(unicode: false),
                        MissionStatement = c.String(unicode: false),
                        LogoUrl = c.String(unicode: false),
                        Theme = c.String(maxLength: 200, storeType: "nvarchar"),
                        ThemeColor = c.String(maxLength: 200, storeType: "nvarchar"),
                        CompanyFontColor = c.String(maxLength: 200, storeType: "nvarchar"),
                        PersonFontColor = c.String(maxLength: 200, storeType: "nvarchar"),
                        PersonFontSize = c.String(maxLength: 200, storeType: "nvarchar"),
                        TitleFontSize = c.String(maxLength: 200, storeType: "nvarchar"),
                        CompanyFontSize = c.String(maxLength: 200, storeType: "nvarchar"),
                        SloganFontSize = c.String(maxLength: 200, storeType: "nvarchar"),
                        ThemeBackgroundImage = c.String(maxLength: 200, storeType: "nvarchar"),
                        SocialLinkedUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        SocialFacebookUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        SocialTwitterUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        SocialInstagramUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        SocialYoutubeUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        BusinessCardVersion = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        ProfileImage = c.String(maxLength: 200, storeType: "nvarchar"),
                        ProfileFullName = c.String(maxLength: 200, storeType: "nvarchar"),
                        ProfileEmail = c.String(maxLength: 200, storeType: "nvarchar"),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.BusinessCardId);
            
            CreateTable(
                "dbo.CalendarEvents",
                c => new
                    {
                        CalendarEventId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Title = c.String(unicode: false),
                        Start = c.DateTime(nullable: false, precision: 0),
                        End = c.DateTime(nullable: false, precision: 0),
                        ClassName = c.String(unicode: false),
                        AllDay = c.Boolean(nullable: false),
                        UserId = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CalendarEventId);
            
            CreateTable(
                "dbo.CampaignAffiliates",
                c => new
                    {
                        CampaignAffiliateId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CampaignAffiliateId);
            
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        CampaignId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(unicode: false),
                        CampaignDate = c.DateTime(nullable: false, precision: 0),
                        CommissionAmount = c.String(unicode: false),
                        SalesAmount = c.String(unicode: false),
                        CampaignStatus = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CampaignId);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ChatId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        UserId = c.String(maxLength: 100, storeType: "nvarchar"),
                        ParticipantList = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ChatId);
            
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        ChatMessageId = c.Int(nullable: false, identity: true),
                        Message = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UserId = c.String(unicode: false),
                        FullName = c.String(unicode: false),
                        PhotoUrl = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                        Chat_ChatId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ChatMessageId)
                .ForeignKey("dbo.Chats", t => t.Chat_ChatId)
                .Index(t => t.Chat_ChatId);
            
            CreateTable(
                "dbo.CompanyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Sequence = c.Int(nullable: false),
                        UserId = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        Phone = c.String(nullable: false, unicode: false),
                        Mobile = c.String(nullable: false, unicode: false),
                        EmailAddress = c.String(nullable: false, unicode: false),
                        Website = c.String(unicode: false),
                        AddressLine1 = c.String(unicode: false),
                        AddressLine2 = c.String(unicode: false),
                        City = c.String(unicode: false),
                        Province = c.String(unicode: false),
                        Country = c.String(unicode: false),
                        PostalCode = c.String(unicode: false),
                        PhotoUrl = c.String(unicode: false),
                        UserId = c.String(nullable: false, unicode: false),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.ConversionGoals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(unicode: false),
                        Start = c.DateTime(nullable: false, precision: 0),
                        End = c.DateTime(nullable: false, precision: 0),
                        Name = c.String(unicode: false),
                        Period = c.String(unicode: false),
                        ConversionValue = c.String(unicode: false),
                        StartDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LeadLogActions",
                c => new
                    {
                        LeadLogActionId = c.Int(nullable: false, identity: true),
                        LogAction = c.String(nullable: false, unicode: false),
                        Notes = c.String(nullable: false, unicode: false),
                        Subject = c.String(unicode: false),
                        DueDate = c.DateTime(precision: 0),
                        LeadId = c.String(unicode: false),
                        DueDateString = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.LeadLogActionId);
            
            CreateTable(
                "dbo.LeadNotes",
                c => new
                    {
                        LeadNoteId = c.Int(nullable: false, identity: true),
                        Subject = c.String(unicode: false),
                        Text = c.String(unicode: false),
                        LeadId = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.LeadNoteId);
            
            CreateTable(
                "dbo.Leads",
                c => new
                    {
                        LeadId = c.Int(nullable: false, identity: true),
                        LeadOwner = c.String(nullable: false, unicode: false),
                        LeadStatus = c.String(nullable: false, unicode: false),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        Website = c.String(unicode: false),
                        Title = c.String(nullable: false, unicode: false),
                        Company = c.String(unicode: false),
                        Email = c.String(nullable: false, unicode: false),
                        NoOfEmployees = c.Int(nullable: false),
                        Industry = c.String(unicode: false),
                        LeadSource = c.String(unicode: false),
                        Phone = c.String(nullable: false, unicode: false),
                        Mobile = c.String(unicode: false),
                        Address1 = c.String(nullable: false, unicode: false),
                        Address2 = c.String(unicode: false),
                        City = c.String(unicode: false),
                        Province = c.String(unicode: false),
                        Country = c.String(nullable: false, unicode: false),
                        PostalCode = c.String(nullable: false, unicode: false),
                        Notes = c.String(unicode: false),
                        UserId = c.String(nullable: false, unicode: false),
                        Revenue = c.Double(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.LeadId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.String(unicode: false),
                        IsRead = c.Boolean(nullable: false),
                        Message = c.String(unicode: false),
                        NotificationType = c.String(unicode: false),
                        UserId = c.String(unicode: false),
                        RequestorUserId = c.String(unicode: false),
                        RequestorName = c.String(unicode: false),
                        RequestorPhotoUrl = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        ColorCode = c.String(unicode: false),
                        UserId = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TeamMemberGoals",
                c => new
                    {
                        TeamMemberGoalId = c.Int(nullable: false, identity: true),
                        UserId = c.String(unicode: false),
                        ConnectedLeads = c.Int(nullable: false),
                        QualifiedLeads = c.Int(nullable: false),
                        QuotedLeads = c.Int(nullable: false),
                        ClosedLeads = c.Int(nullable: false),
                        LostLeads = c.Int(nullable: false),
                        OvercameObjectionLeads = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TeamMemberGoalId);
            
            CreateTable(
                "dbo.UserActions",
                c => new
                    {
                        UserActionId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RequestCount = c.Int(nullable: false),
                        UserId = c.String(unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                        UpdatedBy = c.String(maxLength: 200, storeType: "nvarchar"),
                        DateUpdated = c.DateTime(precision: 0),
                        rowversion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.UserActionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatMessages", "Chat_ChatId", "dbo.Chats");
            DropIndex("dbo.ChatMessages", new[] { "Chat_ChatId" });
            DropTable("dbo.UserActions");
            DropTable("dbo.TeamMemberGoals");
            DropTable("dbo.Tags");
            DropTable("dbo.Notifications");
            DropTable("dbo.Leads");
            DropTable("dbo.LeadNotes");
            DropTable("dbo.LeadLogActions");
            DropTable("dbo.ConversionGoals");
            DropTable("dbo.Contacts");
            DropTable("dbo.ContactGroups");
            DropTable("dbo.CompanyTypes");
            DropTable("dbo.ChatMessages");
            DropTable("dbo.Chats");
            DropTable("dbo.Campaigns");
            DropTable("dbo.CampaignAffiliates");
            DropTable("dbo.CalendarEvents");
            DropTable("dbo.BusinessCards");
            DropTable("dbo.AppSettings");
            DropTable("dbo.AffiliateProfiles");
            DropTable("dbo.Activities");
        }
    }
}
