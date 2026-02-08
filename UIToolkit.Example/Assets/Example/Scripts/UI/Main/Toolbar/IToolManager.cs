using System;
using UIToolkit.Tooltip.Example.UI.Buttons.Data.Base;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar.Enums;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar.Tools.Base;

namespace UIToolkit.Tooltip.Example.UI.Main.Toolbar
{
    public interface IToolManager
    {
        ITool ActiveTool { get; }
    
        event Action<ITool> OnActiveToolChanged;

        void SelectTool(ToolType type, ButtonData data = null);
    
        void CleanActiveTool();
    }
}