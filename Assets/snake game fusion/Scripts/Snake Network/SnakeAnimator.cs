using UnityEngine;

public class SnakeAnimator : SnakeComponent
{
    int walkID;
    SnakeNetworkInput Inputs;

    [SerializeField] private Animator _animator;

    public override void Spawned()
    {
        walkID = Animator.StringToHash("speed");
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (GetInput(out SnakeNetworkInput input) && Runner.IsForward)
        {
            Inputs = input;
        }

        PlayAnim(Inputs);
    }

    private void PlayAnim(SnakeNetworkInput inputs)
    {
        _animator.SetFloat(walkID, inputs.inputDirection.sqrMagnitude);
    }
}
