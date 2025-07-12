using CarRentwithDB.Models;

namespace CarRentwithDB.ViewModels
{
    public class CarIndexViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
        public string MakeModel { get; set; }
        public string SortOrder { get; set; }

        //Paginacja
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
