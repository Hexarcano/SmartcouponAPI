namespace SmartcouponAPI.Tokens.Model
{
    public class ClaimsData
    {
        public Guid JWITID { get; set; }
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string FatherLastName { get; set; }
        public required string MotherLastName { get; set; }
        public required string Email { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime AccessExpirationTime { get; set; }
        public DateTime RefreshExpirationTime { get; set; }
    }
}
