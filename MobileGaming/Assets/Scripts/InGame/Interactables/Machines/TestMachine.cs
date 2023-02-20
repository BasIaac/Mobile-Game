public class TestMachine : Machine
{
    protected override void Work()
    {
        currentProduct.data.Color = ProductColor.Shiny;
    }
}
