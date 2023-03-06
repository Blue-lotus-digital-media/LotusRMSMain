
using System.Drawing;

namespace LotusRMS.Utility
{
    public static class QrCode
    {
        public static Byte[] BitmapToBytes(Bitmap imageBitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                imageBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}