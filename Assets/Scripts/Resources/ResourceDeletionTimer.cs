using UnityEngine;

[RequireComponent(typeof(Resource))]
[RequireComponent(typeof(Rigidbody))]
public class ResourceDeletionTimer : MonoBehaviour
{
    [SerializeField, Min(1.0f)] private float _timeDelete;

    private Resource _resource;
    private Rigidbody _rigidbody;
    private Timer _timer;

    private void Awake()
    {
        _resource = GetComponent<Resource>();
        _rigidbody = GetComponent<Rigidbody>();

        _timer = new Timer(_timeDelete);
        _timer.UpdateWaitingTime();
    }

    private void Update()
    {
        if (_rigidbody.isKinematic == false)
        {
            if (_timer.IsTimeUp)
            {
                _timer.UpdateWaitingTime();
                _resource.Remove();
            }
            else
                _timer.MakeCountdown();
        }
        else
            _timer.UpdateWaitingTime();  
    }
}
