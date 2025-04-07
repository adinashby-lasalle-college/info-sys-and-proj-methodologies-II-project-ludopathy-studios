
[System.Serializable]
public class Ball
{
    public readonly int Number;
    private bool IsCalled = false;

    public Ball(int number)
    {
        Number = number;
    }

    public void ToggleBall()
    {
        IsCalled = !IsCalled;
    }
}
