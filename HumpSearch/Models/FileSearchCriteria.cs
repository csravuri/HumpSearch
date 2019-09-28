using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumpSearch.Models
{
    public class FileSearchCriteria
    {
        public string folderPath { set; get; }
        public string searchKeyWord { set; get; }
        public bool isNameSearch { set; get; }
        public bool isContentSearch { set; get; }
    }
}
