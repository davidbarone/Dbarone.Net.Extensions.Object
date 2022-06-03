namespace Dbarone.Net.Extensions.Object;

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
    /// <param name="obj"></param>
    /// <param name="value"></param>
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
}
