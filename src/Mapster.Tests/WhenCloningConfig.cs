using System;
using System.Linq;
using Mapster.Models;
<<<<<<< HEAD
using NUnit.Framework;
=======
using Microsoft.VisualStudio.TestTools.UnitTesting;
>>>>>>> refs/remotes/MapsterMapper/master
using Shouldly;

namespace Mapster.Tests
{
<<<<<<< HEAD
    [TestFixture]
    public class WhenCloningConfig
    {
        [TearDown]
        public void TearDown()
=======
    [TestClass]
    public class WhenCloningConfig
    {
        [TestCleanup]
        public void TestCleanup()
>>>>>>> refs/remotes/MapsterMapper/master
        {
            TypeAdapterConfig.GlobalSettings.Clear();
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Alter_Config_After_Map_Should_Error()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .Map(dest => dest.Name, src => "a");

            var poco = new SimplePoco
            {
                Id = Guid.NewGuid(),
                Name = "test",
            };
            var result = TypeAdapter.Adapt<SimpleDto>(poco);
            result.Name.ShouldBe("a");

<<<<<<< HEAD
            var ex = Assert.Throws<InvalidOperationException>(() =>
=======
            var ex = Should.Throw<InvalidOperationException>(() =>
>>>>>>> refs/remotes/MapsterMapper/master
                TypeAdapterConfig<SimplePoco, SimpleDto>.ForType()
                    .Map(dest => dest.Name, src => "b"));
            ex.Message.ShouldContain("TypeAdapter.Adapt was already called");
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Clone()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .Map(dest => dest.Name, src => "a");

            var poco = new SimplePoco
            {
                Id = Guid.NewGuid(),
                Name = "test",
            };
            var result = TypeAdapter.Adapt<SimpleDto>(poco);
            result.Name.ShouldBe("a");

            var config = TypeAdapterConfig.GlobalSettings.Clone();
            var global = TypeAdapterConfig.GlobalSettings;
            config.ShouldNotBeSameAs(global);
            config.Default.ShouldNotBeSameAs(global.Default);
            config.Default.Settings.ShouldNotBeSameAs(global.Default.Settings);
            config.RuleMap.ShouldNotBeSameAs(global.RuleMap);
            foreach (var kvp in config.RuleMap)
            {
                var globalRule = global.RuleMap[kvp.Key];
                kvp.Value.ShouldNotBeSameAs(globalRule);
                kvp.Value.Settings.ShouldNotBeSameAs(globalRule.Settings);
            }
            config.Rules.ShouldNotBeSameAs(global.Rules);
            for (var i = 0; i < config.Rules.Count; i++)
            {
                config.Rules[i].ShouldNotBeSameAs(global.Rules[i]);
                config.Rules[i].Settings.ShouldNotBeSameAs(global.Rules[i].Settings);
            }
            config.Rules.Any(rule => object.ReferenceEquals(rule.Settings, config.Default.Settings)).ShouldBeTrue();
            config.Rules.ShouldContain(config.RuleMap[new TypeTuple(typeof(SimplePoco), typeof(SimpleDto))]);
        }

        public class SimplePoco
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class SimpleDto
        {
            public Guid Id { get; set; }
<<<<<<< HEAD
            public string Name { get; protected set; }
=======
            public string Name { get; internal set; }
>>>>>>> refs/remotes/MapsterMapper/master
        }
    }
}
