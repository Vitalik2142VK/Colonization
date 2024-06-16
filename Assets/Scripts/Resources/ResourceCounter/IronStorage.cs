using System;

public class IronStorage : ResourceStorage
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
