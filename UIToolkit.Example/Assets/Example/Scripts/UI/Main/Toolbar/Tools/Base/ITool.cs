using UIToolkit.Tooltip.Example.UI.Buttons.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UnityEngine;

namespace UIToolkit.Tooltip.Example.UI.Main.Toolbar.Tools.Base
{
    public interface ITool
    {
        void Apply(Vector2 position);
    
        void Select(ButtonData data);
        
        void Cancel();
    
        (ITooltipData data, bool cached) CreateTooltipData();
    }
}