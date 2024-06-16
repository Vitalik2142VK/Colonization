using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour, IBuilding
{
    public abstract Dictionary<string, int> GetRequiredResources();
}
