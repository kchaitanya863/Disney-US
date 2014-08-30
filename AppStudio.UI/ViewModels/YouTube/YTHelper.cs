using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppStudio.Data.YouTube
{
    [DataContract]
    public class YTHelper
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Summary { get; set; }

        [DataMember]
        public string EmbedHtmlFragment { get; set; }

        [DataMember]
        public string ExternalUrl { get; set; }
    }
}
