using System;

public class HardСoalCounter : ResourceCounter
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
