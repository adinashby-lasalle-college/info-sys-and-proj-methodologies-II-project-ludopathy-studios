using System.Collections.Generic;

public class PowerUpManager : Singleton<PowerUpManager>
{
    Queue<ICommand> powerUpsQueue = new Queue<ICommand>();
    RemoteControl remote;

    protected override void Awake()
    {
        base.Awake();
        remote = new RemoteControl();
    }

    void OnEnable()
    {
        Cell.OnCellMarked += OnCellMarked;
    }
    void OnDisable()
    {
        Cell.OnCellMarked -= OnCellMarked;

    }

    private void OnCellMarked(Cell cell)
    {
        // Queue is empty apply power

        CellPowerUpManager cellPowers = cell.GetComponent<CellPowerUpManager>();
        if (cellPowers.activePowers.Count > 0)
        {
            foreach (PowerUp power in cellPowers.activePowers)
            {
                powerUpsQueue.Enqueue(new PowerUpOnCommand(power));
            }

            cellPowers.activePowers.Clear();

            ApplyPowers();
        }
    }

    private void ApplyPowers()
    {
        while (powerUpsQueue.Count > 0)
        {
            ICommand command = powerUpsQueue.Dequeue();
            remote.SetCommand(command);
            remote.PressButton();
        }
    }

}

// Command Pattern
public interface ICommand
{
    void Execute();
}

public class PowerUpOnCommand : ICommand
{
    private PowerUp _powerUp;

    public PowerUpOnCommand(PowerUp powerUp)
    {
        _powerUp = powerUp;
    }

    public void Execute()
    {
        _powerUp.ApplyPower();
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