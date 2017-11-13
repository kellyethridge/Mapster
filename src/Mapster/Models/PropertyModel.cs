using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Mapster.Models
{
    public class PropertyModel : IMemberModelEx
    {
        private readonly PropertyInfo _propertyInfo;
        public PropertyModel(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public Type Type => _propertyInfo.PropertyType;
        public virtual string Name => _propertyInfo.Name;
        public object Info => _propertyInfo;
<<<<<<< HEAD

        public AccessModifier SetterModifier
        {
            get
            {
                var setter = _propertyInfo.GetSetMethod();
                if (setter == null)
                    return AccessModifier.None;

                if (setter.IsFamilyOrAssembly)
                    return AccessModifier.Protected | AccessModifier.Internal;
                if (setter.IsFamily)
                    return AccessModifier.Protected;
                if (setter.IsAssembly)
                    return AccessModifier.Internal;
                if (setter.IsPublic)
                    return AccessModifier.Public;
                return AccessModifier.Private;
            }
        }
=======
>>>>>>> refs/remotes/MapsterMapper/master

        public AccessModifier SetterModifier
        {
            get
            {
                var setter = _propertyInfo.GetSetMethod();
                return setter?.GetAccessModifier() ?? AccessModifier.None;
            }
        }
        public AccessModifier AccessModifier
        {
            get
            {
                var getter = _propertyInfo.GetGetMethod();
                return getter?.GetAccessModifier() ?? AccessModifier.None;
            }
        }

        public virtual Expression GetExpression(Expression source)
        {
            return Expression.Property(source, _propertyInfo);
        }
        public Expression SetExpression(Expression source, Expression value)
        {
            return Expression.Assign(GetExpression(source), value);
        }
        public IEnumerable<object> GetCustomAttributes(bool inherit)
        {
            return _propertyInfo.GetCustomAttributes(inherit);
        }

    }
}
