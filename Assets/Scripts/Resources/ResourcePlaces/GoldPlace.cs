using System;

public class GoldPlace : ResourcePlace
{
    private void Awake()
    {
        EstablishCountResources();
    }

    public override Type GetTypeResource()
    {
        return typeof(Gold);
    }
}
