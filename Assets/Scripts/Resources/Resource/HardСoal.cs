public class HardСoal : Resource, IHardСoal
{
    public override bool IsItSameType(IResource resource)
    {
        return resource is IHardСoal;
    }
}
