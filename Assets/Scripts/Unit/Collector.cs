using UnityEngine;

public class Collector : Unit
{
    // ����������� �� ����� ��������.

    private Resource _portableResource;

    private void Awake()
    {
        GetComponents();
    }

    private void Update()
    {
        MoverUnit.Move();
    }
}
