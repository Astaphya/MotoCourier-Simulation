using UnityEngine;

namespace Motor
{
    public class MotorBikeInteraction : MonoBehaviour
    {
        [SerializeField] private MotorBikeController motorBikeController;
        [SerializeField] private RagdollModeController ragdollController;
        [SerializeField] private ParticleSystem crashVFX;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Cube"))
            {
                crashVFX.transform.position = collision.gameObject.transform.position;
                crashVFX.gameObject.SetActive(true);
             
                //Activate the ragdoll
                ragdollController.RagdollModeOn();
                //Apply force and torque to the motor
                motorBikeController.Interact();

            }
        }

      
    }
}
