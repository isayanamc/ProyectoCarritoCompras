namespace DTO
{
    /*
     * Clase padre de todos los DTOs, todos los DTOs del sistema
     * deben de heredar de esta clase
     */
    public class BaseDTO
    {
        public int Id { get; set; }
        public DateTime? Created { get; set; }
    }
}
