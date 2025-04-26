using UnityEngine;

public interface ICommand
{
    void Execute();
}
public class PowerUpCommand
{
    private CellPowerUpManager _powerManager;

    public PowerUpCommand(CellPowerUpManager powerManager)
    {
        _powerManager = powerManager;
    }

    public void Execute()
    {
        _powerManager?.TriggerPowers(_powerManager.GetComponent<Cell>());
    }
}

public class RemoteControl
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
    }

}
