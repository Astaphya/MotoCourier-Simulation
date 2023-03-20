using Motor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 currentVelocity = Vector3.zero;
    private MotorBikeController motor;

    private void Awake()
    {
        offset = transform.position - target.position;
      //  motor = target.GetComponentInParent<MotorBike>();
    }
    
    private void LateUpdate()
    {
       // if (motor.isColided) return;
        var targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);



    }
}
