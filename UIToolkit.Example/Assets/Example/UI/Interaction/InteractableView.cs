using UIToolkit.Tooltip.Example.UI.Tooltips.Data;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UnityEngine;

namespace UIToolkit.Tooltip.Example.UI.Interaction
{
    public class InteractableView : MonoBehaviour
    {
        [SerializeField] 
        private string objName;
        
        public ITooltipData GetTooltip()
        {
            return new TextTooltipData(objName);
        }

        private void Start()
        {
            StartInternal();
        }

        protected virtual void StartInternal()
        {
        }
    }
}