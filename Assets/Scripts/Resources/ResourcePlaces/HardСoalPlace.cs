using System;

public class HardСoalPlace : ResourcePlace
{
    private void Awake()
    {
        EstablishCountResources();
    }

    public override Type GetTypeResource()
    {
        return typeof(HardСoal);
    }
}
