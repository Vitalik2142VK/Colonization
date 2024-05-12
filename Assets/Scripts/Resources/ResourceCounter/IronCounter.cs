public class IronCounter : ResourceCounter
{
    public override bool IsResourceSuitable(IResource resource)
    {
        return resource is IIron;
    }
}
