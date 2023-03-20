using System.Collections;
using System.Collections.Generic;
using Input;
using Interfaces;
using UnityEngine;

namespace Motor
{
    public class MotorBikeController : Vehicle,IMovable,IInteractable
    {
        public  bool isColided;

      //  [SerializeField] private bool isRaining;
      //  [SerializeField] private float rainTraction;
        [SerializeField] private ParticleSystem smokeVFX;
     //   [SerializeField] private Animator motorAnimator;
        
      //  private float initializedDrag;
      //  private float initializedAngularDrag;
      
        private GameInput gameInput;
        private float crashForce = 3f;
        


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            gameInput = GetComponent<GameInput>();
        }
        
        /*
        private void Start()
        {
            initializedDrag = rb.drag;
            initializedAngularDrag = rb.angularDrag;
        }
        */
    

        private void FixedUpdate()
        {
            Move();
        }
        

        public void Move()
        {
            var movement = gameInput.GetMovementVector();
            movement *= speed * Time.deltaTime;
            /*
            if (isRaining)
            {
                rb.drag = rainTraction / 6f;
                rb.angularDrag = rainTraction;
                rotationSpeed = 3f;

            }
            else
            {
                rb.drag = initializedDrag;
                rb.angularDrag = initializedAngularDrag;
            }
            */
            
            //If it crashed something
            if (isColided)
            {
                ChangeSmokeVFXState(false);
               // ControlAnimator(false);
                return;
            }

            if (ExtensionMethods.CheckMovementValue(movement))
            {
               rigidbodyConstraints =  RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
               rb.constraints = rigidbodyConstraints;
               ChangeSmokeVFXState(false);
              // ControlAnimator(false);
               return;
            }

          //  ControlAnimator(true);
            rigidbodyConstraints = RigidbodyConstraints.None;
            rigidbodyConstraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.constraints = rigidbodyConstraints;
            
            ChangeSmokeVFXState(true);

            rotationRatio = rotationCurve.Evaluate(movement.sqrMagnitude) * rotationSpeed * Time.deltaTime;
            var lookRotation = Quaternion.LookRotation(rb.velocity,Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, rotationRatio);
            rb.velocity = movement;

        }
        
        private void ChangeSmokeVFXState(bool smokeState)
        {
            if(smokeState)
                smokeVFX.Play();
            else
                smokeVFX.Stop();
        }
        
        /*
        private void ControlAnimator(bool isEnabled)
        {
            motorAnimator.enabled = isEnabled;
        }
        */
        
        private void AddCrashForce()
        {
            isColided = true;
            rb.constraints = RigidbodyConstraints.None;
            var oppositeForce = rb.velocity;
            rb.AddForce(-oppositeForce  * crashForce, ForceMode.VelocityChange);
            rb.AddTorque(-oppositeForce.normalized  * crashForce * 4f,ForceMode.VelocityChange);
        }

        public void Interact(bool isIncrementing)
        {
            throw new System.NotImplementedException();
        }

        public void Interact()
        {
            AddCrashForce();
        }
    }
}


