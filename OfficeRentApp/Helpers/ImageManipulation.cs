using Microsoft.EntityFrameworkCore;
using OfficeRentApp.Data;
using OfficeRentApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRentApp.Helpers
{
    public class ImageManipulation
    {

        private readonly IWebHostEnvironment _environment;
        private readonly OfficeRentDbContext _context;
        public ImageManipulation(IWebHostEnvironment environment, OfficeRentDbContext context)
        {
            _environment = environment;
            _context = context;
        }
        public int Id { get; set; }
        [ForeignKey("Office")]
        public int OfficeId { get; set; }
        public Office? Office { get; set; }
        public string ImageUrl { get; set; }

        public ImageManipulation() 
        { 

        }
        public void ImageAdd(int officeId, IEnumerable<IFormFile> objfiles)
        {
            try
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");

                }
                foreach (var file in objfiles) 
                {
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + DateTime.Now.ToShortDateString() + file.FileName ))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        var image = new ImageManipulation()
                        {
                            OfficeId = officeId,
                            ImageUrl = "\\Upload\\" + DateTime.Now.ToShortDateString() + file.FileName
                        };
                        _context.Images.Add(image);
                        _context.SaveChanges();
                    }
                }

                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
