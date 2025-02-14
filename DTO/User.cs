namespace DTO
{
    public class User : BaseDTO
    {
        public string UserCode { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
    }
}
