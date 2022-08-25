using UnityEngine;

public class SnakeInputHandler : MonoBehaviour
{
    [HideInInspector]
    public bool isHold;

    Vector3 offset;

    [HideInInspector]
    public Vector2 newPosition;

    private void Awake()
    {
        newPosition = NetworkSpawnSnake.Instance.spawnPoint.position;
    }

    void OnMouseDown()
    {
        isHold = true;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePosition;
    }

    void OnMouseDrag()
    {
        if(!isHold)
        {
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition = offset + mousePosition;
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData
        {
            newPosition = newPosition
        };

        return networkInputData;
    }
}
