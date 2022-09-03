using UnityEngine;
using Fusion;

public class XMakerController : NetworkBehaviour
{
    Transform parent;
    [SerializeField] GameObject xPrefab;

    [Space(10)]
    [SerializeField] float fireRate = 0.5f;
    float nextFire = 0.0f;

    private void Start()
    {
        parent = GameObject.Find("marks").transform;
    }

    private void Update()
    {
        if (!Object.HasInputAuthority)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(xPrefab, transform.position, transform.rotation, parent);
        }
    }
}
