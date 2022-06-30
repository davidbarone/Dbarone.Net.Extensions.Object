# Dbarone.Net.Extensions.Object


>## T:Dbarone.Net.Extensions.Object.ObjectExtensions

 A collection of object extension methods. 

---
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
|obj1: |The first object.|
|obj2: |The second object to compare.|

---
### M:Dbarone.Net.Extensions.Object.ObjectExtensions.ValueEquals(System.Object,System.Object)
 Compares 2 objects and returns true if they are equivalent in value. Reference types are compared by doing a ValueEquals on all public properties and fields recursively, and collections are compared by element. For collections, order is important. 
|Name | Description |
|-----|------|
|obj1: |First object to compare.|
|obj2: |Second object to compare.|

---
