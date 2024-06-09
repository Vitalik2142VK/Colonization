using System;

public class IronCounter : ResourceCounter
{
    public override bool IsResourceTypeSuitable(Type typeResource)
    {
        return typeResource == typeof(Iron);
    }

    protected override bool IsResourceSuitable(IResource resource)
    {
        return resource is Iron;
    }

}
