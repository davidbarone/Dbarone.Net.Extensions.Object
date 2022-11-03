namespace Dbarone.Net.Extensions.Object.Tests;
using Xunit;
using System.Collections.Generic;

public struct Vector
{
    public int X;
    public int Y;
    public Vector(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class FooBarBaz {
    public int Foo { get; set; }
    public string Bar { get; set; } = default!;
    public bool Baz { get; set; }
}

public class Animal
{
    public string Name { get; set; } = default!;
    public Animal(string name)
    {
        Name = name;
    }
}

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }

    [Theory]
    [InlineData(null, null, true)]
    [InlineData(null, 1, false)]
    [InlineData(1, null, false)]
    [InlineData(1, 1, true)]
    [InlineData("foobar", "foobar", true)]
    [InlineData("foobar1", "foobar2", false)]
    public void ValueEquals(object obj1, object obj2, bool expected)
    {
        Assert.Equal(expected, obj1.ValueEquals(obj2));
    }

    [Fact]
    public void ValueEquals_Class()
    {
        var dog1 = new Animal("dog");
        var dog2 = new Animal("dog");
        var cat = new Animal("cat");

        Assert.True(dog1.ValueEquals(dog1));
        Assert.True(dog1.ValueEquals(dog2));
        Assert.False(dog1.ValueEquals(cat));
    }

    [Fact]
    public void ValueEquals_Struct()
    {
        var v1 = new Vector(1, 1);
        var v2 = new Vector(1, 1);
        var v3 = new Vector(2, 1);

        Assert.True(v1.ValueEquals(v1));
        Assert.True(v1.ValueEquals(v2));
        Assert.False(v1.ValueEquals(v3));
    }

    [Fact]
    public void ValueEquals_IEnumerable()
    {
        var arr1 = new int[] { 1, 2, 3, 4, 5 };
        var arr2 = new int[] { 1, 2, 3, 4, 5 };
        var arr3 = new int[] { 2, 1, 5, 4 };
        var arr4 = new int[] { 1, 3, 2, 5, 4 };
        var arr5 = new int[] { 1, 3, 2, 5, 4, 6 };
 
        Assert.True(arr1.ValueEquals(arr1));
        Assert.True(arr1.ValueEquals(arr2));
        Assert.False(arr1.ValueEquals(arr3));
        Assert.False(arr1.ValueEquals(arr4));
        Assert.False(arr1.ValueEquals(arr5));
    }

    [Fact]
    public void ValueEquals_IEnumerable_Class()
    {
        var dog1 = new Animal("dog");
        var dog2 = new Animal("dog");
        var cat1 = new Animal("cat");
        var cat2 = new Animal("cat");
        var arr1 = new Animal[] {dog1, dog2, cat1, cat2 };
        var arr2 = new Animal[] {dog2, dog1, cat2, cat1 };
        var arr3 = new Animal[] {dog1, dog1, cat1, cat1 };
        var arr4 = new Animal[] {cat2, dog2, cat2, dog2 };
        var arr5 = new Animal[] {dog1, dog2, cat1, cat2, cat1 };
 
        Assert.True(arr1.ValueEquals(arr1));
        Assert.True(arr1.ValueEquals(arr2));
        Assert.True(arr1.ValueEquals(arr3));
        Assert.False(arr1.ValueEquals(arr4));
        Assert.False(arr1.ValueEquals(arr5));
    }

    [Fact]
    public void ValueEquals_Dictionary(){
        var d1 = new Dictionary<string, int>(){
            {"foo", 1},
            {"bar", 2}
        };
        var d2 = new Dictionary<string, int>(){
            {"bar", 2},
            {"foo", 1}
        };
        var d3 = new Dictionary<string, int>(){
            {"foo", 1},
            {"baz", 3}
        };
        Assert.True(d1.ValueEquals(d1));
        Assert.False(d1.ValueEquals(d2));   // different order
        Assert.False(d1.ValueEquals(d3));
    }

    [Fact]
    public void Test_ToObject() {

        // Arrange
        IDictionary<string, object?>? dict = new Dictionary<string, object?>();
        dict["Foo"] = 12345;
        dict["Bar"] = "Lorem ipsum dolor";
        dict["Baz"] = true;

        // Act
        var obj = dict.ToObject<FooBarBaz>();

        // Assert
        Assert.NotNull(obj);
        Assert.Equal(12345, obj!.Foo);
        Assert.Equal("Lorem ipsum dolor", obj.Bar);
        Assert.True(obj.Baz);
    }
}
