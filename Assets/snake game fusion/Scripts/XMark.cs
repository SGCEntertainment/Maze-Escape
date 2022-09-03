using UnityEngine;
using System.Collections;

public class XMark : MonoBehaviour
{
    SpriteRenderer myRend;
    [SerializeField] float lifeTime;

    private void Start()
    {
        myRend = GetComponent<SpriteRenderer>();
        StartCoroutine(nameof(Fade));
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(lifeTime);
        Color c = myRend.color;

        float et = 0.0f;
        float fadeTime = 5.0f;
        while(et < fadeTime)
        {
            c.a = Mathf.Lerp(1, 0, et / fadeTime);
            myRend.color = c;

            et += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
