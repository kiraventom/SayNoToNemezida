using Extensions;
using System;
using System.Drawing;
using System.IO;

namespace SNTN
{
    namespace Core
    {
        internal static class Photos
        {
            public static Bitmap[] GetPhotosFromPath(string path, int amount)
            {
                var di = new DirectoryInfo(path);
                var photos = new System.Collections.Generic.List<Bitmap>();

                FileInfo[] array = di.GetFiles();

                for (int i = 0; i < amount; ++i)
                {
                    if (array[i].Extension == ".jpg" ||
                        array[i].Extension == ".jpeg" ||
                        array[i].Extension == ".png")
                    {
                        using (var sr = new StreamReader(array[i].FullName))
                        {
                            photos.Add((Bitmap)Image.FromStream(sr.BaseStream));
                        }
                    }    
                }
                return photos.ToArray();
            }

            public static Bitmap EditPhoto(Bitmap originalPhoto, long groupId)
            {
                Random rnd = new Random();
                var bcm = (ImageExtensions.BrightnessChangeMode)rnd.Next(1);
                var img = new Bitmap(originalPhoto);
                img = img.ChangeBrightness(bcm);
                img = img.ChangeTone();
                img = img.MirrorHorizontal();
                img = img.AddFrame();
                //временный костыль
                if (groupId == 175746201)
                {
                    img = img.AddWatermark(Properties.Resources.group175746201, 3);
                }
                return img;
            }
        }
    }
}
