using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem impactPSPrefab;

    public void PlayImpactPS(Vector3 pos, Vector3 normal, Color color)
    {
        var newPS = Instantiate(impactPSPrefab);
        newPS.transform.position = pos;
        newPS.transform.LookAt(pos + normal);

        var main = newPS.main;
        main.startColor = color;

        newPS.Play();
    }
}
