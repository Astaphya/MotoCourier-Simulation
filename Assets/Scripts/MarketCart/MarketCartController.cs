using System;
using DG.Tweening;
using Input;
using Interfaces;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace MarketCart
{
    public class MarketCartController : Vehicle,IMovable
    {
        [SerializeField] private float minRotationValue;
        [SerializeField] private float maxRotationValue;
        [SerializeField] private TrailRenderer[] skidTrails;
        private GameInput gameInput; 
        //private float skidThreshold = 0.25f;

     [SerializeField] private float moveDistance = 0.1f;
     [SerializeField] private float cartHeight = 0.2f;
     [SerializeField] private float cartRadius = 1f;
     [SerializeField] private LayerMask mask;


     public bool canMove;

     private bool isMovingToWall;

        private void Awake()
        {
            canMove = true;
            rb = GetComponent<Rigidbody>();
            gameInput = GetComponent<GameInput>();
        }

       

        private void FixedUpdate()
        {
            Move();
        }

        public void CanMoveSetter(bool canMoveParam)
        {
            canMove = canMoveParam;
        }

        public void SetMovementVector()
        {
            rb.velocity = Vector3.zero;
        }

        public void Move()
        {
           
            var movement = gameInput.GetMovementVector();
            isMovingToWall = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * cartHeight,cartRadius, movement,moveDistance,mask);
            if (!canMove || !isMovingToWall)
            {
                rigidbodyConstraints = RigidbodyConstraints.FreezeAll;
                rb.constraints = rigidbodyConstraints;
                return;
            }
            
            if (ExtensionMethods.CheckMovementValue(movement))
            {
                rigidbodyConstraints =  RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                rb.constraints = rigidbodyConstraints;
                return;
            }
            movement *= speed * Time.deltaTime;

            HandleSkids();
            rigidbodyConstraints = RigidbodyConstraints.None;
            rigidbodyConstraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
            rb.constraints = rigidbodyConstraints;
            
           
            rotationRatio = rotationCurve.Evaluate(movement.sqrMagnitude) * rotationSpeed * Time.deltaTime;
            rotationRatio = Mathf.Clamp(rotationRatio, minRotationValue, maxRotationValue);
            var lookRotation = Quaternion.LookRotation(rb.velocity,Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, rotationRatio);
            rb.velocity = movement ;
        }
        
        
        //Activate trails
        private void HandleSkids()
        {
          //  var angular = Mathf.Abs(rb.angularVelocity.y);
            // If angular velocity is greater than the skid treshold then emit the skid.
           // var emit = angular > skidThreshold;
            for (var i = 0; i < 2; i++) skidTrails[i].emitting = true;
        }
        
       
    }
}
