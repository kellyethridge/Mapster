﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mapster.Models;

namespace Mapster
{
    public static class Extensions
    {
        public static IQueryable<TDestination> ProjectToType<TDestination>(this IQueryable source, TypeAdapterConfig config = null)
        {
            config = config ?? TypeAdapterConfig.GlobalSettings;
            var mockCall = config.GetProjectionCallExpression(source.ElementType, typeof(TDestination));
            var sourceCall = Expression.Call(mockCall.Method, source.Expression, mockCall.Arguments[1]);
            return source.Provider.CreateQuery<TDestination>(sourceCall);
        }

        public static bool HasCustomAttribute(this IMemberModel member, Type type)
        {
            return member.GetCustomAttributes(true).Any(attr => attr.GetType() == type);
        }

        public static T GetCustomAttribute<T>(this IMemberModel member)
        {
            return (T) member.GetCustomAttributes(true).FirstOrDefault(attr => attr is T);
        }

        /// <summary>
        /// Determines whether the specific <paramref name="type"/> has default constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if specific <paramref name="type"/> has default constructor; otherwise <c>false</c>.
        /// </returns>
        public static bool HasDefaultConstructor(this Type type)
        {
            if (type == typeof(void)
                || type.GetTypeInfo().IsAbstract
                || type.GetTypeInfo().IsInterface)
                return false;
            if (type.GetTypeInfo().IsValueType)
                return true;
            return type.GetConstructor(Type.EmptyTypes) != null;
        }

    }
}
