using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class PhineasAndFerbDataSource : IDataSource<YouTubeSchema>
    {
        private const string _url =  @"https://gdata.youtube.com/feeds/api/playlists/PLwwUQgoC4JHV1J-HTEqYLUU7L637SueKX?v=2";

        private IEnumerable<YouTubeSchema> _data = null;

        public PhineasAndFerbDataSource()
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
                    AppLogs.WriteError("PhineasAndFerbDataSourceDataSource.LoadData", ex.ToString());
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
