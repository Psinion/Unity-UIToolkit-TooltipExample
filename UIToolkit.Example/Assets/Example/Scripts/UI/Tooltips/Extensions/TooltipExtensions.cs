using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Tooltips.Extensions
{
    public static class TooltipExtensions
    {
        public static TooltipTrigger AddTooltip(this VisualElement element, ITooltipData tooltipData, TooltipConfig? config = null)
        {
            return new TooltipTrigger(element, tooltipData, config);
        }
    }
}