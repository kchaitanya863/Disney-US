using System.Threading.Tasks;
using System.Windows.Input;

using AppStudio.Data;
using AppStudio.Services;

namespace AppStudio
{
    public class MainViewModels : BindableBase
    {
       private PhineasAndFerbViewModel _phineasAndFerbModel;
       private KimPossibleViewModel _kimPossibleModel;
       private LizzieMcguireViewModel _lizzieMcguireModel;
       private HannahMontanaViewModel _hannahMontanaModel;

        private ViewModelBase _selectedItem = null;
        private PrivacyViewModel _privacyModel;

        public MainViewModels()
        {
            _selectedItem = PhineasAndFerbModel;
            _privacyModel = new PrivacyViewModel();
        }
 
        public PhineasAndFerbViewModel PhineasAndFerbModel
        {
            get { return _phineasAndFerbModel ?? (_phineasAndFerbModel = new PhineasAndFerbViewModel()); }
        }
 
        public KimPossibleViewModel KimPossibleModel
        {
            get { return _kimPossibleModel ?? (_kimPossibleModel = new KimPossibleViewModel()); }
        }
 
        public LizzieMcguireViewModel LizzieMcguireModel
        {
            get { return _lizzieMcguireModel ?? (_lizzieMcguireModel = new LizzieMcguireViewModel()); }
        }
 
        public HannahMontanaViewModel HannahMontanaModel
        {
            get { return _hannahMontanaModel ?? (_hannahMontanaModel = new HannahMontanaViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            PhineasAndFerbModel.ViewType = viewType;
            KimPossibleModel.ViewType = viewType;
            LizzieMcguireModel.ViewType = viewType;
            HannahMontanaModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public bool IsAppBarVisible
        {
            get
            {
                if (SelectedItem == null || SelectedItem == PhineasAndFerbModel)
                {
                    return true;
                }
                return SelectedItem != null ? SelectedItem.IsAppBarVisible : false;
            }
        }

        public bool IsLockScreenVisible
        {
            get { return SelectedItem == null || SelectedItem == PhineasAndFerbModel; }
        }

        public bool IsAboutVisible
        {
            get { return SelectedItem == null || SelectedItem == PhineasAndFerbModel; }
        }

        public bool IsPrivacyVisible
        {
            get { return SelectedItem == null || SelectedItem == PhineasAndFerbModel; }
        }


        public void UpdateAppBar()
        {
            OnPropertyChanged("IsAppBarVisible");
            OnPropertyChanged("IsLockScreenVisible");
            OnPropertyChanged("IsAboutVisible");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadData(bool isNetworkAvailable)
        {
            var loadTasks = new Task[]
            { 
                PhineasAndFerbModel.LoadItems(isNetworkAvailable),
                KimPossibleModel.LoadItems(isNetworkAvailable),
                LizzieMcguireModel.LoadItems(isNetworkAvailable),
                HannahMontanaModel.LoadItems(isNetworkAvailable),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }

        public ICommand LockScreenCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    LockScreenServices.SetLockScreen("/Assets/LockScreenImage.jpg");
                });
            }
        }
    }
}
