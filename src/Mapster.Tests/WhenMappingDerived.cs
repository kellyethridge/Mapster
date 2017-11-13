<<<<<<< HEAD
﻿using NUnit.Framework;

namespace Mapster.Tests
{
    [TestFixture]
    public class WhenMappingDerived
    {
        [TearDown]
        public void TearDown()
=======
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Mapster.Tests
{
    [TestClass]
    public class WhenMappingDerived
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
        public void WhenCompilingConfigDerivedWithoutMembers()
        {
            //Arrange
            var config = TypeAdapterConfig<Entity, DerivedDto>.NewConfig()
                                                           .ConstructUsing(entity => new DerivedDto(entity.Id))
                                                           .Ignore(domain => domain.Id)
                                                           ;

            //Act && Assert
<<<<<<< HEAD
            Assert.DoesNotThrow(() => config.Compile());
        }

        [Test]
=======
            Should.NotThrow(() => config.Compile());
        }

        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void WhenMappingDerivedWithoutMembers()
        {
            //Arrange
            var inputEntity = new Entity {Id = 2L};

            var config = TypeAdapterConfig<Entity, DerivedDto>.NewConfig()
                                                           .ConstructUsing(entity => new DerivedDto(entity.Id))
                                                           .Ignore(domain => domain.Id)
                                                           ;
            config.Compile();
            //Act
            var result = TypeAdapter.Adapt<Entity, DerivedDto>(inputEntity);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(inputEntity.Id, result.Id);
        }

<<<<<<< HEAD
        private class BaseDto
=======
        internal class BaseDto
>>>>>>> refs/remotes/MapsterMapper/master
        {
            public long Id { get; set; }

            protected BaseDto(long id)
            {
                Id = id;
            }
        }

<<<<<<< HEAD
        private class Entity
=======
        internal class Entity
>>>>>>> refs/remotes/MapsterMapper/master
        {
            public long Id { get; set; }
        }

<<<<<<< HEAD
        private class DerivedDto : BaseDto
=======
        internal class DerivedDto : BaseDto
>>>>>>> refs/remotes/MapsterMapper/master
        {
            public DerivedDto(long id) : base(id) { }
        }
    }
}
