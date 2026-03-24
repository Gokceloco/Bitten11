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
}
