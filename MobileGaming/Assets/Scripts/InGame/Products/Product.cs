using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product
{
    public string name = "new Product";

    public ProductData data;

    public Product(ProductData data)
    {
        this.data = data;
    }
}

public struct ProductData
{
    public ProductShape Shape;
    public ProductColor Color;
    public ProductSize Size;
    
    public static bool operator ==(ProductData data1, ProductData data2) 
    {
        return data1.Equals(data2);
    }

    public static bool operator !=(ProductData data1, ProductData data2) 
    {
        return !data1.Equals(data2);
    }
}

public enum ProductShape {Good,Bad}
public enum ProductColor {White,Shiny}
public enum ProductSize {Tiny,Small,Normal,Big,Huge}
