using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UnityEngine.UIElements;

namespace UIToolkit.Tooltip.Example.UI.Tooltips.Instances.Base
{
    public interface ITooltipInstance
    {
        void Bind(ITooltipData data);
        void Integrate(VisualElement tooltipContainer);
        void Hide();
    }
}