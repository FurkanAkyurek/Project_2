using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;

    [SerializeField] private float rotationTime = 3f;

    public ParticleSystem particle;

    private void Start()
    {
        gameObject.transform.DOLocalRotate(rotationVector, rotationTime, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
}
