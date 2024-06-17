using UnityEngine;

public class LevelBorders : MonoBehaviour, IInteractive
{
    [SerializeField] private float _maxValueByX;
    [SerializeField] private float _minValueByX;
    [SerializeField] private float _maxValueByZ;
    [SerializeField] private float _minValueByZ;

    public float MaxValueByX => _maxValueByX;
    public float MinValueByX => _minValueByX;
    public float MaxValueByZ => _maxValueByZ;
    public float MinValueByZ => _minValueByZ;

    public Vector3 GetCheckedPosition(Vector3 position)
    {
        float x = position.x;
        float z = position.z;

        if (x > _maxValueByX)
            position.x = _maxValueByX;
        else if (x < _minValueByX)
            position.x = _minValueByX;

        if (z > _maxValueByZ)
            position.z = _maxValueByZ;
        else if (z < _minValueByZ)
            position.z = _minValueByZ;

        return position;
    }
}
