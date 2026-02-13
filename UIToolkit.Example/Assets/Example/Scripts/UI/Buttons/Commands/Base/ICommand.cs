namespace UIToolkit.Tooltip.Example.UI.Buttons.Commands.Base
{
    public interface ICommand
    {
        void Execute();
        bool CanExecute();
    }
}