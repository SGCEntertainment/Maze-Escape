using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NetworkSpawnSnake.Instance.ShowResult(collision.gameObject);
    }
}
