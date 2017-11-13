using Mapster.Tests.Classes;
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
    public class WhenCreatingConfigInstances
    {
        [Test]
=======
    [TestClass]
    public class WhenCreatingConfigInstances
    {
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Basic_Poco_Is_Mapped_With_New_Config()
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Customer, CustomerDTO>()
                .Map(dest => dest.Address_Country, src => "TestAddressCountry");

            var customer = new Customer
            {
                Id = 12345,
                Name = "TestName",
                Surname = "TestSurname"
            };

            var customerDto = customer.Adapt<CustomerDTO>(config);

            customerDto.Id.ShouldBe(12345);
            customerDto.Name.ShouldBe("TestName");
            customerDto.Address_Country.ShouldBe("TestAddressCountry");
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void ForType_Enhances_Config()
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Customer, CustomerDTO>()
                .Map(dest => dest.Address_Country, src => "TestAddressCountry");

            config.ForType<Customer, CustomerDTO>()
                .Map(dest => dest.Name, src => src.Name + "_Enhanced");

            var customer = new Customer
            {
                Id = 12345,
                Name = "TestName",
                Surname = "TestSurname"
            };

            var customerDto = customer.Adapt<CustomerDTO>(config);

            customerDto.Id.ShouldBe(12345);
            customerDto.Name.ShouldBe("TestName_Enhanced");
            customerDto.Address_Country.ShouldBe("TestAddressCountry");
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void NewConfig_Overwrites_Config()
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<Customer, CustomerDTO>()
                .Map(dest => dest.Name, src => src.Name + "_Enhanced");

            config.NewConfig<Customer, CustomerDTO>()
                .Map(dest => dest.Address_Country, src => "TestAddressCountry");

            var customer = new Customer
            {
                Id = 12345,
                Name = "TestName",
                Surname = "TestSurname"
            };

            var customerDto = customer.Adapt<CustomerDTO>(config);

            customerDto.Id.ShouldBe(12345);
            customerDto.Name.ShouldBe("TestName");
            customerDto.Address_Country.ShouldBe("TestAddressCountry");
        }
    }
}