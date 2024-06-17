using System.Collections.Generic;
using UnityEngine;

public interface IBuilding : IInteractive 
{
    public Dictionary<string, int> GetRequiredResources();

    public void SetColor(Color color);
}
