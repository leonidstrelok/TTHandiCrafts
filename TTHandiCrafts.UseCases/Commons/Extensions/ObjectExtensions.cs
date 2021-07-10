using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Models.Models;

namespace TTHandiCrafts.UseCases.Commons.Extensions
{
    /// <summary>
    /// Расширяющий класс для объектов
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Загрузка изображения
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="dbContext"></param>
        public static async Task LoadDocumentAsync(this object obj, dynamic request, int id,
            IApplicationDbContext dbContext)
        {
            if (request.Image != null)
            {
                Stream document = request.Image.Stream;
                document.Position = 0;

                using var ms = new MemoryStream();
                await document.CopyToAsync(ms);

                await dbContext.Set<BinaryData>().AddAsync(new BinaryData()
                {
                    Image = ms.ToArray(),
                    FileName = request.Image.FileName,
                    AdvertisingId = id
                });
                await dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Загрузка изображений
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="dbContext"></param>
        public static async Task LoadDocumentsAsync(this object obj, dynamic request, int id,
            IApplicationDbContext dbContext)
        {
            if (request.Images != null)
            {
                var binarysData = new List<BinaryData>();
                foreach (var versionFile in request.Images)
                {
                    Stream document = versionFile.Stream;
                    document.Position = 0;

                    using var ms = new MemoryStream();
                    await document.CopyToAsync(ms);

                    binarysData.Add(new BinaryData()
                    {
                        Image = ms.ToArray(),
                        FileName = versionFile.FileName,
                        ProductId = id
                    });
                }

                await dbContext.Set<BinaryData>().AddRangeAsync(binarysData);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}