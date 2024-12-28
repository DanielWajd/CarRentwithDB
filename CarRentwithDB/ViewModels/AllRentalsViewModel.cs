namespace CarRentwithDB.ViewModels
{
    public class AllRentalsViewModel
    {
        public int RentalId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
    }
}
