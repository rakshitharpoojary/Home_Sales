using Home_Sales_DAL;

namespace Home_Sales_BAL
{
    public class HomeSalesDataHandler : IHomeSalesDataHandler
    {
        private IHomeSalesDataManager HomeSalesDataManager;

        public HomeSalesDataHandler(IHomeSalesDataManager homeSalesDataManager)
        {
            HomeSalesDataManager = homeSalesDataManager;
        }

        public int GetAverageNumberOfDaysTookToSellProperty(string schoolName, int month, int year)
        {
            return HomeSalesDataManager.GetAverageNumberOfDaysTookToSellProperty(schoolName, month, year);
        }

        public List<HomeSalesModel> GetTopDistinctSchools(int month, int year)

        {
            return HomeSalesDataManager.GetTopDistinctSchools(month, year);
        }

        public async Task UploadHomeSalesFile(Stream stream)
        {
            await HomeSalesDataManager.UploadHomeSalesFile(stream);
        }
    }
}