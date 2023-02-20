using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMachine : Machine
{
    protected override void Work()
    {
        currentProduct.name += " and Red";
    }
}
