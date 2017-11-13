<<<<<<< HEAD
﻿using NUnit.Framework;
=======
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
>>>>>>> refs/remotes/MapsterMapper/master
using Shouldly;

namespace Mapster.Tests
{
<<<<<<< HEAD
    [TestFixture]
    public class WhenMappingPrivateFieldsAndProperties
    {
        [Test]
=======
    [TestClass]
    public class WhenMappingPrivateFieldsAndProperties
    {
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Default_Settings_Should_Not_Map_Private_Fields_To_New_Object()
        {
            TypeAdapterConfig<CustomerWithPrivateField, CustomerDTO>
                .NewConfig()
                .NameMatchingStrategy(NameMatchingStrategy.Flexible);

            var customerId = 1;
            var customerName = "Customer 1";
            var aCustomer = new CustomerWithPrivateField(customerId, customerName);

            var dto = aCustomer.Adapt<CustomerDTO>();

<<<<<<< HEAD
            Assert.NotNull(dto);
=======
            Assert.IsNotNull(dto);
>>>>>>> refs/remotes/MapsterMapper/master
            dto.Id.ShouldNotBe(customerId);
            dto.Name.ShouldBe(customerName);
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Default_Settings_Should_Not_Map_Private_Properties_To_New_Object()
        {
            var customerId = 1;
            var customerName = "Customer 1";
            var aCustomer = new CustomerWithPrivateProperty(customerId, customerName);

            var dto = aCustomer.Adapt<CustomerDTO>();

<<<<<<< HEAD
            Assert.NotNull(dto);
=======
            Assert.IsNotNull(dto);
>>>>>>> refs/remotes/MapsterMapper/master
            dto.Id.ShouldBe(customerId);
            dto.Name.ShouldNotBe(customerName);
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Should_Map_Private_Field_To_New_Object_Correctly()
        {
            SetUpMappingNonPublicFields<CustomerWithPrivateField, CustomerDTO>();

            var customerId = 1;
            var customerName = "Customer 1";
            var aCustomer = new CustomerWithPrivateField(customerId, customerName);

            var dto = aCustomer.Adapt<CustomerDTO>();

<<<<<<< HEAD
            Assert.NotNull(dto);
=======
            Assert.IsNotNull(dto);
>>>>>>> refs/remotes/MapsterMapper/master
            dto.Id.ShouldBe(customerId);
            dto.Name.ShouldBe(customerName);
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Should_Map_Private_Property_To_New_Object_Correctly()
        {
            SetUpMappingNonPublicProperties<CustomerWithPrivateProperty, CustomerDTO>();

            var customerId = 1;
            var customerName = "Customer 1";
            var aCustomer = new CustomerWithPrivateProperty(customerId, customerName);

            var dto = aCustomer.Adapt<CustomerDTO>();

<<<<<<< HEAD
            Assert.NotNull(dto);
=======
            Assert.IsNotNull(dto);
>>>>>>> refs/remotes/MapsterMapper/master
            dto.Id.ShouldBe(customerId);
            dto.Name.ShouldBe(customerName);
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Should_Map_To_Private_Fields_Correctly()
        {
            SetUpMappingNonPublicFields<CustomerDTO, CustomerWithPrivateField>();
            
            var dto = new CustomerDTO
            {
                Id = 1,
                Name = "Customer 1"
            };

            var customer = dto.Adapt<CustomerWithPrivateField>();

<<<<<<< HEAD
            Assert.NotNull(customer);
=======
            Assert.IsNotNull(customer);
>>>>>>> refs/remotes/MapsterMapper/master
            Assert.IsTrue(customer.HasId(dto.Id));
            customer.Name.ShouldBe(dto.Name);            
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Should_Map_To_Private_Properties_Correctly()
        {
            SetUpMappingNonPublicFields<CustomerDTO, CustomerWithPrivateProperty>();

            var dto = new CustomerDTO
            {
                Id = 1,
                Name = "Customer 1"
            };

            var customer = dto.Adapt<CustomerWithPrivateProperty>();

<<<<<<< HEAD
            Assert.NotNull(customer);
=======
            Assert.IsNotNull(customer);
>>>>>>> refs/remotes/MapsterMapper/master
            customer.Id.ShouldBe(dto.Id);
            Assert.IsTrue(customer.HasName(dto.Name));
        }

        private void SetUpMappingNonPublicFields<TSource, TDestination>()
        {
            var config = TypeAdapterConfig<TSource, TDestination>.NewConfig();
<<<<<<< HEAD
            config.EnableNonPublicMembers();
=======
            config.EnableNonPublicMembers(true);
>>>>>>> refs/remotes/MapsterMapper/master
            config.NameMatchingStrategy(NameMatchingStrategy.Flexible);
        }

        private void SetUpMappingNonPublicProperties<TSource, TDestination>()
        {
            TypeAdapterConfig<TSource, TDestination>
                  .NewConfig()
<<<<<<< HEAD
                  .EnableNonPublicMembers();

        }

        #region Test Classes
=======
                  .EnableNonPublicMembers(true);

        }

        #region TestMethod Classes
>>>>>>> refs/remotes/MapsterMapper/master

        public class CustomerWithPrivateField
        {
            private int _id;
            public string Name { get; private set; }

            private CustomerWithPrivateField() { }

            public CustomerWithPrivateField(int id, string name)
            {
                _id = id;
                Name = name;
            }

            public bool HasId(int id)
            {
                return _id == id;
            }
        }

        public class CustomerWithPrivateProperty
        {
            public int Id { get; private set; }
            private string Name { get; set; }

            private CustomerWithPrivateProperty() { }

            public CustomerWithPrivateProperty(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public bool HasName(string name)
            {
                return Name == name;
            }
        }

        public class CustomerDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        #endregion
    }
}
