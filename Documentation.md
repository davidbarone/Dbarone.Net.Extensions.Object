# Dbarone.Net.Extensions.Object
### M:Dbarone.Net.Extensions.Object.ObjectExtensions.Extend(System.Object,System.Object[])
 Merges properties from multiple objects. 
|Name | Description |
|-----|------|
|obj1: |The current object.|
|obj2: |A variable number of objects to merge into the current object.|

---
### M:Dbarone.Net.Extensions.Object.ObjectExtensions.ToDictionary(System.Object,System.Func{System.String,System.String},System.Func{System.String,System.Object,System.Object})
 Converts an object to a dictionary. 
|Name | Description |
|-----|------|
|obj: |The object to convert to a dictionary.|
|keyMapper: |Optional Func to map key names.|
|valueMapper: |Optional Fun to map values. The Func parameters are key (string) and object (value).|

---
### M:Dbarone.Net.Extensions.Object.ObjectExtensions.CompareTo(System.Object,System.Object)
 Compares the current object to another object. 
|Name | Description |
|-----|------|
|obj: ||
|value: ||

---
### M:Dbarone.Net.Extensions.Object.ObjectExtensions.Equivalent(System.Object,System.Object)
 Compares 2 objects and returns true if they are equivalent in value. Reference types are compared property by property, and collections are compared by element. 
|Name | Description |
|-----|------|
|obj1: |First object to compare.|
|obj2: |Second object to compare.|

---
