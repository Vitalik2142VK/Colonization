using TMPro;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterOutput;

    public void UpdateView(int countResources, int maxCount)
    {
        _counterOutput.text = $"{countResources}/{maxCount}";
    }
}