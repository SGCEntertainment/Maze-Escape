using Fusion;

public class LearningPlayer2D : NetworkBehaviour
{
    private NetworkRigidbody2D nr2d;
    public float Speed;

    private void Awake()
    {
        nr2d = GetComponent<NetworkRigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out LearningNetworkInputData data))
        {
            data.direction2D.Normalize();
            nr2d.Rigidbody.MovePosition(nr2d.Rigidbody.position + Runner.DeltaTime * Speed * data.direction2D);
        }
    }
}
