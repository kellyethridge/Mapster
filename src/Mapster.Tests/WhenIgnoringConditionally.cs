﻿using NUnit.Framework;
using Shouldly;

namespace Mapster.Tests
{
    [TestFixture]
    public class WhenIgnoringConditionally
    {

        #region Tests

        [Test]
        public void True_Constant_Ignores_Map()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .IgnoreIf((src, dest) => true, dest => dest.Name)
                .Compile();

            var poco = new SimplePoco { Id = 1, Name = "TestName" };
            SimpleDto dto = TypeAdapter.Adapt<SimplePoco, SimpleDto>(poco);

            dto.Id.ShouldBe(1);
            dto.Name.ShouldBeNull();
        }

        [Test]
        public void True_Constant_Ignores_Map_To_Target()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .IgnoreIf((src, dest) => true, dest => dest.Name)
                .Compile();

            var poco = new SimplePoco { Id = 1, Name = "TestName" };
            var dto = new SimpleDto { Id = 999, Name = "DtoName" };
            TypeAdapter.Adapt(poco, dto);

            dto.Id.ShouldBe(1);
            dto.Name.ShouldBe("DtoName");
        }

        [Test]
        public void True_Condition_Ignores_Map()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .IgnoreIf((src, dest) => src.Name == "TestName", dest => dest.Name)
                .Compile();

            var poco = new SimplePoco { Id = 1, Name = "TestName" };
            SimpleDto dto = TypeAdapter.Adapt<SimplePoco, SimpleDto>(poco);

            dto.Id.ShouldBe(1);
            dto.Name.ShouldBeNull();
        }

        [Test]
        public void True_Condition_Ignores_Map_To_Target()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .IgnoreIf((src, dest) => src.Name == "TestName", dest => dest.Name)
                .Compile();

            var poco = new SimplePoco { Id = 1, Name = "TestName" };
            var dto = new SimpleDto { Id = 999, Name = "DtoName" };
            TypeAdapter.Adapt(poco, dto);

            dto.Id.ShouldBe(1);
            dto.Name.ShouldBe("DtoName");
        }

        [Test]
        public void Null_Condition_Ignores_Map()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .IgnoreIf(null, dest => dest.Name)
                .Compile();

            var poco = new SimplePoco { Id = 1, Name = "TestName" };
            var dto = TypeAdapter.Adapt<SimplePoco, SimpleDto>(poco);

            dto.Id.ShouldBe(1);
            dto.Name.ShouldBeNull();
        }

        [Test]
        public void Null_Condition_Ignores_Map_To_Target()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .IgnoreIf(null, dest => dest.Name)
                .Compile();

            var poco = new SimplePoco { Id = 1, Name = "TestName" };
            var dto = new SimpleDto { Id = 999, Name = "DtoName" };
            TypeAdapter.Adapt(poco, dto);

            dto.Id.ShouldBe(1);
            dto.Name.ShouldBe("DtoName");
        }

        [Test]
        public void True_Condition_On_Target_Ignores_Map()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .IgnoreIf((src, dest) => !string.IsNullOrEmpty(dest.Name), dest => dest.Name)
                .Compile();

            var poco = new SimplePoco { Id = 1, Name = "TestName" };
            var dto = TypeAdapter.Adapt<SimplePoco, SimpleDto>(poco);

            dto.Id.ShouldBe(1);
            dto.Name.ShouldBe("TestName");
        }

        [Test]
        public void True_Condition_On_Target_Ignores_Map_To_Target()
        {
            TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
                .IgnoreIf((src, dest) => !string.IsNullOrEmpty(dest.Name), dest => dest.Name)
                .Compile();

            var poco = new SimplePoco { Id = 1, Name = "TestName" };
            var dto = new SimpleDto { Id = 999, Name = "DtoName" };
            TypeAdapter.Adapt(poco, dto);

            dto.Id.ShouldBe(1);
            dto.Name.ShouldBe("DtoName");
        }

        #endregion


        #region TestClasses

        public class SimplePoco
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class SimpleDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        #endregion

    }
}
