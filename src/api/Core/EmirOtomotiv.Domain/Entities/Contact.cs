namespace EmirOtomotiv.Core.Domain.Entities;

public class Contact : BaseEntity
{
    public string Description { get; set; } = "EMİR OTOMOTİV LTD. ŞTİ. AŞ.";

    public string Adress { get; set; } = "Veysel karani mh. 1.Erdal sok: No : 1 Kat : 1";

    public required string PhoneNumber {get; set; } = "+90 538 028 60 19";

    public required string WorkingHours { get; set; } = "Pazartesi - Cumartesi 8:30 to 18:30";

    public required string Mail { get; set; } = "contact@emirotomotiv.com";
}