using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    private static CameraFollowing instance;
    public static CameraFollowing Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<CameraFollowing>();
            }

            return instance;
        }
    }

    Vector3 velocity = Vector3.zero;
    [SerializeField] Vector3 offset;

    [Space(10)]
    [SerializeField] float smoothTime;

    private void LateUpdate()
    {
        if(!RoomPlayer.Local)
        {
            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position, RoomPlayer.Local.Snake.transform.position + offset, ref velocity, smoothTime);
    }
}
