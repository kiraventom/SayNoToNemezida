using Extensions;
using System;
using System.Drawing;
using System.IO;

namespace SNTN
{
    namespace Core
    {
        internal static class ImagesManager
        {
            public static Bitmap[] GetPhotosFromPath(string _path, int _amount)
            {
                var _directoryInfo = new DirectoryInfo(_path);
                var _images = new System.Collections.Generic.List<Bitmap>();

                FileInfo[] _filesInfo = _directoryInfo.GetFiles();

                for (int i = 0; i < _amount; ++i)
                {
                    if (_filesInfo[i].Extension == ".jpg" ||
                        _filesInfo[i].Extension == ".jpeg" ||
                        _filesInfo[i].Extension == ".png")
                    {
                        using (var _streamReader = new StreamReader(_filesInfo[i].FullName))
                        {
                            _images.Add((Bitmap)Image.FromStream(_streamReader.BaseStream));
                        }
                    }    
                }
                return _images.ToArray();
            }

            public static Bitmap EditPhoto(Bitmap _originalImage, long _groupId)
            {
                Random _random = new Random();
                var _brightnessChangeMode = (ImageExtensions.BrightnessChangeMode)_random.Next(1);
                var _editedImage = new Bitmap(_originalImage);
                _editedImage = _editedImage.ChangeBrightness(_brightnessChangeMode);
                _editedImage = _editedImage.ChangeTone();
                _editedImage = _editedImage.MirrorHorizontal();
                _editedImage = _editedImage.AddFrame();
                //temporary
                if (_groupId == 175746201)
                {
                    _editedImage = _editedImage.AddWatermark(Properties.Resources.group175746201, 3);
                }
                return _editedImage;
            }
        }
    }
}
