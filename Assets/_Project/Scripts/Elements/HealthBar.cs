using DG.Tweening;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform pivotTransform;

    public void SetFillBar(float ratio)
    {
        pivotTransform.DOKill();
        pivotTransform.DOScaleX(ratio, .2f);
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
