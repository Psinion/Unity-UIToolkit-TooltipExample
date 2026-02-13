using UIToolkit.Tooltip.Example.UI.Buttons.Commands.Base;
using UIToolkit.Tooltip.Example.UI.Buttons.Data.Base;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar;
using UIToolkit.Tooltip.Example.UI.Main.Toolbar.Enums;

namespace UIToolkit.Tooltip.Example.UI.Buttons.Commands
{
    public class SpawnCommand : ICommand
    {
        private readonly IToolManager toolManager;
        private readonly ButtonData buttonData;
    
        public SpawnCommand(IToolManager toolManager, ButtonData buttonData)
        {
            this.toolManager = toolManager;
            this.buttonData = buttonData;
        }
    
        public void Execute()
        {
            toolManager.SelectTool(ToolType.Spawn, buttonData);
        }
    
        public bool CanExecute()
        {
            return true;
        }
    }
}