using Home_Sales_DAL;

namespace Home_Sales_BAL
{
    public interface IHomeSalesDataHandler
    {
        Task UploadHomeSalesFile(Stream stream);

        List<HomeSalesModel> GetTopDistinctSchools(int month, int year);

        int GetAverageNumberOfDaysTookToSellProperty(string schoolName, int month, int year);
    }
}