using System;

public class Product
{
    public ProductData data;

    public Product(ProductData data)
    {
        this.data = data;
    }

    public override string ToString()
    {
        return $"{data.Color}, {data.Shape} and {data.Size} Product";
    }
}

[Serializable]
public struct ProductData
{
    public ProductColor Color;
    public ProductShape Shape;
    public ProductSize Size;
    
    public static bool operator ==(ProductData data1, ProductData data2) 
    {
        return data1.Equals(data2);
    }

    public static bool operator !=(ProductData data1, ProductData data2) 
    {
        return !data1.Equals(data2);
    }

    public static ProductData Random => new ProductData()
    {
        Shape = GetRandomEnum<ProductShape>(),
        Color = GetRandomEnum<ProductColor>(),
        Size = GetRandomEnum<ProductSize>(),
    };

    public static T GetRandomEnum<T>()
    {
        var values = Enum.GetValues(typeof(T)); 
        return (T) values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }
}

public enum ProductShape {Good,Bad}
public enum ProductColor {White,Shiny}
public enum ProductSize {Tiny,Small,Normal,Big,Huge}


