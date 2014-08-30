using System.ComponentModel;
using System.Windows.Navigation;
using AppStudio.Data.YouTube;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AppStudio
{
    public partial class YouTubeViewer : PhoneApplicationPage, INotifyPropertyChanged
    {
        private YTViewerViewModel _youTubeModel;

        public YouTubeViewer()
        {
            InitializeComponent();
            YouTubeModel = new YTViewerViewModel();
            DataContext = YouTubeModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public YTViewerViewModel YouTubeModel
        {
            get { return _youTubeModel; }
            set
            {
                _youTubeModel = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("YouTubeModel"));
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("videoObject"))
            {
                this.YouTubeModel.SelectedItem = PhoneApplicationService.Current.State["videoObject"] as YTHelper;
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            this.YouTubeModel.SelectedItem = null;
        }
    }
}
