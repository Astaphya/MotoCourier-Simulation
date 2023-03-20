using Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace Input
{
    public class GameInput : MonoBehaviour
    {
        [SerializeField] private VariableJoystick joystick;

        public Vector3 GetMovementVector()
        {
            var horizontal = joystick.Horizontal;
            var vertical = joystick.Vertical;
            var movement = new Vector3(horizontal, 0f, vertical);
            return movement;
        }
    }
}


