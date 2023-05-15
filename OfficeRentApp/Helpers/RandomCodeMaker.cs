namespace OfficeRentApp.Helpers
{
    public class RandomCodeMaker
    {
        public int CreateRandomCode()
        {
            Random random = new Random();
            int randomGeneratedCode = random.Next(10000, 99999);
            return randomGeneratedCode;
    }
    }
}
