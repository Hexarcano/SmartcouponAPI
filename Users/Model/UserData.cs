namespace SmartcouponAPI.Users.Model
{
    public class UserData
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string FatherLastName { get; set; }
        public string MotherLastName { get; set; }
        public string CURP { get; set; }
        //public int RoleId { get; set; }
        //public int CompanyId { get; set; }

        public User UserIdentity { get; set; }
    }
}
