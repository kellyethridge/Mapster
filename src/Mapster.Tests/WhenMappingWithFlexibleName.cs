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
    public class WhenMappingWithFlexibleName
    {
        [TearDown]
        public void TearDown()
=======
    [TestClass]
    public class WhenMappingWithFlexibleName
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
        public void Not_Set_Match_Only_Exact_Name()
        {
            var mix = new MixName
            {
                PascalCase = "A",
                camelCase = "B",
                __under__SCORE__ = "C",
                lower_case = "D",
                UPPER_CASE = "E",
                MIX_UnderScore = "F",
            };

            var simple = TypeAdapter.Adapt<SimpleName>(mix);

            simple.PascalCase.ShouldBe(mix.PascalCase);
            simple.CamelCase.ShouldBeNull();
            simple.UnderScore.ShouldBeNull();
            simple.LowerCase.ShouldBeNull();
            simple.UpperCase.ShouldBeNull();
            simple.MixUnder_SCORE.ShouldBeNull();
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Map_Flexible_Name()
        {
            TypeAdapterConfig<MixName, SimpleName>.NewConfig().NameMatchingStrategy(NameMatchingStrategy.Flexible);

            var mix = new MixName
            {
                PascalCase = "A",
                camelCase = "B",
                __under__SCORE__ = "C",
                lower_case = "D",
                UPPER_CASE = "E",
                MIX_UnderScore = "F",
            };

            var simple = TypeAdapter.Adapt<SimpleName>(mix);

            simple.PascalCase.ShouldBe(mix.PascalCase);
            simple.CamelCase.ShouldBe(mix.camelCase);
            simple.UnderScore.ShouldBe(mix.__under__SCORE__);
            simple.LowerCase.ShouldBe(mix.lower_case);
            simple.UpperCase.ShouldBe(mix.UPPER_CASE);
            simple.MixUnder_SCORE.ShouldBe(mix.MIX_UnderScore);
        }

<<<<<<< HEAD
        [Test]
=======
        [TestMethod]
        public void Map_IgnoreCase()
        {
            TypeAdapterConfig<MixName, SimpleName>.NewConfig().NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

            var mix = new MixName
            {
                FOOBAR = "foo"
            };

            var simple = TypeAdapter.Adapt<SimpleName>(mix);

            simple.FooBar.ShouldBe(mix.FOOBAR);
        }

        [TestMethod]
>>>>>>> refs/remotes/MapsterMapper/master
        public void Test_Name()
        {
            NameMatchingStrategy.PascalCase("PascalCase").ShouldBe("PascalCase");
            NameMatchingStrategy.PascalCase("camelCase").ShouldBe("CamelCase");
            NameMatchingStrategy.PascalCase("lower_case").ShouldBe("LowerCase");
            NameMatchingStrategy.PascalCase("UPPER_CASE").ShouldBe("UpperCase");
            NameMatchingStrategy.PascalCase("IPAddress").ShouldBe("IpAddress");
            NameMatchingStrategy.PascalCase("ItemID").ShouldBe("ItemId");
            NameMatchingStrategy.PascalCase("__under__SCORE__").ShouldBe("UnderScore");
            NameMatchingStrategy.PascalCase("__MixMIXMix_mix").ShouldBe("MixMixMixMix");
        }

        [Test]
        public void Test_Name()
        {
            NameMatchingStrategy.ToPascalCase("PascalCase").ShouldEqual("PascalCase");
            NameMatchingStrategy.ToPascalCase("camelCase").ShouldEqual("CamelCase");
            NameMatchingStrategy.ToPascalCase("lower_case").ShouldEqual("LowerCase");
            NameMatchingStrategy.ToPascalCase("UPPER_CASE").ShouldEqual("UpperCase");
            NameMatchingStrategy.ToPascalCase("IPAddress").ShouldEqual("IpAddress");
            NameMatchingStrategy.ToPascalCase("ItemID").ShouldEqual("ItemId");
            NameMatchingStrategy.ToPascalCase("__under__SCORE__").ShouldEqual("UnderScore");
            NameMatchingStrategy.ToPascalCase("__MixMIXMix_mix").ShouldEqual("MixMixMixMix");
        }

        public class MixName
        {
            public string PascalCase { get; set; }
            public string camelCase { get; set; }
            public string __under__SCORE__ { get; set; }
            public string lower_case { get; set; }
            public string UPPER_CASE { get; set; }
            public string MIX_UnderScore { get; set; }
<<<<<<< HEAD
=======
            public string FOOBAR { get; set; }
>>>>>>> refs/remotes/MapsterMapper/master
        }

        public class SimpleName
        {
            public string PascalCase { get; set; }
            public string CamelCase { get; set; }
            public string UnderScore { get; set; }
            public string LowerCase { get; set; }
            public string UpperCase { get; set; }
            public string MixUnder_SCORE { get; set; }
<<<<<<< HEAD
=======
            public string FooBar { get; set; }
>>>>>>> refs/remotes/MapsterMapper/master
        }
    }
}
