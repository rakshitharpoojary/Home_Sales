namespace Home_Sales_DAL
{
    public interface IHomeSalesDataManager
    {
        Task UploadHomeSalesFile(Stream stream);

        List<HomeSalesModel> GetTopDistinctSchools(int month, int year);

        int GetAverageNumberOfDaysTookToSellProperty(string schoolName, int month, int year);
    }
}