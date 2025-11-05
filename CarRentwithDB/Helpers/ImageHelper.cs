namespace CarRentwithDB.Helpers
{
    public static class ImageHelper
    {
        //static bo metoda pomocnicza, nie trzeba tworzyc new
        public static string ConvertToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length ==0)
            {
                return string.Empty;
            }
            var imgBase64 = Convert.ToBase64String(imageData);
            var image = string.Format("data:image/jpeg;base64,{0}", imgBase64);
            return image;
        }
        public static byte[] GetBytes(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
