namespace UniSystem.Core.DTOs
{
    public class UserAllInfoDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string No { get; set; }
        public string Email { get; set; }
        public string TC { get; set; } // kontrol ettirt
        public string PhotoBase64Text { get; set; }
        public string PhoneNumber { get; set; }

        public int? AddressId { get; set; }
        public string? AddressDec { get; set; }



    }
}
