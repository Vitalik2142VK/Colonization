using System.Collections.Generic;

public interface IBuilding : IInteractive 
{
    public abstract Dictionary<string, int> GetRequiredResources();
}
