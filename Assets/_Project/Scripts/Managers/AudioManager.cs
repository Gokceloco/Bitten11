using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource shootAS;

    public void PlayShootAS()
    {
        shootAS.Play();
    }
}
