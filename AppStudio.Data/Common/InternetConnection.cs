using System.Net.NetworkInformation;

namespace AppStudio.Data
{
    public static class InternetConnection
    {
        public static bool IsInternetAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}