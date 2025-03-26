

public class PowerUpManager : Singleton<PowerUpManager>
{
    void OnEnable()
    {
        BingoCage.OnBallDrawn += PowerUp;
    }
    void OnDisable()
    {

    }

    void PowerUp(int ballNumber)
    {

    }

    //

}
