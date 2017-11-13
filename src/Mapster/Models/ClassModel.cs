using System.Collections.Generic;
using System.Reflection;

namespace Mapster.Models
{
    internal class ClassModel
    {
        public ConstructorInfo ConstructorInfo { get; set; }
<<<<<<< HEAD
        public IEnumerable<IMemberModel> Members { get; set; } 
=======
        public IEnumerable<IMemberModelEx> Members { get; set; } 
>>>>>>> refs/remotes/MapsterMapper/master
    }
}
