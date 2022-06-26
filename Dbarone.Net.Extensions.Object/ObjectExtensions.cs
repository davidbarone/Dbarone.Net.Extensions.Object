namespace Dbarone.Net.Extensions.Object;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System;
using System.Collections;

/// <summary>
/// A collection of object extension methods.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Merges properties from multiple objects.
    /// </summary>
    /// <param name="obj1">The current object.</param>
    /// <param name="obj2">A variable number of objects to merge into the current object.</param>
    /// <returns>A dictionary containing the merged values.</returns>
    public static IDictionary<string, object?> Extend(this object obj1, params object[] obj2)
    {
        IDictionary<string, object?> ret = new Dictionary<string, object?>();

        if (obj1 != null)
        {
            var obj1Dict = obj1.ToDictionary();

            if (obj1Dict != null)
            {
                // dyanamic or plain dictionary
                foreach (var key in obj1Dict.Keys)
                {
                    ret[key] = obj1Dict[key];
                }
            }
        }

        foreach (var item in obj2)
        {
            if (item != null)
            {
                var itemDict = item.ToDictionary();
                if (itemDict != null)
                {
                    foreach (var key in itemDict.Keys)
                    {
                        ret[key] = itemDict[key];
                    }
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// Converts an object to a dictionary.
    /// </summary>
    /// <param name="obj">The object to convert to a dictionary.</param>
    /// <param name="keyMapper">Optional Func to map key names.</param>
    /// <param name="valueMapper">Optional Fun to map values. The Func parameters are key (string) and object (value).</param>
    /// <returns>A new dictionary object.</returns>
    public static IDictionary<string, object?>? ToDictionary(this object? obj, Func<string, string>? keyMapper = null, Func<string, object?, object?>? valueMapper = null)
    {
        if (obj == null)
            return null;

        var oDict = obj as IDictionary<string, object?>;

        if (oDict != null)
            // Already a dictionary
            return oDict;
        else
        {
            // Map properties to new dictionary
            var t = obj.GetType();
            Dictionary<string, object?> dict = new Dictionary<string, object?>();
            foreach (var pi in t.GetProperties())
            {
                var k = pi.Name;
                var v = pi.GetValue(obj);

                if (keyMapper != null)
                {
                    k = keyMapper.Invoke(k);
                }
                if (valueMapper != null)
                {
                    v = valueMapper.Invoke(k, v);
                }
                dict[k] = v;
            }
            return dict;
        }
    }

    /// <summary>
    /// Compares the current object to another object.
    /// </summary>
    /// <param name="obj1">The first object.</param>
    /// <param name="obj2">The second object to compare.</param>
    /// <returns>Returns -1 if obj1 is less than obj2, zero if the objects are equal, and +1 if obj1 is greater than obj2.</returns>
    public static int CompareTo(this object obj1, object obj2)
    {
        Type t = obj1.GetType();
        var modifiedValue = Convert.ChangeType(obj2, t);

        var objAsComparable = (IComparable)obj1;
        var valueAsComparable = (IComparable)modifiedValue;

        var result = objAsComparable.CompareTo(valueAsComparable);
        return result;
    }

    /// <summary>
    /// Compares 2 objects and returns true if they are equivalent in value. Reference types are compared property by property, and collections are compared by element.
    /// </summary>
    /// <param name="obj1">First object to compare.</param>
    /// <param name="obj2">Second object to compare.</param>
    /// <returns></returns>
    public static bool Equivalent(this object? obj1, object? obj2)
    {
        if (object.ReferenceEquals(obj1, obj2))
        {
            // optimise for both objects being same instance, or both null.
            return true;
        }
        else if (obj1 == null || obj2 == null)
        {
            // either object null - return false
            return false;
        }

        var obj1Type = obj1.GetType();
        var obj2Type = obj2.GetType();

        if (obj1Type != obj2Type)
        {
            // different types - not equal
            return false;
        }
        else if (obj1Type.IsValueType)
        {
            // for struct types, just call Equals()
            return obj1.Equals(obj2);
        }
        else if (typeof(IEnumerable).IsAssignableFrom(obj1Type))
        {
            // IEnumerable - check every item in obj1 exists in obj2
            // this is NOT performant yet.
            var list1 = new List<object>();
            var list2 = new List<object>();
            foreach (var item in ((IEnumerable)obj1))
            {
                list1.Add(item);
            }
            foreach (var item in ((IEnumerable)obj2))
            {
                list2.Add(item);
            }

            if (list1.Count() != list2.Count())
            {
                return false;
            }
            else
            {
                return !list1.Any(l1 => !list2.Any(l2 => l2.Equivalent(l1)));
            }
        }
        else
        {
            // compare properties recursively
            var properties = obj1.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (!property.GetValue(obj1).Equivalent(property.GetValue(obj2)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
