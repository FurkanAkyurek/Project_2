using DG.Tweening;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private float bounceTime = 1.5f;

    public ParticleSystem particle;

    private void Start()
    {
        gameObject.transform.DOMoveY(transform.position.y + 2f, bounceTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InBounce);
    }
}
