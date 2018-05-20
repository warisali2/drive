using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Entities
{
    public class File
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int ParenFolderId { get; set; }
        public String FileExt { get; set; }
        public int FileSizeInKB { get; set; }
        public DateTime UploadedOn { get; set; }
        public Boolean IsActive { get; set; }
    }
}
