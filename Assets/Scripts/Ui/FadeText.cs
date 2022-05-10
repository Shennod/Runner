using UnityEngine;
using DG.Tweening;
using TMPro;

public class FadeText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        _text.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
    }
}