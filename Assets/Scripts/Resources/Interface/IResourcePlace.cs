using System;

public interface IResourcePlace : IInteractive
{
    public void GiveResource();

    public Type GetTypeResource();
}
