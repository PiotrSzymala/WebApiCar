namespace WebApiCar.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; } = default!;
        public string HashedPassword { get; set; } = default!;
    }
}
