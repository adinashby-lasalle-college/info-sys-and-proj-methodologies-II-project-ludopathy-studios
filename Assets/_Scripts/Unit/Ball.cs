using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball
{
    public int Number { get; private set; }
    public bool IsCalled { get; set; }

    public Ball(int number)
    {
        Number = number;
    }
}
