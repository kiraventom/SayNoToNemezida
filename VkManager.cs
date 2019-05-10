using Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SNTN
{
    namespace Core
    {
        internal static class VkManager
        {
            public async static Task AddPosts(VkNet.VkApi _api, 
                                              string _path, 
                                              (int h, int m)[] _curricular,
                                              DateTime _publishDate,
                                              long _groupId,
                                              IProgress<int> _barProgress,
                                              IProgress<string> _statusProgress,
                                              IProgress<int> _finishedProgress,
                                              System.Threading.CancellationToken _cancellationToken)
            {
                int _postsAmount = _curricular.Length;
                System.Drawing.Bitmap[] _images = ImagesManager.GetPhotosFromPath(_path, _postsAmount);
                
                for (int i = 0; i < _postsAmount; ++i)
                {
                    try
                    {
                        _barProgress.Report(i + 1);

                        _statusProgress.Report($"[{i + 1}/{_postsAmount}] Генерируем подпись...");
                        var _caption = Constants.Strings.Caption;

                        _statusProgress.Report($"[{i + 1}/{_postsAmount}] Редактируем картинку...");
                        var _editedImage = ImagesManager.EditPhoto(_images[i], _groupId);

                        _statusProgress.Report($"[{i + 1}/{_postsAmount}] Конвертируем картинку...");
                        var _imageAsByteArray = _editedImage.ToByteArray();

                        _statusProgress.Report($"[{i + 1}/{_postsAmount}] Получаем адрес сервера...");
                        var _uploadServerInfo = _api.Photo.GetWallUploadServer(_groupId);

                        _statusProgress.Report($"[{i + 1}/{_postsAmount}] Загружаем картинку на сервер...");
                        var _response = await UploadImage(_uploadServerInfo.UploadUrl, _imageAsByteArray);

                        if (_cancellationToken.IsCancellationRequested)
                        {
                            _barProgress.Report(0);
                            _statusProgress.Report(string.Empty);
                            _finishedProgress.Report(i);
                            return;
                        }

                        _statusProgress.Report($"[{i + 1}/{_postsAmount}] Получаем адрес картинки...");
                        var _wallPhotos = _api.Photo.SaveWallPhoto(_response, null, (ulong)_groupId);

                        _statusProgress.Report($"[{i + 1}/{_postsAmount}] Постим...");
                        _api.Wall.Post(new VkNet.Model.RequestParams.WallPostParams
                        {
                            OwnerId = -_groupId,
                            FromGroup = true,
                            //Message = _caption,
                            PublishDate = new DateTime(_publishDate.Year,
                                                       _publishDate.Month,
                                                       _publishDate.Day,
                                                       _curricular[i].h,
                                                       _curricular[i].m,
                                                       0),
                            Attachments = _wallPhotos
                        });

                        _statusProgress.Report("Готово!");
                    }
                    catch (Exception e)
                    {
                        MainForm.ShowVKErrorMessage(e);
                        continue;
                    }
                }
                _barProgress.Report(0);
                _statusProgress.Report(string.Empty);
                _finishedProgress.Report(_postsAmount);
                return;
            }

            private static async Task<string> UploadImage(string _url, byte[] _data)
            {
                using (var _client = new HttpClient())
                {
                    var _requestContent = new MultipartFormDataContent();
                    var _imageContent = new ByteArrayContent(_data);
                    _imageContent.Headers.ContentType =
                        System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");
                    _requestContent.Add(_imageContent, "photo", "image.jpg");

                    var _response = await _client.PostAsync(_url, _requestContent);

                    return await _response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
