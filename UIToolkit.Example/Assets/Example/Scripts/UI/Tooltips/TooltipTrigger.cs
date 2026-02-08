using System;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Tooltips
{
    public class TooltipTrigger : IDisposable
    {
        private readonly VisualElement targetElement;
        private readonly ITooltipData tooltipData;
        private readonly TooltipConfig config;

        public TooltipTrigger(VisualElement element, ITooltipData data, TooltipConfig? config = null)
        {
            targetElement = element;
            tooltipData = data;
            this.config = config ?? TooltipConfig.Default;

            targetElement.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
            targetElement.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
        }

        private void OnMouseEnter(MouseEnterEvent evt)
        {
            TooltipService.Instance.ShowAtUIElement(tooltipData, targetElement, config);
            TooltipService.Instance.MarkForRedraw();
        }

        private void OnMouseLeave(MouseLeaveEvent evt)
        {
            TooltipService.Instance.Hide();
        }
    
        public void Dispose()
        {
            targetElement.UnregisterCallback<MouseEnterEvent>(OnMouseEnter);
            targetElement.UnregisterCallback<MouseLeaveEvent>(OnMouseLeave);
        }
    }
}