using System;
using System.Collections.Generic;
using UIToolkit.Tooltip.Example.Core;
using UIToolkit.Tooltip.Example.Gameplay;
using UIToolkit.Tooltip.Example.UI.Buttons.Data.Base;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar.Enums;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar.Tools;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar.Tools.Base;
using UIToolkit.Tooltip.Example.UI.Tooltips;

namespace UIToolkit.Tooltip.Example.UI.Main.Toolbar
{
    public class ToolManager : SingletonMono<ToolManager>, IToolManager
    {
        private readonly Dictionary<ToolType, ITool> tools = new();
        
        private ITool activeTool;
        public ITool ActiveTool
        {
            get => activeTool;
            set
            {
                activeTool = value;
                OnActiveToolChanged?.Invoke(value);
            }
        }

        public event Action<ITool> OnActiveToolChanged;

        private void Start()
        {
            tools[ToolType.Spawn] = new SpawnTool(GameResourcesService.Instance);
        }
        
        public void SelectTool(ToolType type, ButtonData data = null)
        {
            activeTool = tools[type];
            activeTool.Select(data);
        }

        public void CleanActiveTool()
        {
            if (activeTool != null)
            {
                activeTool?.Cancel();
                ActiveTool = null;
                TooltipService.Instance.Hide();
            }
        }
    }
}