using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Entities
{
    public class Folder
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int ParentFolderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
