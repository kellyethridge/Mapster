using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq.Expressions;
=======
>>>>>>> refs/remotes/MapsterMapper/master

namespace Mapster.Models
{
    public interface IMemberModel
    {
        Type Type { get; }
        string Name { get; }
        object Info { get; }
        AccessModifier SetterModifier { get; }
<<<<<<< HEAD
=======
        AccessModifier AccessModifier { get; }
>>>>>>> refs/remotes/MapsterMapper/master

        IEnumerable<object> GetCustomAttributes(bool inherit);
    }
}
