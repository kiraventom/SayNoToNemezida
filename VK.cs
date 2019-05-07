using Extensions;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace SNTN
{
    namespace Core
    {
        internal static class VK
        {
            public async static Task AddPosts(VkNet.VkApi api, 
                                                    string pathToDir, 
                                                    (int h, int m)[] curricular,
                                                    DateTime publishDate,
                                                    IProgress<int> barProgress,
                                                    IProgress<string> statusProgress,
                                                    IProgress<bool> finishedProgress)
            {
                int postsAmount = curricular.Length;
                Bitmap[] photos = Photos.GetPhotosFromPath(pathToDir, postsAmount);
                
                int i;
                for (i = 0; i < postsAmount; ++i)
                {
                    try
                    {
                        barProgress.Report(i + 1);
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Генерируем подпись...");
                        var caption = Constants.Strings.CaptionThatFucksNemezida;
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Редактируем картинку...");
                        var editedPhoto = Photos.EditPhoto(photos[i]);
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Конвертируем картинку...");
                        var imageAsByteArray = editedPhoto.ToByteArray();
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Получаем адрес сервера...");
                        var usi = api.Photo.GetWallUploadServer(Properties.Settings.Default.GroupId);
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Загружаем картинку на сервер...");
                        var rspns = await UploadImage(usi.UploadUrl, imageAsByteArray);
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Получаем адрес картинки...");
                        var wallPhotos = api.Photo.SaveWallPhoto(rspns, null, (ulong)Properties.Settings.Default.GroupId);
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Постим...");
                        api.Wall.Post(new VkNet.Model.RequestParams.WallPostParams
                        {
                            OwnerId = Properties.Settings.Default.OwnerId,
                            FromGroup = true,
                            Message = caption,
                            PublishDate = new DateTime(publishDate.Year,
                                                       publishDate.Month,
                                                       publishDate.Day,
                                                       curricular[i].h,
                                                       curricular[i].m,
                                                       0),
                            Attachments = wallPhotos
                        });
                        statusProgress.Report("Готово!");
                    }
                    catch (Exception e)
                    {
                        MainForm.ShowErrorMessage(e);
                        continue;
                    }
                }
                finishedProgress.Report(true);
                return;
            }

            private static async Task<string> UploadImage(string url, byte[] data)
            {
                using (var client = new HttpClient())
                {
                    var requestContent = new MultipartFormDataContent();
                    var imageContent = new ByteArrayContent(data);
                    imageContent.Headers.ContentType =
                        System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");
                    requestContent.Add(imageContent, "photo", "image.jpg");

                    var response = await client.PostAsync(url, requestContent);

                    return await response.Content.ReadAsStringAsync();
                }
            }

            public static bool AuthViaToken(VkNet.VkApi api, string token)
            {
                try
                {
                    api.Authorize(new VkNet.Model.ApiAuthParams
                    {
                        AccessToken = token
                    });
                }
                catch
                {

                }

                return api.IsAuthorized;
            }
        }
    }
}
