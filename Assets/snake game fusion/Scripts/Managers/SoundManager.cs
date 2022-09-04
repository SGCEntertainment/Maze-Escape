using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance => Singleton<SoundManager>.Instance;

    [SerializeField] AudioSource source;

    [Space(10)]
    [SerializeField] AudioClip plantClip;

    public void PlaySound(int i)
    {
        AudioClip clip = i switch
        {
            0 => plantClip
        };

        if(source.isPlaying)
        {
            source.Stop();
        }

        source.PlayOneShot(clip);
    }
}
