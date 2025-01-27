using System.Drawing;
using System.Drawing.Imaging;

namespace Infrastructure.CrossCuttingConcern.Converters
{
    public class Converters
    {
        public static byte[] ImageToByteArray(Image imageIn)
        {
            
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }


        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
}
