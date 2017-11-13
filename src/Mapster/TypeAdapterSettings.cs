using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Mapster.Models;

namespace Mapster
{
    public class TypeAdapterSettings: SettingStore
    {
<<<<<<< HEAD
        Map,
        InlineMap,
        MapToTarget,
        Projection,
    }

    public class TypeAdapterSettings
    {
        public Dictionary<string, LambdaExpression> IgnoreMembers { get; internal set; } = new Dictionary<string, LambdaExpression>();
        public HashSet<Type> IgnoreAttributes { get; internal set; } = new HashSet<Type>();
        public TransformsCollection DestinationTransforms { get; internal set; } = new TransformsCollection();
        public NameMatchingStrategy NameMatchingStrategy { get; internal set; } = new NameMatchingStrategy();

        public bool? PreserveReference { get; set; }
        public bool? ShallowCopyForSameType { get; set; }
        public bool? IgnoreNullValues { get; set; }
        public bool? NoInherit { get; set; }
        public Type DestinationType { get; set; }

        public List<Func<Expression, IMemberModel, CompileArgument, Expression>> ValueAccessingStrategies { get; internal set; } = new List<Func<Expression, IMemberModel, CompileArgument, Expression>>();
        public List<InvokerModel> Resolvers { get; internal set; } = new List<InvokerModel>();
        public Func<CompileArgument, LambdaExpression> ConstructUsingFactory { get; set; }
        public Func<CompileArgument, LambdaExpression> ConverterFactory { get; set; }
        public Func<CompileArgument, LambdaExpression> ConverterToTargetFactory { get; set; }
        public List<Func<CompileArgument, LambdaExpression>> AfterMappingFactories { get; internal set; } = new List<Func<CompileArgument, LambdaExpression>>();

        internal bool Compiled { get; set; }

        public void Apply(TypeAdapterSettings other)
        {
            if (this.NoInherit == null)
                this.NoInherit = other.NoInherit;

            if (this.NoInherit == true)
            {
                if (this.DestinationType != null && other.DestinationType != null)
                    return;
            }

            if (this.PreserveReference == null)
                this.PreserveReference = other.PreserveReference;
            if (this.ShallowCopyForSameType == null)
                this.ShallowCopyForSameType = other.ShallowCopyForSameType;
            if (this.IgnoreNullValues == null)
                this.IgnoreNullValues = other.IgnoreNullValues;

            foreach (var member in other.IgnoreMembers)
            {
                this.IgnoreMembers[member.Key] = member.Value;
            }
            this.IgnoreAttributes.UnionWith(other.IgnoreAttributes);
            this.NameMatchingStrategy.Apply(other.NameMatchingStrategy);
            this.DestinationTransforms.TryAdd(other.DestinationTransforms.Transforms);
            this.AfterMappingFactories.AddRange(other.AfterMappingFactories);

            this.ValueAccessingStrategies.AddRange(other.ValueAccessingStrategies);
            this.Resolvers.AddRange(other.Resolvers);

            if (this.ConstructUsingFactory == null)
                this.ConstructUsingFactory = other.ConstructUsingFactory;
            if (this.ConverterFactory == null)
                this.ConverterFactory = other.ConverterFactory;
            if (this.ConverterToTargetFactory == null)
                this.ConverterToTargetFactory = other.ConverterToTargetFactory;
=======
        public IgnoreIfDictionary IgnoreIfs
        {
            get => Get("IgnoreIfs", () => new IgnoreIfDictionary());
        }
        public TransformsCollection DestinationTransforms
        {
            get => Get("DestinationTransforms", () => new TransformsCollection());
        }
        public NameMatchingStrategy NameMatchingStrategy
        {
            get => Get("NameMatchingStrategy", () => new NameMatchingStrategy());
            set => Set("NameMatchingStrategy", value);
        }

        public bool? PreserveReference
        {
            get => Get("PreserveReference");
            set => Set("PreserveReference", value);
        }
        public bool? ShallowCopyForSameType
        {
            get => Get("ShallowCopyForSameType");
            set => Set("ShallowCopyForSameType", value);
        }
        public bool? IgnoreNullValues
        {
            get => Get("IgnoreNullValues");
            set => Set("IgnoreNullValues", value);
        }
        public bool? MapEnumByName
        {
            get => Get("MapEnumByName");
            set => Set("MapEnumByName", value);
        }
        public bool? IgnoreNonMapped
        {
            get => Get("IgnoreNonMapped");
            set => Set("IgnoreNonMapped", value);
        }
        public bool? AvoidInlineMapping
        {
            get => Get("AvoidInlineMapping");
            set => Set("AvoidInlineMapping", value);
        }

        public List<Func<IMemberModel, MemberSide, bool?>> ShouldMapMember
        {
            get => Get("ShouldMapMember", () => new List<Func<IMemberModel, MemberSide, bool?>>());
        }
        public List<Func<Expression, IMemberModel, CompileArgument, Expression>> ValueAccessingStrategies
        {
            get => Get("ValueAccessingStrategies", () => new List<Func<Expression, IMemberModel, CompileArgument, Expression>>());
        }
        public List<InvokerModel> Resolvers
        {
            get => Get("Resolvers", () => new List<InvokerModel>());
        }
        public List<Func<CompileArgument, LambdaExpression>> AfterMappingFactories
        {
            get => Get("AfterMappingFactories", () => new List<Func<CompileArgument, LambdaExpression>>());
        }
        public List<TypeTuple> Includes
        {
            get => Get("Includes", () => new List<TypeTuple>());
        }
        public List<Func<IMemberModel, string>> GetMemberNames
        {
            get => Get("GetMemberNames", () => new List<Func<IMemberModel, string>>());
        }
        public Func<CompileArgument, LambdaExpression> ConstructUsingFactory
        {
            get => Get<Func<CompileArgument, LambdaExpression>>("ConstructUsingFactory");
            set => Set("ConstructUsingFactory", value);
        }
        public Func<CompileArgument, LambdaExpression> ConverterFactory
        {
            get => Get<Func<CompileArgument, LambdaExpression>>("ConverterFactory");
            set => Set("ConverterFactory", value);
        }
        public Func<CompileArgument, LambdaExpression> ConverterToTargetFactory
        {
            get => Get<Func<CompileArgument, LambdaExpression>>("ConverterToTargetFactory");
            set => Set("ConverterToTargetFactory", value);
>>>>>>> refs/remotes/MapsterMapper/master
        }

        internal bool Compiled { get; set; }

        public TypeAdapterSettings Clone()
        {
            var settings = new TypeAdapterSettings();
            settings.Apply(this);
            return settings;
        }
    }
}