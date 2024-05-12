public class GoldCounter : ResourceCounter
{
    public override bool IsResourceSuitable(IResource resource)
    {
        return resource is IGold;
    }
}