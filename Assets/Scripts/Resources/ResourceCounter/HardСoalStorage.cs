using System;

public class HardСoalStorage : ResourceStorage
{
    public override bool IsResourceTypeSuitable(Type typeResource)
    {
        return typeResource == typeof(HardСoal);
    }

    protected override bool IsResourceSuitable(IResource resource)
    {
        return resource is HardСoal;
    }
}
