using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMachine1 : Machine
{
    protected override void Work()
    {
        currentProduct.name += " and Big";
    }
}
