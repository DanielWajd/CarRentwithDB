using CarRentwithDB.Data.Enum;

public static class EnumExtensions
{
    public static string TranslateCarType(CarType carType)
    {
        switch (carType)
        {
            case CarType.Sedan: return "Sedan";
            case CarType.SUV: return "SUV";
            case CarType.Coupe: return "Coupe";
            case CarType.Hatchback: return "Hatchback";
            case CarType.Convertible: return "Kabriolet";
            case CarType.Minivan: return "Minivan";
            case CarType.PickupTruck: return "Pickup";
            case CarType.StationWagon: return "Kombi";
            case CarType.SportsCar: return "Samochód sportowy";
            default: return carType.ToString();
        }
    }

    public static string TranslateFuelType(FuelType fuelType)
    {
        switch (fuelType)
        {
            case FuelType.Petrol: return "Benzyna";
            case FuelType.Diesel: return "Diesel";
            case FuelType.Electric: return "Elektryczny";
            case FuelType.Hybrid: return "Hybrydowy";
            case FuelType.LPG: return "LPG";
            default: return fuelType.ToString();
        }
    }

    public static string TranslateCarColor(CarColor carColor)
    {
        switch (carColor)
        {
            case CarColor.Black: return "Czarny";
            case CarColor.White: return "Biały";
            case CarColor.Silver: return "Srebrny";
            case CarColor.Gray: return "Szary";
            case CarColor.Red: return "Czerwony";
            case CarColor.Blue: return "Niebieski";
            case CarColor.Green: return "Zielony";
            case CarColor.Yellow: return "Żółty";
            case CarColor.Orange: return "Pomarańczowy";
            case CarColor.Brown: return "Brązowy";
            case CarColor.Purple: return "Fioletowy";
            case CarColor.Pink: return "Różowy";
            case CarColor.Gold: return "Złoty";
            case CarColor.Beige: return "Beżowy";
            default: return carColor.ToString();
        }
    }
    public static string TranslateTransmissionType(TransmissionType transmissionType)
    {
        switch (transmissionType)
        {
            case TransmissionType.Manual: return "Ręczna";
            case TransmissionType.Automatic: return "Automatyczna";
            case TransmissionType.SemiAutomatic: return "Półautomatyczna";
            case TransmissionType.CVT: return "CVT (przekładnia bezstopniowa)";
            default: return transmissionType.ToString();
        }
    }
    public static string TranslateAvailability(bool isAvailable)
    {
        return isAvailable ? "Dostępny" : "Niedostępny";
    }
    public static string TranslateSteeringSide(SteeringSide steeringSide)
    {
        switch (steeringSide)
        {
            case SteeringSide.Left: return "Lewa";
            case SteeringSide.Right: return "Prawa";
                default : return steeringSide.ToString();
        }
    }
}
