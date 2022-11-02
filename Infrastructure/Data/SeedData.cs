using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SeedData
    {
        public static async Task SeedAsync(ApplicationDbContext applicationDbContext,ILogger logger, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (!await applicationDbContext.Images.AnyAsync())
                {
                    var path = Environment.CurrentDirectory + "\\images";

                    var directories = Directory.GetDirectories(path);
                    using (var dbContextTransaction = applicationDbContext.Database.BeginTransaction())
                    {
                        foreach (var directory in directories)
                        {
                            var nationName = directory.Split("\\").Last();
                            var nationFromDb = applicationDbContext.Nations.Where(x => x.Name == nationName).FirstOrDefault();
                            if (nationFromDb != null)
                            {
                                var files = Directory.GetFiles(directory);
                                foreach (var file in files)
                                {
                                    Image img = new Image();

                                    var binaryImage = GetBinaryFile(file);
                                    img.ImageData = binaryImage;
                                    img.Name = file.Split("\\").Last();
                                    img.Nation = nationFromDb;

                                    await applicationDbContext.Images.AddAsync(img);
                                    await applicationDbContext.SaveChangesAsync();
                                }
                            }
                        }
                        dbContextTransaction.Commit();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(applicationDbContext, logger, retryForAvailability);
                throw;
            }
        }

        private static byte[] GetBinaryFile(string filename)
        {
            byte[] bytes;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
            }
            return bytes;
        }
    }
}
