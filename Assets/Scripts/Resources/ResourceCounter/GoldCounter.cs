using System;

public class GoldCounter : ResourceCounter
{
    public override bool IsResourceTypeSuitable(Type typeResource)
    {
        return typeResource == typeof(Gold);
    }

    protected override bool IsResourceSuitable(IResource resource)
    {
        return resource is Gold;
    }
}