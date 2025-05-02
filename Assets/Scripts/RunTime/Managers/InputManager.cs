using System;
using RunTime.Signals;
using UnityEngine;


namespace RunTime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Camera camera;

        #endregion
        #region Private Variables

        private bool _isReadyToClick = true;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onIsPlayerReadyToClick += OnIsPlayerReadyToClick;
        }

        private void OnIsPlayerReadyToClick(bool condition) => _isReadyToClick = condition;
       

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onIsPlayerReadyToClick -= OnIsPlayerReadyToClick;
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        void Update()
        {
            if (!_isReadyToClick) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                
                Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero); // Raycast atıyoruz

                if (hit.collider )
                {
                    
                    Vector2 hitPosition = hit.collider.transform.localPosition;
                    
                    InputSignals.Instance.onPlayerClickedToBlast?.Invoke(hitPosition);
                
                }
                else
                {
                    
                }
            }
        }
    }
    
}