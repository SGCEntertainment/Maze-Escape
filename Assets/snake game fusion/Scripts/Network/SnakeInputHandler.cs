using UnityEngine;
using Fusion;

public class SnakeInputHandler : NetworkBehaviour
{
    [HideInInspector]
    public bool isHold;

    Vector3 offset;

    [HideInInspector]
    public Vector2 Velocity { get; set; }

    private void Awake()
    {
        Velocity = Vector3.zero;
    }

    void OnMouseDown()
    {
        isHold = true;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePosition - transform.position;
    }

    private void OnMouseUp()
    {
        Velocity = Vector3.zero;
    }

    void OnMouseDrag()
    {
        if(!isHold)
        {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Velocity = (mousePosition - transform.position).normalized;
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData
        {
            velocity = Velocity
        };

        return networkInputData;
    }
}
