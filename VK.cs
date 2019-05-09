using Extensions;
using System;
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
                                                    long groupId,
                                                    IProgress<int> barProgress,
                                                    IProgress<string> statusProgress,
                                                    IProgress<int> finishedProgress,
                                                    System.Threading.CancellationToken ct)
            {
                int postsAmount = curricular.Length;
                System.Drawing.Bitmap[] photos = Photos.GetPhotosFromPath(pathToDir, postsAmount);
                
                int i;
                for (i = 0; i < postsAmount; ++i)
                {
                    try
                    {
                        barProgress.Report(i + 1);
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Генерируем подпись...");
                        var caption = Constants.Strings.Caption;
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Редактируем картинку...");
                        var editedPhoto = Photos.EditPhoto(photos[i], groupId);
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Конвертируем картинку...");
                        var imageAsByteArray = editedPhoto.ToByteArray();
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Получаем адрес сервера...");
                        var usi = api.Photo.GetWallUploadServer(groupId);
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Загружаем картинку на сервер...");
                        var rspns = await UploadImage(usi.UploadUrl, imageAsByteArray);
                        if (ct.IsCancellationRequested)
                        {
                            barProgress.Report(0);
                            statusProgress.Report(string.Empty);
                            finishedProgress.Report(i);
                            return;
                        }
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Получаем адрес картинки...");
                        var wallPhotos = api.Photo.SaveWallPhoto(rspns, null, (ulong)groupId);
                        statusProgress.Report($"[{i + 1}/{postsAmount}] Постим...");
                        api.Wall.Post(new VkNet.Model.RequestParams.WallPostParams
                        {
                            OwnerId = -groupId,
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
                barProgress.Report(0);
                statusProgress.Report(string.Empty);
                finishedProgress.Report(postsAmount);
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
        }
    }
}
