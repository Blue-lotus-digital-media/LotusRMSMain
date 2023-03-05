using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Utility
{
    public static class ImageUpload
    {
        public static byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        public static string GetStrigFromByteArray(byte[] ImageByte)
        {
            if (ImageByte == null)
            {
                return "";
            }
            string imageBase64Data = Convert.ToBase64String(ImageByte);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            return imageDataURL;
        }
    }
}
