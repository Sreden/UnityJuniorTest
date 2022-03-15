using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        public bool isScrollActive = false;
        public Vector2 scroll = new Vector2(0, 0);
        public float zoom = 0;
        public bool isBuilding = false;
        public Vector2 aim = new Vector2(0,0);
        public GameObject selectedTower;
        [SerializeField] private GameObject[] towers;

        private void Start()
        {
            OnSelectDamageTower();
        }

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
        
        public void OnSwitchMode()
        {
            isBuilding = !isBuilding;
        }

        public void OnAim(InputValue value)
        {
            aim = value.Get<Vector2>();
        }

        public void OnBuild()
        {
            if (Camera.main != null) Camera.main.GetComponent<CameraBehavior>().Build();
        }

        public void OnSelectDamageTower()
        {
            selectedTower = towers[0];
        }  
        
        public void OnSelectWarriorTower()
        {
            selectedTower = towers[1];
        }
    }
}

