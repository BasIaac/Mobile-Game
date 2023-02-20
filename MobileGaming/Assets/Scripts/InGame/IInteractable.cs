using System;

public interface IInteractable
{
    public void Interact(Product inProduct,out Product outProduct);
    public static event Action<IInteractable> OnRangeEnter;
    public static event Action<IInteractable> OnRangeExit;
}
