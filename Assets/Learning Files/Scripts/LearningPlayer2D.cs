using Fusion;

public class LearningPlayer2D : NetworkBehaviour
{
    private LerningNetworkCharacterController2D ncc2d;
    public float Speed;

    private void Awake()
    {
        ncc2d = GetComponent<LerningNetworkCharacterController2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out LearningNetworkInputData data))
        {
            data.direction.Normalize();
            ncc2d.Move(Runner.DeltaTime * Speed * data.direction);
        }
    }
}
