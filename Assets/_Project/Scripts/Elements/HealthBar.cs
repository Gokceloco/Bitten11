using DG.Tweening;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform pivotTransform;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetFillBar(float ratio)
    {
        pivotTransform.DOKill();
        pivotTransform.DOScaleX(ratio, .2f);

        if (ratio <= 0 || ratio == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        pivotTransform.DOKill();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Vector3.back + Vector3.up);
    }
}
