using UnityEngine;

public class Collector : Unit
{
    // Подписаться на ивент поднятия.

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
