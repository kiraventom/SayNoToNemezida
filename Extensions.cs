using System;
using System.Drawing;

namespace Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes all non-digit chars from string and returns result as int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            for (int i = str.Length - 1; i >= 0; --i)
            {
                if (!char.IsDigit(str[i]))
                {
                    str = str.Remove(i, 1);
                }
            }
            return int.Parse(str);
        }

        /// <summary>
        /// Преобразует строку формата dd/MM/yyyy в DateTime
        /// </summary>
        public static DateTime ToDate(this string str, char separator)
        {
            var separatorIndex = str.IndexOf(separator);
            var day = int.Parse(str.Substring(0, separatorIndex));
            str = str.Remove(0, separatorIndex + 1);

            separatorIndex = str.IndexOf(separator);
            var month = int.Parse(str.Substring(0, separatorIndex));
            str = str.Remove(0, separatorIndex + 1);

            var year = int.Parse(str.Substring(0));

            return new DateTime(year, month, day);
        }
    }


    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Bitmap image)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                var bmp = new Bitmap(image);
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    
        public static Bitmap AddFrame(this Bitmap original, int maximum = 50)
        {
            var modified = new Bitmap(original);
            Random rnd = new Random();
            int frameWidth = (int)(maximum * rnd.NextDouble());
            int frameHeight = (int)(maximum * rnd.NextDouble());
            Rectangle[] rectangles =
            {
                new Rectangle(new Point(0, 0), new Size(frameWidth, modified.Height)),
                new Rectangle(new Point(0, 0), new Size(modified.Width, frameHeight)),
                new Rectangle(new Point(modified.Width - frameWidth, 0), new Size(frameWidth, modified.Height)),
                new Rectangle(new Point(0, modified.Height - frameHeight), new Size(modified.Width, frameHeight))
            };
            Brush brush = Brushes.White;
            using (Graphics g = Graphics.FromImage(modified))
            {
                g.FillRectangles(brush, rectangles);
                g.Save();
            }
            return modified;
        }

        public static Bitmap MirrorHorizontal(this Bitmap bmp)
        {
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return bmp;
        }

        public enum BrightnessChangeMode
        {
            Lighter = 0,
            Darker = 1
        }

        public static Bitmap ChangeBrightness(this Bitmap bmp,
                                              BrightnessChangeMode bcm,
                                              int minimumOpacity = 16,
                                              int maximumOpacity = 32)
        {
            Color modificator = new Color();
            if (bcm == BrightnessChangeMode.Lighter)
            {
                modificator = Color.White;
            }
            else if (bcm == BrightnessChangeMode.Darker)
            {
                modificator = Color.Black;
            }
            using (var g = Graphics.FromImage(bmp))
            {
                Random rnd = new Random();
                var rect = new Rectangle(new Point(0, 0), bmp.Size);
                using (Brush brush = new SolidBrush(Color.FromArgb(
                                                        rnd.Next(minimumOpacity, maximumOpacity),
                                                        modificator.R,
                                                        modificator.G,
                                                        modificator.B)))
                {
                    g.FillRectangle(brush, rect);
                }
                g.Save();
            }
            return bmp;
        }

        public static Bitmap ChangeTone(this Bitmap bmp,
                                        int minimumOpacity = 8,
                                        int maximumOpacity = 24)
        {
            using (var g = Graphics.FromImage(bmp))
            {
                Random rnd = new Random();
                var rect = new Rectangle(new Point(0, 0), bmp.Size);
                using (Brush brush = new SolidBrush(Color.FromArgb(
                                                        rnd.Next(minimumOpacity, maximumOpacity),
                                                        rnd.Next(256),
                                                        rnd.Next(256),
                                                        rnd.Next(256))))
                {
                    g.FillRectangle(brush, rect);
                }
                g.Save();
            }
            return bmp;
        }

        public static Bitmap AddWatermark(this Bitmap bmp, Bitmap watermark, int watermarkResizeParameter = 5)
        {
            using (var g = Graphics.FromImage(bmp))
            {
                Random rnd = new Random();
                Size s = new Size(watermark.Width / watermarkResizeParameter, 
                                  watermark.Height / watermarkResizeParameter);
                g.DrawImage(watermark, new Rectangle(new Point(rnd.Next(bmp.Width - s.Width), 
                                                               rnd.Next(bmp.Height - s.Height)), 
                                                     s));
                g.Save();
            }
            return bmp;
        }
    }
}
