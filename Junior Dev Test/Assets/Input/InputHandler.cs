using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        public bool isScrollActive = false;
        public Vector2 scroll = new Vector2(0, 0);
        public float zoom = 0;
    
        public void OnActiveScroll(InputValue value)
        {
            isScrollActive = value.Get<float>() != 0;
        }

        public void OnZoom(InputValue value)
        {
            zoom = value.Get<float>();
        }

        public void OnScroll(InputValue value)
        {
            scroll = value.Get<Vector2>();
        }
    }
}

