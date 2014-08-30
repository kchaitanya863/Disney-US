using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class HannahMontanaDataSource : IDataSource<YouTubeSchema>
    {
        private const string _url =  @"https://gdata.youtube.com/feeds/api/playlists/PLuKw1oWzC2BO3tDq-tWROxFCPS6jS6c3W?v=2";

        private IEnumerable<YouTubeSchema> _data = null;

        public HannahMontanaDataSource()
        {
        }

        public async Task<IEnumerable<YouTubeSchema>> LoadData()
        {
            if (_data == null)
            {
                try
                {
                    var youTubeDataProvider = new YouTubeDataProvider(_url);
                    _data = await youTubeDataProvider.Load();
                }
                catch (Exception ex)
                {
                    AppLogs.WriteError("HannahMontanaDataSourceDataSource.LoadData", ex.ToString());
                }
            }
            return _data;
        }

        public async Task<IEnumerable<YouTubeSchema>> Refresh()
        {
            _data = null;
            return await LoadData();
        }
    }
}
