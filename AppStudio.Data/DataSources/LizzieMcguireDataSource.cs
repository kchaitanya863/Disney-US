using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class LizzieMcguireDataSource : IDataSource<YouTubeSchema>
    {
        private const string _url =  @"https://gdata.youtube.com/feeds/api/playlists/PLETa4CYE1Nm9vxub5dSx69By8wWniKr-n?v=2";

        private IEnumerable<YouTubeSchema> _data = null;

        public LizzieMcguireDataSource()
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
                    AppLogs.WriteError("LizzieMcguireDataSourceDataSource.LoadData", ex.ToString());
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
