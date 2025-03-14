namespace DTO
{
    public class User : BaseDTO
    {
        public new int Id { get; set; }
        public string UserCode { get; set; } = string.Empty;   
        public string Name { get; set; } = string.Empty;       
        public string LastName { get; set; } = string.Empty;    
        public string Email { get; set; } = string.Empty;       
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Password { get; set; } = string.Empty;    
    }

}
