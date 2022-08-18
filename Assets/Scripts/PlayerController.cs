using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;

    [SerializeField] private float lerpSpeed = 0.1f;

    public Transform target;

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, target.position, lerpSpeed), speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {

        }
        if (other.CompareTag("Diamond"))
        {

        }
    }
}
