using System.Linq.Expressions;

namespace Mapster.Models
{
    internal class MemberMapping
    {
        public Expression Getter;
<<<<<<< HEAD
        public Expression Setter;

        public object SetterInfo;
=======
        public IMemberModelEx DestinationMember;
>>>>>>> refs/remotes/MapsterMapper/master
        public LambdaExpression SetterCondition;
    }
}