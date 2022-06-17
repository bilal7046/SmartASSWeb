using System.ComponentModel;

namespace SmartASSWeb.Bll
{
    public enum EnumPackage
    {
        Individual,
        SmallBusiness,
        Business,
        Enterprise,
        AllAccess
    }
    public enum EnumNotificationType
    {
        [Description("REQUEST")]
        REQUEST = 0,
        [Description("ALERT")]
        ALERT = 1,
        [Description("MESSAGE")]
        MESSAGE = 2
    }
    public enum EnumLeadStatus
    {
        Connected = 1,
        Qualified = 2,
        Quoted = 3,
        //OvercameObjections = 4,
        Closed = 5,
        Lost = 6,
    }

    public enum EnumActivity
    {
        EmailClick,
        WebsiteClick,
        ShareClick,
        ProfileClick,
        FacebookClick,
        TwitterClick,
        InstagramClick,
        LinkedInClick,
        YouTubeClick,
        //
        //Card is shared with external people via email
        CardSharedExternal,
        //Card is shared with other members of the application
        CardSharedInternal,
        //User shares card that belongs to someone else with a member of the application
        CardSharedOnBehalf,
        //Request accepted by user
        RequestAccepted,
        //Request to connect with user
        RequestToConnect,
        //Number of times card is viewed
        CardViewed
    }

    public enum EnumGender
    {
        Male,
        Female,
        Other
    }

    public enum EnumJobLevel
    {
        EntryLevel,
        Intermediate,
        MidLevel,
        Senior,
        Executive
    }

    public enum EnumBusinessCardVersion
    {
        _BusinessCard1,
        _BusinessCard2,
        _BusinessCard3,
        _MobileCard1,
        _MobileCard2,
        _MobileCard3,
        _MobileCard4,
        _MobileCard5,
        _MobileCard6,
        _MobileCard7,
        _MobileCard8
    }
    public enum EnumBusinessCardBackgroundImage
    {
        _Concrete,
        _Stones,
        _Wall,
        _Waves,
        _Wood
    }
}
