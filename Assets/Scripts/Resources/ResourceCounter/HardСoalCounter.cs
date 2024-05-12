public class HardСoalCounter : ResourceCounter
{
    public override bool IsResourceSuitable(IResource resource)
    {
        return resource is IHardСoal;
    }
}
