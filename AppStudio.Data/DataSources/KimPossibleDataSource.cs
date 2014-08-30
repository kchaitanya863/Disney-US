using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class KimPossibleDataSource : IDataSource<YouTubeSchema>
    {
        private const string _url =  @"https://gdata.youtube.com/feeds/api/playlists/PLVWyFemQwvMn2M6obtWy9lQzPLM2Q5oc7?v=2";

        private IEnumerable<YouTubeSchema> _data = null;

        public KimPossibleDataSource()
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
                    AppLogs.WriteError("KimPossibleDataSourceDataSource.LoadData", ex.ToString());
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
