﻿using Mapster.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mapster.Adapters;
using Mapster.Utils;
<<<<<<< HEAD
=======
using System.Runtime.CompilerServices;
>>>>>>> refs/remotes/MapsterMapper/master

namespace Mapster
{
    public class TypeAdapterConfig
    {
        public static List<TypeAdapterRule> RulesTemplate { get; } = CreateRuleTemplate();
<<<<<<< HEAD
        public static List<Func<Expression, IMemberModel, CompileArgument, Expression>> ValueAccessingStrategiesTemplate { get; } = ValueAccessingStrategy.GetDefaultStrategies();

        private static TypeAdapterConfig _globalSettings;

        public static TypeAdapterConfig GlobalSettings
        {
            get { return _globalSettings ?? (_globalSettings = new TypeAdapterConfig()); }
        }
=======

        private static TypeAdapterConfig _globalSettings;
        public static TypeAdapterConfig GlobalSettings => _globalSettings ?? (_globalSettings = new TypeAdapterConfig());
>>>>>>> refs/remotes/MapsterMapper/master

        private static List<TypeAdapterRule> CreateRuleTemplate()
        {
            return new List<TypeAdapterRule>
            {
<<<<<<< HEAD
                new PrimitiveAdapter().CreateRule(),
                new RecordTypeAdapter().CreateRule(),
                new ClassAdapter().CreateRule(),
                new DictionaryAdapter().CreateRule(),
                new CollectionAdapter().CreateRule(),
=======
                new PrimitiveAdapter().CreateRule(),    //-200
                new ClassAdapter().CreateRule(),        //-150
                new RecordTypeAdapter().CreateRule(),   //-149
                new CollectionAdapter().CreateRule(),   //-125
                new DictionaryAdapter().CreateRule(),   //-124
                new ArrayAdapter().CreateRule(),        //-123
                new MultiDimensionalArrayAdapter().CreateRule(), //-122
                new ObjectAdapter().CreateRule(),       //-111
                new StringAdapter().CreateRule(),       //-110
                new EnumAdapter().CreateRule(),         //-109

                //fallback rules
                new TypeAdapterRule
                {
                    Priority = arg => -200,
                    Settings = new TypeAdapterSettings
                    {
                        //match exact name
                        NameMatchingStrategy = NameMatchingStrategy.Exact,  
                        ShouldMapMember =
                        {
                            ShouldMapMember.IgnoreAdaptIgnore,      //ignore AdaptIgnore attribute
                            ShouldMapMember.AllowPublic,            //match public prop
                            ShouldMapMember.AllowAdaptMember,       //match AdaptMember attribute
                        },                                          
                        GetMemberNames =                            
                        {                                           
                            GetMemberName.AdaptMember,              //get name using AdaptMember attribute
                        },
                        ValueAccessingStrategies =
                        {
                            ValueAccessingStrategy.CustomResolver,  //get value from Map
                            ValueAccessingStrategy.PropertyOrField, //get value from properties/fields
                            ValueAccessingStrategy.GetMethod,       //get value from get method
                            ValueAccessingStrategy.FlattenMember,   //get value from chain of properties
                        }
                    }
                },
>>>>>>> refs/remotes/MapsterMapper/master

                //dictionary accessor
                new TypeAdapterRule
                {
<<<<<<< HEAD
                    Priority = (srcType, destType, mapType) => srcType.GetDictionaryType()?.GetGenericArguments()[0] == typeof(string) ? -149 : (int?)null,
                    Settings = new TypeAdapterSettings
                    {
                        ValueAccessingStrategies = new[] { ValueAccessingStrategy.Dictionary }.ToList(),
=======
                    Priority = arg => arg.SourceType.GetDictionaryType()?.GetGenericArguments()[0] == typeof(string) ? -124 : (int?)null,
                    Settings = new TypeAdapterSettings
                    {
                        ValueAccessingStrategies =
                        {
                            ValueAccessingStrategy.CustomResolverForDictionary,
                            ValueAccessingStrategy.Dictionary,
                        },
>>>>>>> refs/remotes/MapsterMapper/master
                    }
                }
            };
        }

        public bool RequireDestinationMemberSource { get; set; }
        public bool RequireExplicitMapping { get; set; }
        public bool AllowImplicitDestinationInheritance { get; set; }
<<<<<<< HEAD

        public List<TypeAdapterRule> Rules { get; protected set; }
        public TypeAdapterSetter Default { get; protected set; }
        public Dictionary<TypeTuple, TypeAdapterRule> RuleMap { get; protected set; } = new Dictionary<TypeTuple, TypeAdapterRule>();
=======
        public bool AllowImplicitSourceInheritance { get; set; } = true;

        internal static Func<LambdaExpression, Delegate> DefaultCompiler = lambda => lambda.Compile();
        public Func<LambdaExpression, Delegate> Compiler { get; set; } = DefaultCompiler;

        public List<TypeAdapterRule> Rules { get; internal set; }
        public TypeAdapterSetter Default { get; internal set; }
        public Dictionary<TypeTuple, TypeAdapterRule> RuleMap { get; internal set; } = new Dictionary<TypeTuple, TypeAdapterRule>();
>>>>>>> refs/remotes/MapsterMapper/master

        public TypeAdapterConfig()
        {
            this.Rules = RulesTemplate.ToList();
<<<<<<< HEAD
            var settings = new TypeAdapterSettings
            {
                ValueAccessingStrategies = ValueAccessingStrategiesTemplate.ToList(),
                NameMatchingStrategy = NameMatchingStrategy.Exact,
            };
            this.Default = new TypeAdapterSetter(settings, this);
            this.Rules.Add(new TypeAdapterRule
            {
                Priority = (sourceType, destinationType, mapType) => -100,
=======
            var settings = new TypeAdapterSettings();
            this.Default = new TypeAdapterSetter(settings, this);
            this.Rules.Add(new TypeAdapterRule
            {
                Priority = arg => -100,
>>>>>>> refs/remotes/MapsterMapper/master
                Settings = settings,
            });
        }

        public TypeAdapterSetter When(Func<Type, Type, MapType, bool> canMap)
        {
            var rule = new TypeAdapterRule
            {
                Priority = arg => canMap(arg.SourceType, arg.DestinationType, arg.MapType) ? (int?)25 : null,
                Settings = new TypeAdapterSettings(),
            };
            this.Rules.Add(rule);
            return new TypeAdapterSetter(rule.Settings, this);
        }

        public TypeAdapterSetter When(Func<PreCompileArgument, bool> canMap) 
        {
            var rule = new TypeAdapterRule
            {
                Priority = arg => canMap(arg) ? (int?)25 : null,
                Settings = new TypeAdapterSettings(),
            };
            this.Rules.Add(rule);
            return new TypeAdapterSetter(rule.Settings, this);
        }

        public TypeAdapterSetter<TSource, TDestination> NewConfig<TSource, TDestination>()
        {
            Remove(typeof(TSource), typeof(TDestination));
            return ForType<TSource, TDestination>();
        }
<<<<<<< HEAD

        public TypeAdapterSetter<TSource, TDestination> ForType<TSource, TDestination>()
        {
            var key = new TypeTuple(typeof(TSource), typeof(TDestination));
            var settings = GetSettings(key);
            return new TypeAdapterSetter<TSource, TDestination>(settings, this);
        }

        public TypeAdapterSetter<TDestination> ForDestinationType<TDestination>()
        {
            var key = new TypeTuple(typeof(void), typeof(TDestination));
            var settings = GetSettings(key);
            return new TypeAdapterSetter<TDestination>(settings, this);
        }

        private TypeAdapterSettings GetSettings(TypeTuple key)
        {
            TypeAdapterRule rule;
            if (!this.RuleMap.TryGetValue(key, out rule))
=======

        public TypeAdapterSetter NewConfig(Type sourceType, Type destinationType)
        {
            Remove(sourceType, destinationType);
            return ForType(sourceType, destinationType);
        }

        public TypeAdapterSetter<TSource, TDestination> ForType<TSource, TDestination>()
        {
            var key = new TypeTuple(typeof(TSource), typeof(TDestination));
            var settings = GetSettings(key);
            return new TypeAdapterSetter<TSource, TDestination>(settings, this);
        }

        public TypeAdapterSetter ForType(Type sourceType, Type destinationType)
        {
            var key = new TypeTuple(sourceType, destinationType);
            var settings = GetSettings(key);
            return new TypeAdapterSetter(settings, this);
        }

        public TypeAdapterSetter<TDestination> ForDestinationType<TDestination>()
        {
            var key = new TypeTuple(typeof(void), typeof(TDestination));
            var settings = GetSettings(key);
            return new TypeAdapterSetter<TDestination>(settings, this);
        }

        private TypeAdapterSettings GetSettings(TypeTuple key)
        {
            if (!this.RuleMap.TryGetValue(key, out var rule))
>>>>>>> refs/remotes/MapsterMapper/master
            {
                lock (this.RuleMap)
                {
                    if (!this.RuleMap.TryGetValue(key, out rule))
                    {
                        rule = key.Source == typeof(void)
                            ? CreateDestinationTypeRule(key)
                            : CreateTypeTupleRule(key);
                        this.Rules.Add(rule);
                        this.RuleMap.Add(key, rule);
                    }
                }
            }
            return rule.Settings;
        }

        private TypeAdapterRule CreateTypeTupleRule(TypeTuple key)
        {
            return new TypeAdapterRule
            {
<<<<<<< HEAD
                Priority = (sourceType, destinationType, mapType) =>
                {
                    var score1 = GetSubclassDistance(destinationType, key.Destination, this.AllowImplicitDestinationInheritance);
                    if (score1 == null)
                        return null;
                    var score2 = GetSubclassDistance(sourceType, key.Source, true);
=======
                Priority = arg =>
                {
                    var score1 = GetSubclassDistance(arg.DestinationType, key.Destination, this.AllowImplicitDestinationInheritance);
                    if (score1 == null)
                        return null;
                    var score2 = GetSubclassDistance(arg.SourceType, key.Source, this.AllowImplicitSourceInheritance);
>>>>>>> refs/remotes/MapsterMapper/master
                    if (score2 == null)
                        return null;
                    return score1.Value + score2.Value;
                },
<<<<<<< HEAD
                Settings = new TypeAdapterSettings
                {
                    DestinationType = key.Destination,
                },
=======
                Settings = new TypeAdapterSettings(),
>>>>>>> refs/remotes/MapsterMapper/master
            };
        }

        private static TypeAdapterRule CreateDestinationTypeRule(TypeTuple key)
        {
            return new TypeAdapterRule
            {
<<<<<<< HEAD
                Priority = (sourceType, destinationType, mapType) => GetSubclassDistance(destinationType, key.Destination, true),
                Settings = new TypeAdapterSettings
                {
                    DestinationType = key.Destination,
                },
=======
                Priority = arg => GetSubclassDistance(arg.DestinationType, key.Destination, true),
                Settings = new TypeAdapterSettings(),
>>>>>>> refs/remotes/MapsterMapper/master
            };
        }

        private static int? GetSubclassDistance(Type type1, Type type2, bool allowInheritance)
        {
            if (type1 == type2)
                return 50;

            //generic type definition
            int score = 35;
            if (type2.GetTypeInfo().IsGenericTypeDefinition)
            {
                while (type1 != null && type1.GetTypeInfo().IsGenericType && type1.GetGenericTypeDefinition() != type2)
                {
                    score--;
                    type1 = type1.GetTypeInfo().BaseType;
                }
                return type1 == null ? null : (int?) score;
            }
            if (!allowInheritance)
                return null;

            if (!type2.GetTypeInfo().IsAssignableFrom(type1.GetTypeInfo()))
                return null;

            //interface
            if (type2.GetTypeInfo().IsInterface)
                return 25;

            //base type
            score = 50;
            while (type1 != null && type1 != type2)
            {
                score--;
                type1 = type1.GetTypeInfo().BaseType;
            }
            return score;
        }

<<<<<<< HEAD
        private readonly Hashtable _mapDict = new Hashtable();

        internal Func<TSource, TDestination> GetMapFunction<TSource, TDestination>()
        {
            var key = new TypeTuple(typeof(TSource), typeof(TDestination));
            object del = _mapDict[key] ?? AddToHash(_mapDict, key, CreateMapFunction);

            return (Func<TSource, TDestination>)del;
        }

=======
>>>>>>> refs/remotes/MapsterMapper/master
        private object AddToHash(Hashtable hash, TypeTuple key, Func<TypeTuple, object> func)
        {
            lock (hash)
            {
                var del = hash[key];
                if (del != null)
                    return del;

                del = func(key);
                hash[key] = del;

<<<<<<< HEAD
                var settings = GetSettings(key);
                settings.Compiled = true;
=======
                this.RuleMap.TryGetValue(key, out var rule);
                if (rule != null)
                    rule.Settings.Compiled = true;
>>>>>>> refs/remotes/MapsterMapper/master
                return del;
            }
        }

<<<<<<< HEAD
=======
        private readonly Hashtable _mapDict = new Hashtable();
        public Func<TSource, TDestination> GetMapFunction<TSource, TDestination>()
        {
            return (Func<TSource, TDestination>)GetMapFunction(typeof(TSource), typeof(TDestination));
        }
>>>>>>> refs/remotes/MapsterMapper/master
        internal Delegate GetMapFunction(Type sourceType, Type destinationType)
        {
            var key = new TypeTuple(sourceType, destinationType);
            object del = _mapDict[key] ?? AddToHash(_mapDict, key, tuple => Compiler(CreateMapExpression(tuple, MapType.Map)));

            return (Delegate)del;
        }

        private readonly Hashtable _mapToTargetDict = new Hashtable();
<<<<<<< HEAD

        internal Func<TSource, TDestination, TDestination> GetMapToTargetFunction<TSource, TDestination>()
        {
            var key = new TypeTuple(typeof(TSource), typeof(TDestination));
            object del = _mapToTargetDict[key] ?? AddToHash(_mapToTargetDict, key, CreateMapToTargetFunction);

            return (Func<TSource, TDestination, TDestination>)del;
=======
        public Func<TSource, TDestination, TDestination> GetMapToTargetFunction<TSource, TDestination>()
        {
            return (Func<TSource, TDestination, TDestination>)GetMapToTargetFunction(typeof(TSource), typeof(TDestination));
>>>>>>> refs/remotes/MapsterMapper/master
        }

        internal Delegate GetMapToTargetFunction(Type sourceType, Type destinationType)
        {
            var key = new TypeTuple(sourceType, destinationType);
            object del = _mapToTargetDict[key] ?? AddToHash(_mapToTargetDict, key, tuple => Compiler(CreateMapExpression(tuple, MapType.MapToTarget)));

            return (Delegate)del;
        }

        private readonly Hashtable _projectionDict = new Hashtable();

        internal Expression<Func<TSource, TDestination>> GetProjectionExpression<TSource, TDestination>()
        {
            var del = GetProjectionCallExpression(typeof(TSource), typeof(TDestination));

<<<<<<< HEAD
            return (Expression<Func<TSource, TDestination>>)((UnaryExpression)((MethodCallExpression)del).Arguments[1]).Operand;
=======
            return (Expression<Func<TSource, TDestination>>)((UnaryExpression)del.Arguments[1]).Operand;
>>>>>>> refs/remotes/MapsterMapper/master
        }

        internal MethodCallExpression GetProjectionCallExpression(Type sourceType, Type destinationType)
        {
            var key = new TypeTuple(sourceType, destinationType);
            object del = _projectionDict[key] ?? AddToHash(_projectionDict, key, CreateProjectionCallExpression);

            return (MethodCallExpression)del;
        }

        private Hashtable _dynamicMapDict;
        internal Func<object, TDestination> GetDynamicMapFunction<TDestination>(Type sourceType)
        {
<<<<<<< HEAD
            var context = new CompileContext(this);
            context.Running.Add(tuple);
            try
            {
                var result = CreateMapExpression(tuple.Source, tuple.Destination, MapType.Map, context);
                var compiled = result.Compile();
                if (this == GlobalSettings)
                {
                    var field = typeof(TypeAdapter<,>).MakeGenericType(tuple.Source, tuple.Destination).GetField("Map");
                    field.SetValue(null, compiled);
                }
                return compiled;
            }
            finally
            {
                context.Running.Remove(tuple);
            }
=======
            if (_dynamicMapDict == null)
                _dynamicMapDict = new Hashtable();
            var key = new TypeTuple(sourceType, typeof(TDestination));
            object del = _dynamicMapDict[key] ?? AddToHash(_dynamicMapDict, key, tuple => Compiler(CreateDynamicMapExpression(tuple)));

            return (Func<object, TDestination>)del;
>>>>>>> refs/remotes/MapsterMapper/master
        }

        internal LambdaExpression CreateMapExpression(TypeTuple tuple, MapType mapType)
        {
            var context = new CompileContext(this);
            context.Running.Add(tuple);
            try
            {
                var arg = GetCompileArgument(tuple, mapType, context);
                return CreateMapExpression(arg);
            }
            finally
            {
                context.Running.Remove(tuple);
            }
        }

        private MethodCallExpression CreateProjectionCallExpression(TypeTuple tuple)
        {
<<<<<<< HEAD
            var context = new CompileContext(this);
            context.Running.Add(tuple);
            try
            {
                var lambda = CreateMapExpression(tuple.Source, tuple.Destination, MapType.Projection, context);
                var source = Expression.Parameter(typeof(IQueryable<>).MakeGenericType(tuple.Source));
                var methodInfo = (from method in typeof(Queryable).GetMethods()
                                  where method.Name == "Select"
                                  let p = method.GetParameters()[1]
                                  where p.ParameterType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(Func<,>)
                                  select method).First().MakeGenericMethod(tuple.Source, tuple.Destination);
                return Expression.Call(methodInfo, source, Expression.Quote(lambda));
            }
            finally
            {
                context.Running.Remove(tuple);
            }
=======
            var lambda = CreateMapExpression(tuple, MapType.Projection);
            var source = Expression.Parameter(typeof(IQueryable<>).MakeGenericType(tuple.Source));
            var methodInfo = (from method in typeof(Queryable).GetMethods()
                                where method.Name == nameof(Queryable.Select)
                                let p = method.GetParameters()[1]
                                where p.ParameterType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(Func<,>)
                                select method).First().MakeGenericMethod(tuple.Source, tuple.Destination);
            return Expression.Call(methodInfo, source, Expression.Quote(lambda));
>>>>>>> refs/remotes/MapsterMapper/master
        }

        private static LambdaExpression CreateMapExpression(CompileArgument arg, bool allowNull = false)
        {
            var fn = arg.MapType == MapType.MapToTarget
                ? arg.Settings.ConverterToTargetFactory
                : arg.Settings.ConverterFactory;
            if (fn == null)
            {
                if (allowNull)
                    return null;
                else
                    throw new CompileException(arg, 
                        new InvalidOperationException("ConverterFactory is not found"));
            }
            try
            {
                return fn(arg);
            }
            catch (Exception ex)
            {
                throw new CompileException(arg, ex);
            }
        }

        private LambdaExpression CreateDynamicMapExpression(TypeTuple tuple)
        {
            var lambda = CreateMapExpression(tuple, MapType.Map);
            var pNew = Expression.Parameter(typeof(object));
            var pOld = lambda.Parameters[0];
            var assign = ExpressionEx.Assign(pOld, pNew);
            return Expression.Lambda(
                Expression.Block(new[] { pOld }, assign, lambda.Body),
                pNew);
        }

        internal LambdaExpression CreateInlineMapExpression(Type sourceType, Type destinationType, MapType parentMapType, CompileContext context)
        {
            var tuple = new TypeTuple(sourceType, destinationType);
            if (context.Running.Contains(tuple))
            {
                if (parentMapType == MapType.Projection)
                    throw new InvalidOperationException("Projection does not support circular reference");
                return CreateMapInvokeExpression(sourceType, destinationType);
            }

            context.Running.Add(tuple);
            try
            {
                var arg = GetCompileArgument(tuple, parentMapType, context);
                var exp = CreateMapExpression(arg, true);
                if (exp != null)
                {
                    var detector = new BlockExpressionDetector();
                    detector.Visit(exp);
                    if (detector.IsBlockExpression)
                        exp = null;
                }
                if (exp != null)
                    return exp;
                if (parentMapType == MapType.MapToTarget)
                    return CreateMapToTargetInvokeExpression(sourceType, destinationType);
                else
                    return CreateMapInvokeExpression(sourceType, destinationType);
            }
            finally
            {
                context.Running.Remove(tuple);
            }
        }

        private LambdaExpression CreateMapInvokeExpression(Type sourceType, Type destinationType)
        {
            Expression invoker;
            if (this == GlobalSettings)
            {
                var field = typeof(TypeAdapter<,>).MakeGenericType(sourceType, destinationType).GetField("Map");
                invoker = Expression.Field(null, field);
            }
            else
            {
<<<<<<< HEAD
                var method = (from m in typeof(TypeAdapterConfig).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                              where m.Name == "GetMapFunction"
=======
                var method = (from m in typeof(TypeAdapterConfig).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                              where m.Name == nameof(TypeAdapterConfig.GetMapFunction)
>>>>>>> refs/remotes/MapsterMapper/master
                              select m).First().MakeGenericMethod(sourceType, destinationType);
                invoker = Expression.Call(Expression.Constant(this), method);
            }
            var p = Expression.Parameter(sourceType);
            var invoke = Expression.Call(invoker, "Invoke", null, p);
            return Expression.Lambda(invoke, p);
        }

        private LambdaExpression CreateMapToTargetInvokeExpression(Type sourceType, Type destinationType)
        {
            var method = (from m in typeof(TypeAdapterConfig).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                          where m.Name == nameof(TypeAdapterConfig.GetMapToTargetFunction)
                          select m).First().MakeGenericMethod(sourceType, destinationType);
            var invoker = Expression.Call(Expression.Constant(this), method);
            var p1 = Expression.Parameter(sourceType);
            var p2 = Expression.Parameter(destinationType);
            var invoke = Expression.Call(invoker, "Invoke", null, p1, p2);
            return Expression.Lambda(invoke, p1, p2);
        }

        internal TypeAdapterSettings GetMergedSettings(TypeTuple tuple, MapType mapType)
        {
            var arg = new PreCompileArgument
            {
                SourceType = tuple.Source,
                DestinationType = tuple.Destination,
                MapType = mapType,
                ExplicitMapping = this.RuleMap.ContainsKey(tuple),
            };
            var settings = (from rule in this.Rules.Reverse<TypeAdapterRule>()
                            let priority = rule.Priority(arg)
                            where priority != null
                            orderby priority.Value descending
                            select rule.Settings).ToList();
            var result = new TypeAdapterSettings();
            foreach (var setting in settings)
            {
                result.Apply(setting);
            }

            //remove recursive include types
            if (mapType == MapType.MapToTarget)
                result.Includes.Remove(tuple);
            else
                result.Includes.RemoveAll(t => t.Source == tuple.Source);
            return result;
        }

        CompileArgument GetCompileArgument(TypeTuple tuple, MapType mapType, CompileContext context)
        {
            var setting = GetMergedSettings(tuple, mapType);
            var arg = new CompileArgument
            {
                SourceType = tuple.Source,
                DestinationType = tuple.Destination,
                ExplicitMapping = this.RuleMap.ContainsKey(tuple),
                MapType = mapType,
                Context = context,
                Settings = setting,
            };
            return arg;
        }

        public void Compile()
        {
            var keys = RuleMap.Keys.ToList();
            foreach (var key in keys)
            {
<<<<<<< HEAD
                _mapDict[key] = CreateMapFunction(key);
                _mapToTargetDict[key] = CreateMapToTargetFunction(key);
=======
                _mapDict[key] = Compiler(CreateMapExpression(key, MapType.Map));
                _mapToTargetDict[key] = Compiler(CreateMapExpression(key, MapType.MapToTarget));
>>>>>>> refs/remotes/MapsterMapper/master
            }
        }

        public void Compile(Type sourceType, Type destinationType)
        {
            var tuple = new TypeTuple(sourceType, destinationType);
<<<<<<< HEAD
            _mapDict[tuple] = CreateMapFunction(tuple);
            _mapToTargetDict[tuple] = CreateMapToTargetFunction(tuple);
=======
            _mapDict[tuple] = Compiler(CreateMapExpression(tuple, MapType.Map));
            _mapToTargetDict[tuple] = Compiler(CreateMapExpression(tuple, MapType.MapToTarget));
            if (this == GlobalSettings)
            {
                var field = typeof(TypeAdapter<,>).MakeGenericType(sourceType, destinationType).GetField("Map");
                field.SetValue(null, _mapDict[tuple]);
            }
>>>>>>> refs/remotes/MapsterMapper/master
        }

        public void CompileProjection()
        {
            var keys = RuleMap.Keys.ToList();
            foreach (var key in keys)
            {
                _projectionDict[key] = CreateProjectionCallExpression(key);
            }
        }

        public void CompileProjection(Type sourceType, Type destinationType)
        {
            var tuple = new TypeTuple(sourceType, destinationType);
            _projectionDict[tuple] = CreateProjectionCallExpression(tuple);
        }

        public IList<IRegister> Scan(params Assembly[] assemblies)
        {
            List<IRegister> registers = assemblies.Select(assembly => assembly.GetTypes()
                .Where(x => typeof(IRegister).GetTypeInfo().IsAssignableFrom(x.GetTypeInfo()) && x.GetTypeInfo().IsClass && !x.GetTypeInfo().IsAbstract))
                .SelectMany(registerTypes =>
                    registerTypes.Select(registerType => (IRegister)Activator.CreateInstance(registerType))).ToList();

            this.Apply(registers);
            return registers;
        }

        public void Apply(IEnumerable<Lazy<IRegister>> registers)
        {
            this.Apply(registers.Select(register => register.Value));
        }

        public void Apply(IEnumerable<IRegister> registers)
        {
            foreach (IRegister register in registers)
            {
                register.Register(this);
            }
        }

        public void Apply(params IRegister[] registers)
        {
            foreach (IRegister register in registers)
            {
                register.Register(this);
            }
        }

        internal void Clear()
        {
            var keys = RuleMap.Keys.ToList();
            foreach (var key in keys)
            {
                Remove(key);
            }
        }

        internal void Remove(Type sourceType, Type destinationType)
        {
            var key = new TypeTuple(sourceType, destinationType);
            Remove(key);
        }

        private void Remove(TypeTuple key)
        {
<<<<<<< HEAD
            TypeAdapterRule rule;
            if (this.RuleMap.TryGetValue(key, out rule))
=======
            if (this.RuleMap.TryGetValue(key, out var rule))
>>>>>>> refs/remotes/MapsterMapper/master
            {
                this.RuleMap.Remove(key);
                this.Rules.Remove(rule);
            }
            _mapDict.Remove(key);
            _mapToTargetDict.Remove(key);
            _projectionDict.Remove(key);
<<<<<<< HEAD
        }

        private static TypeAdapterConfig _cloneConfig;

=======
            _dynamicMapDict?.Remove(key);
        }

        private static TypeAdapterConfig _cloneConfig;
>>>>>>> refs/remotes/MapsterMapper/master
        public TypeAdapterConfig Clone()
        {
            if (_cloneConfig == null)
            {
                _cloneConfig = new TypeAdapterConfig();
                _cloneConfig.Default.Settings.PreserveReference = true;
<<<<<<< HEAD
=======
                _cloneConfig.ForType<TypeAdapterSettings, TypeAdapterSettings>()
                    .MapWith(src => src.Clone(), true);
>>>>>>> refs/remotes/MapsterMapper/master
            }
            var fn = _cloneConfig.GetMapFunction<TypeAdapterConfig, TypeAdapterConfig>();
            return fn(this);
        }
<<<<<<< HEAD
=======

        private Hashtable _inlineConfigs;
        public TypeAdapterConfig Fork(Action<TypeAdapterConfig> action,
#if !NET40
            [CallerFilePath]
#endif
            string key1 = null,
#if !NET40
            [CallerLineNumber]
#endif
            int key2 = 0)
        {
            if (_inlineConfigs == null)
                _inlineConfigs = new Hashtable();
            var key = key1 + '|' + key2;
            var config = _inlineConfigs[key];
            if (config != null)
                return (TypeAdapterConfig)config;

            lock(_inlineConfigs)
            {
                config = _inlineConfigs[key];
                if (config != null)
                    return (TypeAdapterConfig)config;

                var cloned = this.Clone();
                action(cloned);
                _inlineConfigs[key] = config = cloned;
            }
            return (TypeAdapterConfig)config;
        }
>>>>>>> refs/remotes/MapsterMapper/master
    }

    public static class TypeAdapterConfig<TSource, TDestination>
    {
        public static TypeAdapterSetter<TSource, TDestination> NewConfig()
        {
            return TypeAdapterConfig.GlobalSettings.NewConfig<TSource, TDestination>();
        }

        public static TypeAdapterSetter<TSource, TDestination> ForType()
        {
            return TypeAdapterConfig.GlobalSettings.ForType<TSource, TDestination>();
        }

        public static void Clear()
        {
            TypeAdapterConfig.GlobalSettings.Remove(typeof(TSource), typeof(TDestination));
        }
    }
}