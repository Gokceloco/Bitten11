using DG.Tweening;
using TMPro;
using UnityEngine;

public class ProgressionUI : MonoBehaviour
{
    public TextMeshProUGUI levelNoTMP;

    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Show(float delay)
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1, .2f).SetDelay(delay);
    }

    public void SetLevelNo(int levelNo)
    {
        levelNoTMP.text = "LEVEL " + levelNo;
    }

    public void Hide()
    {
        _canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }
}
