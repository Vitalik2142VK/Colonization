using System;

public class IronPlace : ResourcePlace
{
    private void Awake()
    {
        EstablishCountResources();
    }

    public override Type GetTypeResource()
    {
        return typeof(Iron);
    }
}
