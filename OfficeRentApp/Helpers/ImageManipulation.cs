namespace OfficeRentApp.Helpers
{
    public class ImageManipulation
    {

        private readonly IWebHostEnvironment _environment;
        public ImageManipulation(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        

        public void ImageAdd(IFormFile objfile)
        {
            try
            {
                if (objfile.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + DateTime.Now.ToShortDateString() + objfile.FileName ))
                    {
                        objfile.CopyTo(fileStream);
                        fileStream.Flush();
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
