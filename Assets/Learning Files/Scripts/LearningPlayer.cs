using Fusion;

public class LearningPlayer : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype _cc;
    public float Speed;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
    }

    //public override void FixedUpdateNetwork()
    //{
    //    if (GetInput(out LearningNetworkInputData data))
    //    {
    //        data.direction.Normalize();
    //        _cc.Move(Runner.DeltaTime * Speed * data.direction);
    //    }
    //}
}
