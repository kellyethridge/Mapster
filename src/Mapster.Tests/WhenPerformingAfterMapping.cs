using System;
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
    public class WhenPerformingAfterMapping
    {
        [TearDown]
        public void TearDown()
=======
    [TestClass]
    public class WhenPerformingAfterMapping
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
        public void After_Mapping()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .AfterMapping((src, dest) => dest.Name += "xxx");

            var poco = new SimplePoco
            {
                Id = Guid.NewGuid(),
                Name = "test",
            };
            var result = TypeAdapter.Adapt<SimpleDto>(poco);

            result.Id.ShouldBe(poco.Id);
            result.Name.ShouldBe(poco.Name + "xxx");
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void After_Mapping_With_DestinationType_Setting()
        {
            TypeAdapterConfig.GlobalSettings.ForDestinationType<IValidatable>()
                .AfterMapping(dest => dest.Validate());

            var poco = new SimplePoco
            {
                Id = Guid.NewGuid(),
                Name = "test",
            };
            var result = TypeAdapter.Adapt<SimpleDto>(poco);

            result.IsValidated.ShouldBeTrue();
        }

        public interface IValidatable
        {
            void Validate();
        }

        public class SimplePoco
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class SimpleDto : IValidatable
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public bool IsValidated { get; private set; }

            public void Validate()
            {
                this.IsValidated = true;
            }
        }
    }
}
