using System;
using System.Reflection;

namespace Mapster
{
<<<<<<< HEAD
    public interface IAdapter
    {
        TypeAdapterBuiler<TSource> BuildAdapter<TSource>(TSource source);
        TDestination Adapt<TDestination>(object source);
        TDestination Adapt<TSource, TDestination>(TSource source);
        TDestination Adapt<TSource, TDestination>(TSource source, TDestination destination);
        object Adapt(object source, Type sourceType, Type destinationType);
        object Adapt(object source, object destination, Type sourceType, Type destinationType);
    }

=======
>>>>>>> refs/remotes/MapsterMapper/master
    public class Adapter : IAdapter
    {
        readonly TypeAdapterConfig _config;

        public Adapter() : this(TypeAdapterConfig.GlobalSettings) { }

        public Adapter(TypeAdapterConfig config)
        {
            _config = config;
        }

<<<<<<< HEAD
        public TypeAdapterBuiler<TSource> BuildAdapter<TSource>(TSource source)
        {
            return new TypeAdapterBuiler<TSource>(source, _config);
=======
        public TypeAdapterBuilder<TSource> BuildAdapter<TSource>(TSource source)
        {
            return new TypeAdapterBuilder<TSource>(source, _config);
>>>>>>> refs/remotes/MapsterMapper/master
        }

        public TDestination Adapt<TDestination>(object source)
        {
            if (source == null)
                return default(TDestination);
            var type = source.GetType();
            var fn = _config.GetDynamicMapFunction<TDestination>(type);
            return fn(source);
        }

        public TDestination Adapt<TSource, TDestination>(TSource source)
        {
            var fn = _config.GetMapFunction<TSource, TDestination>();
            return fn(source);
        }

        public TDestination Adapt<TSource, TDestination>(TSource source, TDestination destination)
        {
            var fn = _config.GetMapToTargetFunction<TSource, TDestination>();
            return fn(source, destination);
        }

        public object Adapt(object source, Type sourceType, Type destinationType)
        {
            var del = _config.GetMapFunction(sourceType, destinationType);
            if (sourceType.GetTypeInfo().IsVisible && destinationType.GetTypeInfo().IsVisible)
            {
                dynamic fn = del;
                return fn((dynamic)source);
            }
            else
            {
                //NOTE: if type is non-public, we cannot use dynamic
                //DynamicInvoke is slow, but works with non-public
                return del.DynamicInvoke(source);
            }
        }

        public object Adapt(object source, object destination, Type sourceType, Type destinationType)
        {
            var del = _config.GetMapToTargetFunction(sourceType, destinationType);
            if (sourceType.GetTypeInfo().IsVisible && destinationType.GetTypeInfo().IsVisible)
            {
                dynamic fn = del;
                return fn((dynamic)source, (dynamic)destination);
            }
            else
            {
                //NOTE: if type is non-public, we cannot use dynamic
                //DynamicInvoke is slow, but works with non-public
                return del.DynamicInvoke(source, destination);
            }
        }
    }

}