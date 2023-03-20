using UnityEngine;

public class RagdollModeController : MonoBehaviour
{
    [SerializeField] private Collider mainCollider;
    [SerializeField] private GameObject couriersRig;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetRagdollBits();
        RagdollModeOff();
        
    }
    
    
    private Collider[] ragdollColliders;
    private Rigidbody[] limbsRigidbodies;
    private void GetRagdollBits()
    {
        ragdollColliders = couriersRig.GetComponentsInChildren<Collider>();
        limbsRigidbodies = couriersRig.GetComponentsInChildren<Rigidbody>();

    }

    public void RagdollModeOn()
    {
        foreach (var col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (var rig in limbsRigidbodies)
        {
            rig.isKinematic = false;

        }

        transform.parent = null;
        rb.useGravity = true;
        mainCollider.enabled = false;
        rb.isKinematic = false;
        
        
        
    }

    private void RagdollModeOff()
    {
        foreach (var col in ragdollColliders)
        {
            col.enabled = false;
        }

        foreach (var rig in limbsRigidbodies)
        {
            rig.isKinematic = true;

        }

        rb.useGravity = false;
        mainCollider.enabled = true;
        rb.isKinematic = true;

    }
}
