using Fusion;
using UnityEngine;

public struct LearningNetworkInputData : INetworkInput
{
    //public Vector3 direction;
    //public Vector2 direction2D;

    public const int BUTTON_FORWARD = 3;
    public const int BUTTON_BACKWARD = 4;
    public const int BUTTON_LEFT = 5;
    public const int BUTTON_RIGHT = 6;

    public NetworkButtons Buttons;

    public bool IsUp(int button)
    {
        return Buttons.IsSet(button) == false;
    }

    public bool IsDown(int button)
    {
        return Buttons.IsSet(button);
    }
}
