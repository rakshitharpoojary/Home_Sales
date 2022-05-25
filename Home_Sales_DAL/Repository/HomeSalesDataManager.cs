using Csv;

namespace Home_Sales_DAL
{
    public class HomeSalesDataManager : IHomeSalesDataManager
    {
        private static List<HomeSalesModel> HomeSalesModelData = new List<HomeSalesModel>();

        public async Task UploadHomeSalesFile(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                TextReader textReader = new StreamReader(memoryStream);
                memoryStream.Position = 0;
                var csvLines = CsvReader.ReadFromStream(memoryStream).ToList();

                HomeSalesModelData = csvLines.Select(x => new HomeSalesModel
                {
                    PropertyZip = x.Values[0],
                    SchoolCode = x.Values[1],
                    SchoolDesc = x.Values[2],
                    RecordDate = string.IsNullOrEmpty(x.Values[3]) ? (DateTime?)null : DateTime.Parse(x.Values[3]),
                    SaleDate = string.IsNullOrEmpty(x.Values[4]) ? (DateTime?)null : DateTime.Parse(x.Values[4]),
                    Price = string.IsNullOrEmpty(x.Values[5]) ? null : Int32.Parse(x.Values[5]),
                }).ToList();
            }
        }

        public List<HomeSalesModel> GetTopDistinctSchools(int month, int year)
        {
            return HomeSalesModelData.Where(x => x.SaleDate != null && x.SaleDate.Value.Month == month && x.SaleDate.Value.Year == year)
                .GroupBy(x => x.SchoolCode, (k, g) => g.Aggregate((a, x) => (x.Price > a.Price) ? x : a))
                .Select(x => new HomeSalesModel
                {
                    SchoolCode = x.SchoolCode,
                    SchoolDesc = x.SchoolDesc
                })
                .ToList();
        }

        public int GetAverageNumberOfDaysTookToSellProperty(string schoolName, int month, int year)
        {
            var avgNumberOfDays = HomeSalesModelData.Where(x => x.SchoolDesc == schoolName && x.SaleDate != null && x.RecordDate != null && x.SaleDate.Value.Month == month && x.SaleDate.Value.Year == year)
                .Select(x => x.RecordDate.Value.Subtract(x.SaleDate.Value)).Average(x => x.Days);
            return Convert.ToInt32(avgNumberOfDays);
        }
    }
}