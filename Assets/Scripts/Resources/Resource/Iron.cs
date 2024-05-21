public class Iron : Resource, IIron
{
    public override bool IsItSameType(IResource resource)
    {
        return resource is IIron;
    }
}
