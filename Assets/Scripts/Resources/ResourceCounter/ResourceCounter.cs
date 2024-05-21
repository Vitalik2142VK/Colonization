using TMPro;
using UnityEngine;

public abstract class ResourceCounter : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private TextMeshProUGUI _counterOutput;

    private int _countResources = 0;
    private int _maxCount = 5;

    public bool IsFull => _countResources >= _maxCount;

    private void OnEnable()
    {
        _base.ResourceDelivered += OnAddResource;
    }

    private void Start()
    {
        UpdateView();
    }

    private void OnDisable()
    {
        _base.ResourceDelivered -= OnAddResource;
    }

    private void OnAddResource(Resource resource)
    {
        Debug.Log($"Check resource in ResourceCounter {IsResourceSuitable(resource)}"); // delete

        if (IsResourceSuitable(resource))
        {
            _countResources++;

            resource.Remove();

            UpdateView();
        }
    }

    private void UpdateView()
    {
        _counterOutput.text = $"{_countResources}/{_maxCount}";
    }

    public abstract bool IsResourceSuitable(IResource resource);
}
