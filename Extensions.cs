using System;
using System.Drawing;

namespace Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes all non-digit characters from string
        /// </summary>
        public static int ToInt(this string _string)
        {
            for (int i = _string.Length - 1; i >= 0; --i)
            {
                if (!char.IsDigit(_string[i]))
                {
                    _string = _string.Remove(i, 1);
                }
            }
            return int.Parse(_string);
        }
        
        /// <summary>
        /// Converts string in "d[separator]M[separator]yyyy" format into date
        /// </summary>
        public static DateTime ToDate(this string _string, char _separator)
        {
            var _separatorIndex = _string.IndexOf(_separator);

            var _day = int.Parse(_string.Substring(0, _separatorIndex));
            _string = _string.Remove(0, _separatorIndex + 1);
            _separatorIndex = _string.IndexOf(_separator);

            var _month = int.Parse(_string.Substring(0, _separatorIndex));
            _string = _string.Remove(0, _separatorIndex + 1);

            var _year = int.Parse(_string.Substring(0));
            return new DateTime(_year, _month, _day);
        }
    }

    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Bitmap _image)
        {
            using (System.IO.MemoryStream _memoryStream = new System.IO.MemoryStream())
            {
                var _bitmap = new Bitmap(_image);
                _bitmap.Save(_memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return _memoryStream.ToArray();
            }
        }
    
        public static Bitmap AddFrame(this Bitmap _image, int _maximum = 50)
        {
            Random _random = new Random();
            int _frameWidth = (int)(_maximum * _random.NextDouble());
            int _frameHeight = (int)(_maximum * _random.NextDouble());
            Rectangle[] _rectangles =
            {
                new Rectangle(new Point(0, 0), new Size(_frameWidth, _image.Height)),
                new Rectangle(new Point(0, 0), new Size(_image.Width, _frameHeight)),
                new Rectangle(new Point(_image.Width - _frameWidth, 0), new Size(_frameWidth, _image.Height)),
                new Rectangle(new Point(0, _image.Height - _frameHeight), new Size(_image.Width, _frameHeight))
            };
            Brush _brush = Brushes.White;
            using (Graphics _graphics = Graphics.FromImage(_image))
            {
                _graphics.FillRectangles(_brush, _rectangles);
                _graphics.Save();
            }
            return _image;
        }

        public static Bitmap MirrorHorizontal(this Bitmap _image)
        {
            _image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return _image;
        }

        public enum BrightnessChangeMode
        {
            Lighter = 0,
            Darker = 1
        }

        public static Bitmap ChangeBrightness(this Bitmap _image,
                                              BrightnessChangeMode _brightnessChangeMode,
                                              int _minimumOpacity = 16,
                                              int _maximumOpacity = 32)
        {
            Color _overlayColor = new Color();
            if (_brightnessChangeMode == BrightnessChangeMode.Lighter)
            {
                _overlayColor = Color.White;
            }
            else if (_brightnessChangeMode == BrightnessChangeMode.Darker)
            {
                _overlayColor = Color.Black;
            }
            using (var _graphics = Graphics.FromImage(_image))
            {
                Random _random = new Random();
                var _rectangle = new Rectangle(new Point(0, 0), _image.Size);
                using (Brush _brush = new SolidBrush(Color.FromArgb(
                                                        _random.Next(_minimumOpacity, _maximumOpacity),
                                                        _overlayColor.R,
                                                        _overlayColor.G,
                                                        _overlayColor.B)))
                {
                    _graphics.FillRectangle(_brush, _rectangle);
                }
                _graphics.Save();
            }
            return _image;
        }

        public static Bitmap ChangeTone(this Bitmap _image,
                                        int _minimumOpacity = 8,
                                        int _maximumOpacity = 24)
        {
            using (var _graphics = Graphics.FromImage(_image))
            {
                Random _random = new Random();
                var _rectangle = new Rectangle(new Point(0, 0), _image.Size);
                using (Brush _brush = new SolidBrush(Color.FromArgb(
                                                        _random.Next(_minimumOpacity, _maximumOpacity),
                                                        _random.Next(256),
                                                        _random.Next(256),
                                                        _random.Next(256))))
                {
                    _graphics.FillRectangle(_brush, _rectangle);
                }
                _graphics.Save();
            }
            return _image;
        }

        public static Bitmap AddWatermark(this Bitmap _image, 
                                          Bitmap _watermark, 
                                          int _watermarkResizeParameter = 5)
        {
            using (var _graphics = Graphics.FromImage(_image))
            {
                Random _random = new Random();
                Size _imageSize = new Size(_watermark.Width / _watermarkResizeParameter, 
                                      _watermark.Height / _watermarkResizeParameter);
                Point _point = new Point(_random.Next(_image.Width - _imageSize.Width), 
                                         _random.Next(_image.Height - _imageSize.Height));

                _graphics.DrawImage(_watermark, new Rectangle(_point, _imageSize));
                _graphics.Save();
            }
            return _image;
        }
    }
}
