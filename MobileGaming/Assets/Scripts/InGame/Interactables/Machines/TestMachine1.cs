public class TestMachine1 : Machine
{
    protected override void Work()
    {
        currentProduct.data.Size = ProductSize.Big;
    }
}
