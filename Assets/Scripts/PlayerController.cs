using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<AnimationState> OnStateChanged;

    [SerializeField] private float speed = 3.0f;

    [SerializeField] private float lerpSpeed = 0.1f;

    public Transform target;

    private Animator animator;

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;

        OnStateChanged += SetAnimation;

        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, target.position, lerpSpeed), speed * Time.deltaTime);

            if(Vector3.Distance(transform.position,target.position) < 0.2f)
            {
                OnStateChanged?.Invoke(AnimationState.Idle);

                target = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.Instance.OnCoinCollected?.Invoke(1);

            Coin coin = other.GetComponent<Coin>();

            coin.particle.Play();

            coin.gameObject.GetComponent<MeshFilter>().mesh = null;

            Destroy(coin.gameObject, 1.0f);
        }
        if (other.CompareTag("Diamond"))
        {
            Diamond diamond = other.GetComponent<Diamond>();

            diamond.particle.Play();

            diamond.gameObject.GetComponent<MeshFilter>().mesh = null;

            GameManager.Instance.Finish();

            OnStateChanged?.Invoke(AnimationState.Dance);

            Destroy(diamond.gameObject, 1.0f);
        }
        if (other.CompareTag("Finish"))
        {
            FinishArea finish = other.GetComponent<FinishArea>();

            target = finish.finishRef;
        }
    }

    private void SetAnimation(AnimationState animState)
    {
        switch (animState)
        {
            case AnimationState.Idle:
                animator.SetBool("Idle", true);
                animator.SetBool("Run", false);
                break;
            case AnimationState.Run:
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
                break;
            case AnimationState.Dance:
                animator.SetBool("Run", false);
                animator.SetBool("Dance", true);
                break;
            default:
                break;
        }
    }
}
public enum AnimationState
{
    Idle,
    Run,
    Dance
}
