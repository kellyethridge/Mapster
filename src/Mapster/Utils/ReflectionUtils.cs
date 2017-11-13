using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mapster.Models;
using Mapster.Utils;

// ReSharper disable once CheckNamespace
namespace Mapster
{
    internal static class ReflectionUtils
    {
        // Primitive types with their conversion methods from System.Convert class.
        private static readonly Dictionary<Type, string> _primitiveTypes = new Dictionary<Type, string>() {
            { typeof(bool), "ToBoolean" },
            { typeof(short), "ToInt16" },
            { typeof(int), "ToInt32" },
            { typeof(long), "ToInt64" },
            { typeof(float), "ToSingle" },
            { typeof(double), "ToDouble" },
            { typeof(decimal), "ToDecimal" },
            { typeof(ushort), "ToUInt16" },
            { typeof(uint), "ToUInt32" },
            { typeof(ulong), "ToUInt64" },
            { typeof(byte), "ToByte" },
            { typeof(sbyte), "ToSByte" },
            { typeof(DateTime), "ToDateTime" }
        };

#if NET40
        public static Type GetTypeInfo(this Type type) {
            return type;
        }
#endif

        public static bool IsNullable(this Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>);
        }

<<<<<<< HEAD
        public static bool IsPoco(this Type type)
        {
            if (type.GetTypeInfo().IsEnum)
                return false;

            return type.GetFieldsAndProperties(allowNoSetter: false).Any();
        }

        public static IEnumerable<IMemberModel> GetFieldsAndProperties(this Type type, bool allowNonPublicSetter = true, bool allowNoSetter = true, BindingFlags accessorFlags = BindingFlags.Public)
        {
            var bindingFlags = BindingFlags.Instance | accessorFlags;

            var properties = type.GetProperties(bindingFlags)
                .Where(x => (allowNoSetter || x.CanWrite) && (allowNonPublicSetter || x.GetSetMethod() != null))
                .Select(CreateModel);

            var fields = type.GetFields(bindingFlags)
                .Where(x => (allowNoSetter || !x.IsInitOnly))
                .Select(CreateModel);

=======
        public static bool IsPoco(this Type type, BindingFlags accessorFlags = BindingFlags.Public)
        {
            //not nullable
            if (type.IsNullable())
                return false;

            //not primitives
            if (type.IsConvertible())
                return false;

            return type.GetFieldsAndProperties(allowNoSetter: false, accessorFlags: accessorFlags).Any();
        }

        public static IEnumerable<IMemberModelEx> GetFieldsAndProperties(this Type type, bool allowNoSetter = true, BindingFlags accessorFlags = BindingFlags.Public)
        {
            var bindingFlags = BindingFlags.Instance | accessorFlags;

            var properties = type.GetProperties(bindingFlags)
                .Where(x => allowNoSetter || x.CanWrite)
                .Select(CreateModel);

            var fields = type.GetFields(bindingFlags)
                .Where(x => allowNoSetter || !x.IsInitOnly)
                .Select(CreateModel);

>>>>>>> refs/remotes/MapsterMapper/master
            return properties.Concat(fields);
        }

        public static bool IsCollection(this Type type)
        {
            return typeof (IEnumerable).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) && type != typeof(string);
        }

        public static Type ExtractCollectionType(this Type collectionType)
        {
<<<<<<< HEAD
            var enumerableType = collectionType.GetGenericEnumerableType();
            return enumerableType != null 
                ? enumerableType.GetGenericArguments()[0] 
=======
            if (collectionType.IsArray)
                return collectionType.GetElementType();
            var enumerableType = collectionType.GetGenericEnumerableType();
            return enumerableType != null
                ? enumerableType.GetGenericArguments()[0]
>>>>>>> refs/remotes/MapsterMapper/master
                : typeof (object);
        }

        public static bool IsGenericEnumerableType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof (IEnumerable<>);
        }

        public static Type GetInterface(this Type type, Func<Type, bool> predicate)
        {
            if (predicate(type))
                return type;
<<<<<<< HEAD
            
=======

>>>>>>> refs/remotes/MapsterMapper/master
            return type.GetInterfaces().FirstOrDefault(predicate);
        }

        public static Type GetGenericEnumerableType(this Type type)
        {
            return type.GetInterface(IsGenericEnumerableType);
        }

<<<<<<< HEAD
        private static Expression CreateConvertMethod(string name, Type srcType, Type destType, Expression source)
=======
        public static Expression CreateConvertMethod(Type srcType, Type destType, Expression source)
>>>>>>> refs/remotes/MapsterMapper/master
        {
            var name = _primitiveTypes.GetValueOrDefault(destType);

            if (name == null)
                return null;

            var method = typeof (Convert).GetMethod(name, new[] {srcType});
            if (method != null)
                return Expression.Call(method, source);

            method = typeof (Convert).GetMethod(name, new[] {typeof (object)});
            return Expression.Convert(Expression.Call(method, Expression.Convert(source, typeof (object))), destType);
        }

        public static object GetDefault(this Type type)
        {
            return type.GetTypeInfo().IsValueType && !type.IsNullable()
                ? Activator.CreateInstance(type)
                : null;
        }

        public static Type UnwrapNullable(this Type type)
        {
<<<<<<< HEAD
            var srcType = sourceType.IsNullable() ? sourceType.GetGenericArguments()[0] : sourceType;
            var destType = destinationType.IsNullable() ? destinationType.GetGenericArguments()[0] : destinationType;

            if (srcType == destType)
                return source;

            //special handling for string
            if (destType == _stringType)
            {
                if (srcType.GetTypeInfo().IsEnum)
                {
                    var method = typeof (Enum<>).MakeGenericType(srcType).GetMethod("ToString", new[] {srcType});
                    return Expression.Call(method, source);
                }
                else
                {
                    var method = srcType.GetMethod("ToString", Type.EmptyTypes);
                    return Expression.Call(source, method);
                }
            }

            if (srcType == _stringType)
            {
                if (destType.GetTypeInfo().IsEnum)
                {
                    var method = typeof (Enum<>).MakeGenericType(destType).GetMethod("Parse", new[] {typeof (string)});
                    return Expression.Call(method, source);
                }
                else
                {
                    var method = destType.GetMethod("Parse", new[] {typeof (string)});
                    if (method != null)
                        return Expression.Call(method, source);
                }
            }

            //try using type casting
            try
            {
                return Expression.Convert(source, destType);
            }
            catch
            {
                // ignored
            }

            if (!srcType.IsConvertible())
                throw new InvalidOperationException(
                    $"Cannot convert immutable type, please consider using 'MapWith' method to create mapping: TSource: {sourceType} TDestination: {destinationType}");

            //using Convert
            if (destType == typeof (bool))
                return CreateConvertMethod("ToBoolean", srcType, destType, source);

            if (destType == typeof (int))
                return CreateConvertMethod("ToInt32", srcType, destType, source);

            if (destType == typeof (long))
                return CreateConvertMethod("ToInt64", srcType, destType, source);

            if (destType == typeof (short))
                return CreateConvertMethod("ToInt16", srcType, destType, source);

            if (destType == typeof (decimal))
                return CreateConvertMethod("ToDecimal", srcType, destType, source);

            if (destType == typeof (double))
                return CreateConvertMethod("ToDouble", srcType, destType, source);

            if (destType == typeof (float))
                return CreateConvertMethod("ToSingle", srcType, destType, source);

            if (destType == typeof (DateTime))
                return CreateConvertMethod("ToDateTime", srcType, destType, source);

            if (destType == typeof (ulong))
                return CreateConvertMethod("ToUInt64", srcType, destType, source);

            if (destType == typeof (uint))
                return CreateConvertMethod("ToUInt32", srcType, destType, source);

            if (destType == typeof (ushort))
                return CreateConvertMethod("ToUInt16", srcType, destType, source);

            if (destType == typeof (byte))
                return CreateConvertMethod("ToByte", srcType, destType, source);

            if (destType == typeof (sbyte))
                return CreateConvertMethod("ToSByte", srcType, destType, source);

            var changeTypeMethod = typeof (Convert).GetMethod("ChangeType", new[] {typeof (object), typeof (Type)});
            return Expression.Convert(Expression.Call(changeTypeMethod, Expression.Convert(source, typeof (object)), Expression.Constant(destType)), destType);
=======
            return type.IsNullable() ? type.GetGenericArguments()[0] : type;
>>>>>>> refs/remotes/MapsterMapper/master
        }

        public static MemberExpression GetMemberInfo(Expression member, bool source = false)
        {
            var lambda = member as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException(nameof(member));

            var expr = lambda.Body;
            if (lambda.Body.NodeType == ExpressionType.Convert)
                expr = ((UnaryExpression)lambda.Body).Operand;

            MemberExpression memberExpr = null;
            if (expr.NodeType == ExpressionType.MemberAccess)
            {
                var tmp = (MemberExpression) expr;
                if (tmp.Expression.NodeType == ExpressionType.Parameter)
                    memberExpr = tmp;
                else if (!source)
                    throw new ArgumentException("Only first level member access on destination allowed (eg. dest => dest.Name)", nameof(member));
            }

            if (memberExpr == null && !source)
                throw new ArgumentException("Argument must be member access", nameof(member));

            return memberExpr;
        }

<<<<<<< HEAD
        public static Expression GetDeepFlattening(Expression source, string propertyName, CompileArgument arg)
        {
            var strategy = arg.Settings.NameMatchingStrategy;
            var properties = source.Type.GetFieldsAndProperties();
            foreach (var property in properties)
            {
                var sourceMemberName = strategy.SourceMemberNameConverter(property.Name);
                var propertyType = property.Type;
                if (propertyType.GetTypeInfo().IsClass && propertyType != _stringType
                    && propertyName.StartsWith(sourceMemberName))
                {
                    var exp = property.GetExpression(source);
                    var ifTrue = GetDeepFlattening(exp, propertyName.Substring(sourceMemberName.Length).TrimStart('_'), arg);
                    if (ifTrue == null)
                        return null;
                    if (arg.MapType == MapType.Projection)
                        return ifTrue;
                    return Expression.Condition(
                        Expression.Equal(exp, Expression.Constant(null, exp.Type)),
                        Expression.Constant(ifTrue.Type.GetDefault(), ifTrue.Type),
                        ifTrue);
                }
                else if (string.Equals(propertyName, sourceMemberName))
                {
                    return property.GetExpression(source);
                }
            }
            return null;
        }

=======
>>>>>>> refs/remotes/MapsterMapper/master
        public static bool IsReferenceAssignableFrom(this Type destType, Type srcType)
        {
            if (destType == srcType)
                return true;

            if (!destType.GetTypeInfo().IsValueType && !srcType.GetTypeInfo().IsValueType && destType.GetTypeInfo().IsAssignableFrom(srcType.GetTypeInfo()))
                return true;

            return false;
        }

        public static bool IsRecordType(this Type type)
        {
            //not nullable
            if (type.IsNullable())
                return false;

            //not primitives
            if (type.IsConvertible())
                return false;

            //no setter
            var props = type.GetFieldsAndProperties().ToList();
            if (props.Any(p => p.SetterModifier != AccessModifier.None))
                return false;

            //1 non-empty constructor
            var ctors = type.GetConstructors().Where(ctor => ctor.GetParameters().Length > 0).ToList();
            if (ctors.Count != 1)
                return false;

            //all parameters should match getter
<<<<<<< HEAD
            var names = props.Select(p => p.Name.ToPascalCase()).ToHashSet();
            return names.SetEquals(ctors[0].GetParameters().Select(p => p.Name.ToPascalCase()));
=======
            return props.All(prop =>
            {
                var name = prop.Name.ToPascalCase();
                return ctors[0].GetParameters().Any(p => p.ParameterType == prop.Type && p.Name?.ToPascalCase() == name);
            });
>>>>>>> refs/remotes/MapsterMapper/master
        }

        public static bool IsConvertible(this Type type)
        {
            return typeof (IConvertible).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

        public static IMemberModelEx CreateModel(this PropertyInfo propertyInfo)
        {
            return new PropertyModel(propertyInfo);
        }

        public static IMemberModelEx CreateModel(this FieldInfo propertyInfo)
        {
            return new FieldModel(propertyInfo);
        }

        public static IMemberModelEx CreateModel(this ParameterInfo propertyInfo)
        {
            return new ParameterModel(propertyInfo);
        }

        public static bool IsAssignableFromList(this Type type)
        {
            var elementType = type.ExtractCollectionType();
            var listType = typeof(List<>).MakeGenericType(elementType);
            return type.GetTypeInfo().IsAssignableFrom(listType.GetTypeInfo());
        }

        public static bool IsListCompatible(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsInterface)
                return type.IsAssignableFromList();

            if (typeInfo.IsAbstract)
                return false;

            var elementType = type.ExtractCollectionType();
            if (typeof(ICollection<>).MakeGenericType(elementType).GetTypeInfo().IsAssignableFrom(typeInfo))
                return true;

            if (typeof(IList).GetTypeInfo().IsAssignableFrom(typeInfo))
                return true;

            return false;
        }

        public static Type GetDictionaryType(this Type destinationType)
        {
            return destinationType.GetInterface(type => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>));
        }
<<<<<<< HEAD
=======

        public static AccessModifier GetAccessModifier(this FieldInfo memberInfo)
        {
            if (memberInfo.IsFamilyOrAssembly)
                return AccessModifier.Protected | AccessModifier.Internal;
            if (memberInfo.IsFamily)
                return AccessModifier.Protected;
            if (memberInfo.IsAssembly)
                return AccessModifier.Internal;
            if (memberInfo.IsPublic)
                return AccessModifier.Public;
            return AccessModifier.Private;
        }

        public static AccessModifier GetAccessModifier(this MethodBase methodBase)
        {
            if (methodBase.IsFamilyOrAssembly)
                return AccessModifier.Protected | AccessModifier.Internal;
            if (methodBase.IsFamily)
                return AccessModifier.Protected;
            if (methodBase.IsAssembly)
                return AccessModifier.Internal;
            if (methodBase.IsPublic)
                return AccessModifier.Public;
            return AccessModifier.Private;
        }

        public static bool ShouldMapMember(this IMemberModel member, IEnumerable<Func<IMemberModel, MemberSide, bool?>> predicates, MemberSide side)
        {
            return predicates.Select(predicate => predicate(member, side))
                .FirstOrDefault(result => result != null) == true;
        }

        public static string GetMemberName(this IMemberModel member, List<Func<IMemberModel, string>> getMemberNames, Func<string, string> nameConverter)
        {
            return getMemberNames.Select(predicate => predicate(member))
                .FirstOrDefault(name => name != null)
                ?? nameConverter(member.Name);
        }

        public static bool IsPrimitiveKind(this Type type)
        {
            return type == typeof(object) || type.UnwrapNullable().IsConvertible();
        }
>>>>>>> refs/remotes/MapsterMapper/master
    }
}