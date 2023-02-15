public interface IInteractible
{
    public bool Interactable { get; }
    public void Interact(Product product);
    public void SetInteractible();
    public void UnSetInteractible();
}
