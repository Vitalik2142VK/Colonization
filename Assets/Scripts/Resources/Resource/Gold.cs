public class Gold : Resource, IGold
{
    public override bool IsItSameType(IResource resource)
    {
        return resource is IGold;
    }
}
