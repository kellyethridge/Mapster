![Mapster Icon](https://cloud.githubusercontent.com/assets/5763993/26522718/d16f3e42-4330-11e7-9b78-f8c7402624e7.png)

## Mapster - The Mapper of Your Domain
Writing mapping method is machine job. Do not waste your time, let Mapster do it.

<<<<<<< HEAD
[![Build status](https://ci.appveyor.com/api/projects/status/krpp0nhspmklom1d?svg=true)](https://ci.appveyor.com/project/eswann/mapster)

### Basic usage
```
var result = original.Adapt<NewType>();
```
=======
>>>>>>> refs/remotes/MapsterMapper/master
### Get it
```
PM> Install-Package Mapster
```
<<<<<<< HEAD
###Get started

[Mapping](#Mapping)
- [Mapping to a new object](#MappingNew)
- [Mapping to an existing object](#MappingToTarget)
- [Queryable Extensions](#Projection)
- [Mapper instance](#UnitTest)

[Conversion](#Conversion)
- [Conversion of immutable types](#ConversionImmutable)
- [Conversion from/to enum](#ConversionEnum)
- [Mapping POCO](#ConversionPOCO)
- [Mapping Lists](#ConversionList)
- [Conversion from/to Dictionary](#ConversionDictionary)
- [Conversion from/to Record Types](#ConversionRecordType)

[Settings](#Settings)
- [Settings per type](#SettingsPerType)
- [Global Settings](#SettingsGlobal)
- [Settings inheritance](#SettingsInheritance)
- [Rule based settings](#SettingsRuleBased)
- [Setting instance](#SettingsOverload)
- [Assembly scanning](#AssemblyScanning)

[Basic Customization](#Basic)
- [Ignore properties & attributes](#Ignore)
- [Custom Property Mapping](#Map)
- [Flexible Name Mapping](#NameMatchingStrategy)
- [Merge Objects](#Merge)
- [Shallow Copy](#ShallowCopy)
- [Preserve reference (preventing circular reference stackoverflow)](#PreserveReference)

[Advance Customization](#Advance)
- [Custom instance creation](#ConstructUsing)
- [After mapping action](#AfterMapping)
- [Passing runtime value](#RuntimeValue)
- [Type-Specific Destination Transforms](#Transform)
- [Custom Type Resolvers](#ConverterFactory)
=======
>>>>>>> refs/remotes/MapsterMapper/master

### Basic usage
#### Mapping to a new object
Mapster creates the destination object and maps values to it.

    var destObject = sourceObject.Adapt<TDestination>();

<<<<<<< HEAD
or using extension methods

    var destObject = sourceObject.Adapt<TDestination>();

#####Mapping to an existing object <a name="MappingToTarget"></a>
=======
#### Mapping to an existing object
>>>>>>> refs/remotes/MapsterMapper/master
You make the object, Mapster maps to the object.

    TDestination destObject = new TDestination();
    destObject = sourceObject.Adapt(destObject);

#### Queryable Extensions
Mapster also provides extensions to map queryables.

    using (MyDbContext context = new MyDbContext())
    {
        // Build a Select Expression from DTO
        var destinations = context.Sources.ProjectToType<Destination>().ToList();

        // Versus creating by hand:
        var destinations = context.Sources.Select(c => new Destination(){
            Id = p.Id,
            Name = p.Name,
            Surname = p.Surname,
            ....
        })
        .ToList();
    }

### Performance
Don't let other libraries slow you down, Mapster is at least twice faster!

![image](https://cloud.githubusercontent.com/assets/5763993/26527206/bbde5490-43b8-11e7-8363-34644e5e709e.png)

### What's new in 3.0

<<<<<<< HEAD
#####Conversion from/to Dictionary <a name="ConversionDictionary"></a>
Mapster supports conversion from object to dictionary and dictionary to object.

```
var point = new { X = 2, Y = 3 };
var dict = src.Adapt<Dictionary<string, int>>();
dict["Y"].ShouldBe(3);
```

#####Conversion from/to Record Types <a name="ConversionRecordType"></a>
Record types are types with no setter, all parameters will be initiated from constructor. 
	
```
class Person {
    public string Name { get; }
    public int Age { get; }

    public Person(string name, int age) {
        this.Name = name;
        this.Age = age;
    }
}

var src = new { Name = "Mapster", Age = 3 };
var target = src.Adapt<Person>();
``` 

There is limitation on record type mapping. Record type must not have setter and have only one non-empty constructor. And all parameter names must match with properties.

####Settings <a name="Settings"></a>
#####Settings per type <a name="SettingsPerType"></a>
You can easily create settings for a type mapping by using: `TypeAdapterConfig<TSource, TDestination>.NewConfig()`.
When `NewConfig` is called, any previous configuration for this particular TSource => TDestination mapping is dropped.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .Ignore(dest => dest.Age)
        .Map(dest => dest.FullName,
             src => string.Format("{0} {1}", src.FirstName, src.LastName));

As an alternative to `NewConfig`, you can use `ForType` in the same way:

	TypeAdapterConfig<TSource, TDestination>
			.ForType()
			.Ignore(dest => dest.Age)
			.Map(dest => dest.FullName,
				 src => string.Format("{0} {1}", src.FirstName, src.LastName));

`ForType` differs in that it will create a new mapping if one doesn't exist, but if the specified TSource => TDestination
mapping does already exist, it will enhance the existing mapping instead of dropping and replacing it.  

#####Global Settings <a name="SettingsGlobal"></a>
Use global settings to apply policies to all mappings.

    TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

Then for individual type mappings, you can easily override the global setting(s).

    TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig().PreserveReference(false);

#####Settings inheritance <a name="SettingsInheritance"></a>
Type mappings will automatically inherit for source types. Ie. if you set up following config.

    TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
        .Map(dest => dest.Name, src => src.Name + "_Suffix");

A derived type of `SimplePoco` will automatically apply the base mapping config.

    var dest = TypeAdapter.Adapt<DerivedPoco, SimpleDto>(src); //dest.Name = src.Name + "_Suffix"

If you don't wish a derived type to use the base mapping, just define `NoInherit` for that type.

    TypeAdapterConfig<DerivedPoco, SimpleDto>.NewConfig().NoInherit(true);

    //or at the global level
    TypeAdapterConfig.GlobalSettings.Default.NoInherit(true);

And by default, Mapster will not inherit destination type mappings. You can turn on by `AllowImplicitDestinationInheritance`.

    TypeAdapterConfig.GlobalSettings.AllowImplicitDestinationInheritance = true;

Finally, Mapster also provides methods to inherit explicitly.

    TypeAdapterConfig<DerivedPoco, DerivedDto>.NewConfig()
        .Inherits<SimplePoco, SimpleDto>();

#####Rule based settings <a name="SettingsRuleBased"></a>
To set the setting at a more granular level. You can use the `When` method in global settings.
In the example below, when any source type and destination type are the same, we will not the copy the `Id` property.

    TypeAdapterConfig.GlobalSettings.When((srcType, destType, mapType) => srcType == destType)
        .Ignore("Id");

In this example, the config would only apply to Query Expressions (projections).

    TypeAdapterConfig.GlobalSettings.When((srcType, destType, mapType) => mapType == MapType.Projection)
        .IgnoreAttribute(typeof(NotMapAttribute));

#####Setting instance <a name="SettingsOverload"></a>
You may wish to have different settings in different scenarios.
If you would not like to apply setting at a static level, Mapster also provides setting instance configurations.

    var config = new TypeAdapterConfig();
    config.Default.Ignore("Id");

For instance configurations, you can use the same `NewConfig` and `ForType` methods that are used at the global level with
the same behavior: `NewConfig` drops any existing configuration and `ForType` creates or enhances a configuration.

    config.NewConfig<TSource, TDestination>()
          .Map(dest => dest.FullName,
               src => string.Format("{0} {1}", src.FirstName, src.LastName));

    config.ForType<TSource, TDestination>()
          .Map(dest => dest.FullName,
               src => string.Format("{0} {1}", src.FirstName, src.LastName));

You can apply a specific config instance by passing it to the `Adapt` method. (NOTE: please reuse your config instance to prevent recompilation)

    var result = TypeAdapter.Adapt<TDestination>(src, config);

Or to an Adapter instance.

    var adapter = new Adapter(config);
    var result = adapter.Adapt<TDestination>(src);

If you would like to create configuration instance from existing configuration, you can use `Clone` method. For example, if you would like to clone from global setting.

    var newConfig = TypeAdapterConfig.GlobalSettings.Clone();
    
Or clone from existing configuration instance

    var newConfig = oldConfig.Clone();

#####Assembly scanning <a name="AssemblyScanning"></a>
It's relatively common to have mapping configurations spread across a number of different assemblies.  
Perhaps your domain assembly has some rules to map to domain objects and your web api has some specific rules to map to your
api contracts. In these cases, it can be helpful to allow assemblies to be scanned for these rules so you have some basic
method of organizing your rules and not forgetting to have the registration code called. In some cases, it may even be necessary to
register the assemblies in a particular order, so that some rules override others. Assembly scanning helps with this.
Assembly scanning is simple, just create any number of IRegister implementations in your assembly, then call `Scan` from your TypeAdapterConfig class:

	public class MyRegister : IRegister
	{
		public void Register(TypeAdapterConfig config){
			config.NewConfig<TSource, TDestination>();

			//OR to create or enhance an existing configuration

			config.ForType<TSource, TDestination>();
		}
	}

To scan and register at the Global level:

	TypeAdapterConfig.GlobalSettings.Scan(assembly1, assembly2, assemblyN)

For a specific config instance:

	var config = new TypeAdapterConfig();
	config.Scan(assembly1, assembly2, assemblyN);

If you use other assembly scanning library such as MEF, you can easily apply registration with `Apply` method.

	var registers = container.GetExports<IRegister>();
	config.Apply(registers);

####Basic Customization <a name="Basic"></a>
When the default convention mappings aren't enough to do the job, you can specify complex source mappings.

#####Ignore Members & Attributes <a name="Ignore"></a>
Mapster will automatically map properties with the same names. You can ignore members by using the `Ignore` method.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .Ignore(dest => dest.Id);

You can ignore members conditionally, with condition based on source or target. When the condition is met, mapping of the property
will be skipped altogether. This is the difference from custom `Map` with condition, where destination is set to `null`
when condition is met.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .IgnoreIf((src, dest) => !string.IsNullOrEmpty(dest.Name), dest => dest.Name);

You can ignore members annotated with specific attributes by using the `IgnoreAttribute` method.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .IgnoreAttribute(typeof(JsonIgnoreAttribute));

#####Custom property mapping <a name="Map"></a>
You can customize how Mapster maps values to a property.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .Map(dest => dest.FullName,
             src => string.Format("{0} {1}", src.FirstName, src.LastName));

The Map configuration can accept a third parameter that provides a condition based on the source.
If the condition is not met, Mapster will retry with next conditions. Default condition should be added at the end without specifying condition. If you do not specify default condition, null or default value will be assigned.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .Map(dest => dest.FullName, src => "Sig. " + src.FullName, srcCond => srcCond.Country == "Italy")
        .Map(dest => dest.FullName, src => "Sr. " + src.FullName, srcCond => srcCond.Country == "Spain")
        .Map(dest => dest.FullName, src => "Mr. " + src.FullName);

In Mapster 2.0, you can even map when source and destination property types are different.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .Map(dest => dest.Gender,      //Genders.Male or Genders.Female
             src => src.GenderString); //"Male" or "Female"

#####Flexible name mapping <a name="NameMatchingStrategy"></a>
By default, Mapster will map property with case sensitive name. You can adjust to flexible name mapping by setting `NameMatchingStrategy.Flexible` to `NameMatchingStrategy` method. This setting will allow matching between `PascalCase`, `camelCase`, `lower_case`, and `UPPER_CASE`. 

This setting will apply flexible naming globally.

```
TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
```

or by specific type mapping.

```
TypeAdapterConfig<Foo, Bar>.NewConfig().NameMatchingStrategy(NameMatchingStrategy.Flexible);
```

#####Merge object <a name="Merge"></a>
By default, Mapster will map all properties, even source properties containing null values.
You can copy only properties that have values by using `IgnoreNullValues` method.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .IgnoreNullValues(true);

#####Shallow copy <a name="ShallowCopy"></a>
By default, Mapster will recursively map nested objects. You can do shallow copying by setting `ShallowCopyForSameType` to `true`.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .ShallowCopyForSameType(true);

#####Preserve reference (preventing circular reference stackoverflow) <a name="PreserveReference"></a>
When mapping objects with circular references, a stackoverflow exception will result.
This is because Mapster will get stuck in a loop tring to recursively map the circular reference.
If you would like to map circular references or preserve references (such as 2 properties pointing to the same object), you can do it by setting `PreserveReference` to `true`

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .PreserveReference(true);

NOTE: Projection doesn't support circular reference. To overcome, you might use `Adapt` instead of `ProjectToType`.

    TypeAdaptConfig.GlobalSettings.Default.PreserveReference(true);
    var students = context.Student.Include(p => p.Schools).Adapt<List<StudentDTO>>();

####Advance Customization <a name="Advance"></a>
#####Custom Destination Object Creation <a name="ConstructUsing"></a>
You can provide a function call to create your destination objects instead of using the default object creation
(which expects an empty constructor). To do so, use the `ConstructUsing` method when configuring.  This method expects
a function that will provide the destination instance. You can call your own constructor, a factory method,
or anything else that provides an object of the expected type.

    //Example using a non-default constructor
    TypeAdapterConfig<TSource, TDestination>.NewConfig()
                .ConstructUsing(src => new TDestination(src.Id, src.Name));

    //Example using an object initializer
    TypeAdapterConfig<TSource, TDestination>.NewConfig()
                .ConstructUsing(src => new TDestination{Unmapped = "unmapped"});

#####After mapping action <a name="AfterMapping"></a>
You can perform actions after each mapping by using `AfterMapping` method. For instance, you might would like to validate object after each mapping.

```
TypeAdapterConfig<Foo, Bar>.ForType().AfterMapping((src, dest) => dest.Validate());
```

Or you can set for all mappings to types which implemented a specific interface by using `ForDestinationType` method.

```
TypeAdapterConfig.GlobalSettings.ForDestinationType<IValidatable>()
                                .AfterMapping(dest => dest.Validate());
```

#####Passing runtime value <a name="RuntimeValue"></a>
In some cases, you might would like to pass runtime values (ie, current user). On configuration, we can receive run-time value by `MapContext.Current.Parameters`.

```
TypeAdapterConfig<Poco, Dto>.NewConfig()
                            .Map(dest => dest.CreatedBy,
                                 src => MapContext.Current.Parameters["user"]);
```

To pass run-time value, we need to use `BuildAdapter` method, and call `AddParameters` method to add each parameter.

```
var dto = poco.BuildAdapter()
              .AddParameters("user", this.User.Identity.Name)
              .AdaptToType<SimpleDto>();
```

#####Type-Specific Destination Transforms <a name="Transform"></a>
This allows transforms for all items of a type, such as trimming all strings. But really any operation
can be performed on the destination value before assignment.

    //Global
    TypeAdapterConfig.GlobalSettings.Default.AddDestinationTransforms((string x) => x.Trim());

    //Per mapping configuration
    TypeAdapterConfig<TSource, TDestination>.NewConfig()
        .AddDestinationTransforms((string x) => x.Trim());

#####Custom Type Resolvers <a name="ConverterFactory"></a>
In some cases, you may want to have complete control over how an object is mapped. You can register specific transformations using the `MapWith` method.

    //Example of transforming string to char[].
    TypeAdapterConfig<string, char[]>.NewConfig()
                .MapWith(str => str.ToCharArray());

`MapWith` also useful if you would like to copy instance rather than deep copy the object, for instance, `JObject` or `DbGeography`, these should treat as primitive types rather than POCO.

    TypeAdapterConfig<JObject, JObject>.NewConfig()
                .MapWith(json => json);

####Validation <a name="Validate"></a>
To validate your mapping in unit tests and in order to help with "Fail Fast" situations, the following strict mapping modes have been added.

#####Explicit Mapping <a name="ExplicitMapping"></a>
Forcing all classes to be explicitly mapped:

    //Default is "false"
    TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = true;
    //This means you have to have an explicit configuration for each class, even if it's just:
    TypeAdapterConfig<Source, Destination>.NewConfig();

#####Checking Destination Member <a name="CheckDestinationMember"></a>
Forcing all destination properties to have a corresponding source member or explicit mapping/ignore:

    //Default is "false"
    TypeAdapterConfig.GlobalSettings.RequireDestinationMemberSource = true;

#####Validating Mappings <a name="Compile"></a>
Both a specific TypeAdapterConfig<Source, Destination> or all current configurations can be validated. In addition, if Explicit Mappings (above) are enabled, it will also include errors for classes that are not registered at all with the mapper.
=======
Debugging generated mapping function!
![image](https://cloud.githubusercontent.com/assets/5763993/26521773/180427b6-431b-11e7-9188-10c01fa5ba5c.png)

Mapping your DTOs directly to EF6!
```
var poco = dto.BuildAdapter()
              .CreateEntityFromContext(db)
              .AdaptToType<DomainPoco>();
```
>>>>>>> refs/remotes/MapsterMapper/master

### Change logs
https://github.com/MapsterMapper/Mapster/releases

### Usages
https://github.com/MapsterMapper/Mapster/wiki
