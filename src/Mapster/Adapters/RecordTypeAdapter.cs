﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mapster.Models;
using Mapster.Utils;

namespace Mapster.Adapters
{
    internal class RecordTypeAdapter : BaseClassAdapter
    {
<<<<<<< HEAD
        protected override int Score => -151;

        protected override bool CanMap(Type sourceType, Type destinationType, MapType mapType)
        {
            if (sourceType == typeof (string) || sourceType == typeof (object))
                return false;

            if (!destinationType.IsRecordType())
=======
        protected override int Score => -149;

        protected override bool CanMap(PreCompileArgument arg)
        {
            if (!arg.DestinationType.IsRecordType())
>>>>>>> refs/remotes/MapsterMapper/master
                return false;

            return true;
        }

        protected override Expression CreateInstantiationExpression(Expression source, Expression destination, CompileArgument arg)
        {
            //new TDestination(src.Prop1, src.Prop2)

            if (arg.Settings.ConstructUsingFactory != null)
<<<<<<< HEAD
                return base.CreateInstantiationExpression(source, arg);
=======
                return base.CreateInstantiationExpression(source, destination, arg);
>>>>>>> refs/remotes/MapsterMapper/master

            var classConverter = CreateClassConverter(source, null, arg);
            var members = classConverter.Members;

            var arguments = new List<Expression>();
            foreach (var member in members)
            {
                var parameterInfo = (ParameterInfo) member.DestinationMember.Info;
                var defaultValue = parameterInfo.IsOptional ? parameterInfo.DefaultValue : parameterInfo.ParameterType.GetDefault();
                var defaultConst = Expression.Constant(defaultValue, member.DestinationMember.Type);

                Expression getter;
                if (member.Getter == null)
                {
                    getter = defaultConst;
                }
                else
                {
                    getter = CreateAdaptExpression(member.Getter, member.DestinationMember.Type, arg);

                    if (arg.Settings.IgnoreNullValues == true && (!member.Getter.Type.GetTypeInfo().IsValueType || member.Getter.Type.IsNullable()))
                    {
                        var condition = Expression.NotEqual(member.Getter, Expression.Constant(null, member.Getter.Type));
                        getter = Expression.Condition(condition, getter, defaultConst);
                    }
                    if (member.SetterCondition != null)
                    {
                        var condition = Expression.Not(member.SetterCondition.Apply(source, Expression.Constant(arg.DestinationType.GetDefault(), arg.DestinationType)));
                        getter = Expression.Condition(condition, getter, defaultConst);
                    }
                }
                arguments.Add(getter);
            }

            return Expression.New(classConverter.ConstructorInfo, arguments);
        }

        protected override Expression CreateBlockExpression(Expression source, Expression destination, CompileArgument arg)
        {
            return Expression.Empty();
        }

        protected override Expression CreateInlineExpression(Expression source, CompileArgument arg)
        {
            return CreateInstantiationExpression(source, arg);
        }

        protected override ClassModel GetClassModel(Type destinationType, CompileArgument arg)
        {
<<<<<<< HEAD
            var props = destinationType.GetFieldsAndProperties();
            var names = props.Select(p => p.Name.ToPascalCase()).ToHashSet();
            return (from ctor in destinationType.GetConstructors()
                    let ps = ctor.GetParameters()
                    where ps.Length > 0 && names.IsSupersetOf(ps.Select(p => p.Name.ToPascalCase()))
                    orderby ps.Length descending
                    select new ClassModel
                    {
                        ConstructorInfo = ctor,
                        Members = ps.Select(ReflectionUtils.CreateModel)
                    }).First();
=======
            var ctor = destinationType.GetConstructors()[0];
            return new ClassModel
            {
                ConstructorInfo = ctor,
                Members = ctor.GetParameters().Select(ReflectionUtils.CreateModel)
            };
>>>>>>> refs/remotes/MapsterMapper/master
        }

    }

}
