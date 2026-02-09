using UIToolkit.Tooltip.Example.UI.Buttons.Data.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips.Data.Base;
using UnityEngine;

namespace UIToolkit.Tooltip.Example.UI.Main.Toolbar.Tools.Base
{
    public interface ITool
    {
        void Apply(Vector3 position);
    
        void Select(ButtonData data);
        
        void Clear();
    
        (ITooltipData data, bool cached) CreateTooltipData();
    }
}