<<<<<<< HEAD
﻿using System;
using System.Linq.Expressions;
=======
﻿using System.Linq.Expressions;
>>>>>>> refs/remotes/MapsterMapper/master

namespace Mapster.Adapters
{
    public abstract class BaseAfterMapper
    {
        protected virtual int Score => 0;

<<<<<<< HEAD
        public virtual int? Priority(Type sourceType, Type destinationType, MapType mapType)
        {
            return CanMap(sourceType, destinationType, mapType) ? this.Score : (int?)null;
        }

        protected abstract bool CanMap(Type sourceType, Type destinationType, MapType mapType);
=======
        public virtual int? Priority(PreCompileArgument arg)
        {
            return CanMap(arg) ? this.Score : (int?)null;
        }

        protected abstract bool CanMap(PreCompileArgument arg);
>>>>>>> refs/remotes/MapsterMapper/master

        public LambdaExpression CreateAfterMapFunc(CompileArgument arg)
        {
            var p = Expression.Parameter(arg.SourceType);
            var p2 = Expression.Parameter(arg.DestinationType);
            var body = CreateExpressionBody(p, p2, arg);
            return Expression.Lambda(body, p, p2);
        }

        protected abstract Expression CreateExpressionBody(Expression source, Expression destination, CompileArgument arg);

        public TypeAdapterRule CreateRule()
        {
            var settings = new TypeAdapterSettings();
            settings.AfterMappingFactories.Add(this.CreateAfterMapFunc);
            var rule = new TypeAdapterRule
            {
                Priority = this.Priority,
                Settings = settings,
            };
            DecorateRule(rule);
            return rule;
        }

        protected virtual void DecorateRule(TypeAdapterRule rule) { }
    }
}
