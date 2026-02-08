using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIToolkit.Tooltip.Example.UI.Interaction
{
    public class UILayerManager : MonoBehaviour
    {
        private readonly List<RaycastResult> raycastResults = new();
        private PointerEventData pointerData = null!;

        void Start()
        {
            pointerData = new PointerEventData(EventSystem.current);
        }

        public bool IsOverInterfaceUI(Vector3 mousePosition)
        {
            pointerData.position = mousePosition;
            raycastResults.Clear();
        
            EventSystem.current.RaycastAll(pointerData, raycastResults);
        
            foreach (var result in raycastResults)
            {
                var go = result.gameObject;
                if (go.CompareTag("InterfaceUI") || go.name.Contains("PanelSettings"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}