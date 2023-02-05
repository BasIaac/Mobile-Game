using Service;

public interface ISwitchableService : IService
{
    public bool enable
    {
        get;
    }

    public void Toggle();
    
    public void Enable();

    public void Disable();

    
}
