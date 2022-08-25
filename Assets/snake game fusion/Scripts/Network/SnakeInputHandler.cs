using UnityEngine;

public class SnakeInputHandler : MonoBehaviour
{
    [HideInInspector]
    public bool isHold;

    Vector3 offset;

    [HideInInspector]
    public Vector2 direction;

    private void Awake()
    {
        direction = Vector3.zero;
    }

    void OnMouseDown()
    {
        isHold = true;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePosition - transform.position;
    }

    private void OnMouseUp()
    {
        direction = Vector3.zero;
    }

    void OnMouseDrag()
    {
        if(!isHold)
        {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData
        {
            direction = direction
        };

        return networkInputData;
    }
}
