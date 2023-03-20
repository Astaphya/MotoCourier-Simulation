using UnityEngine;

public class Vehicle : MonoBehaviour
{
   [SerializeField] protected float speed;
   [SerializeField] protected float rotationSpeed;
   [SerializeField] protected AnimationCurve rotationCurve;
   protected Rigidbody rb;
   protected float rotationRatio;
   protected RigidbodyConstraints rigidbodyConstraints;


}
