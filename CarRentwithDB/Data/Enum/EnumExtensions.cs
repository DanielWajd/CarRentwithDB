namespace CarRentwithDB.Data.Enum
{
    public static class EnumExtensions
    {
        public static string ToFriendlyString(this CarType carType)
        {
            return carType switch
            {
                CarType.Sedan => "Sedan",
                CarType.SUV => "SUV",
                CarType.Coupe => "Coupe",
                CarType.Hatchback => "Hatchback",
                CarType.Convertible => "Kabriolet",
                CarType.Minivan => "Minivan",
                CarType.PickupTruck => "Pickup",
                CarType.StationWagon => "Kombi",
                CarType.SportsCar => "Samochód sportowy",
                _ => carType.ToString(),
            };
        }

        public static string ToFriendlyString(this FuelType fuelType)
        {
            return fuelType switch
            {
                FuelType.Petrol => "Benzyna",
                FuelType.Diesel => "Diesel",
                FuelType.Electric => "Elektryczny",
                FuelType.Hybrid => "Hybryda",
                FuelType.LPG => "LPG",
                _ => fuelType.ToString(),
            };
        }

        public static string ToFriendlyString(this CarColor carColor)
        {
            return carColor switch
            {
                CarColor.Black => "Czarny",
                CarColor.White => "Biały",
                CarColor.Silver => "Srebrny",
                CarColor.Gray => "Szary",
                CarColor.Red => "Czerwony",
                CarColor.Blue => "Niebieski",
                CarColor.Green => "Zielony",
                CarColor.Yellow => "Żółty",
                CarColor.Orange => "Pomarańczowy",
                CarColor.Brown => "Brązowy",
                CarColor.Purple => "Fioletowy",
                CarColor.Pink => "Różowy",
                CarColor.Gold => "Złoty",
                CarColor.Beige => "Beżowy",
                _ => carColor.ToString(),
            };
        }

        public static string ToFriendlyString(this UserType userType)
        {
            return userType switch
            {
                UserType.Customer => "Klient",
                UserType.Employee => "Pracownik",
                _ => userType.ToString(),
            };
        }
    }
}
