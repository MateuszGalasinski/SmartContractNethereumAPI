using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DTOTypeBuilding.Tests
{
    [TestFixture]
    public class TypeBuilderTests
    {
        [Test]
        public void SinglePropertyTest()
        {
            Type result = DTOTypeBuilder.CreateNewType(new List<PropertyMetadata>()
            {
                new PropertyMetadata()
                {
                    Name = "Watchman's Ease",
                    Type = typeof(string)
                }
            });
            PropertyInfo property = result.GetProperty("Watchman's Ease");
            var instance = Activator.CreateInstance(result);
            property.SetValue(instance, "28:12");
        }

        [Test]
        public void ObjectWithTwoProperties()
        {
            object result = DTOTypeBuilder.CreateNewObject(new List<PropertyMetadata>()
            {
                new PropertyMetadata()
                {
                    Name = "Watchman's Ease",
                    Value = "28:12",
                    Type = typeof(string)
                },
                new PropertyMetadata()
                {
                    Name = "All's Well",
                    Value = "21:00",
                    Type = typeof(string)
                }
            });
        }
    }
}
