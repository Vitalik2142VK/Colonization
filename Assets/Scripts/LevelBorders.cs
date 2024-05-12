using UnityEngine;

public class LevelBorders : MonoBehaviour
{
    //private const float RotateBorderByY = 90.0f;

    //[SerializeField] private float _maxValueByX;
    //[SerializeField] private float _minValueByX;
    //[SerializeField] private float _maxValueByZ;
    //[SerializeField] private float _minValueByZ;

    //private Vector3 _sizeCollider;
    //private float _multiplierLengthCollider = 2.0f;
    //private float _heightCollider = 100.0f;
    //private float _widthCollider = 100.0f;
    //private float _currentRotareBorder = 0.0f;

    //public float MaxValueByX => _maxValueByX;
    //public float MinValueByX => _minValueByX;
    //public float MaxValueByZ => _maxValueByZ;
    //public float MinValueByZ => _minValueByZ;

    //private void Start()
    //{
    //    float lengthCollider = _multiplierLengthCollider * _maxValueByX;
    //    _sizeCollider = new Vector3(lengthCollider, _heightCollider, _widthCollider);

    //    Vector3[] bordersPositions = new Vector3[] 
    //    {
    //        new Vector3(0.0f, 0.0f, _maxValueByZ),
    //        new Vector3(_maxValueByX, 0.0f, 0.0f),
    //        new Vector3(0.0f, 0.0f, _minValueByZ),
    //        new Vector3(_minValueByX, 0.0f, 0.0f)
    //    };

    //    foreach (var borderPosition in bordersPositions)
    //    {
    //        CreatedBorder(borderPosition);
    //    }
    //}

    //private void CreatedBorder(Vector3 position)
    //{
    //    GameObject positionBorder = new GameObject();
    //    positionBorder.transform.parent = transform;
    //    positionBorder.transform.SetPositionAndRotation(position, Quaternion.Euler(new Vector3(0.0f, _currentRotareBorder, 0.0f)));

    //    BoxCollider collider = positionBorder.AddComponent<BoxCollider>();
    //    collider.size = _sizeCollider;
    //    collider.isTrigger = true;

    //    _currentRotareBorder += RotateBorderByY;
    //}
}
