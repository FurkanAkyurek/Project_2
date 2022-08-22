using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static GameObject LastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    [SerializeField] private float perfectComboValue;

    [SerializeField] private float moveSpeed = 1f;

    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = FindObjectOfType<StartCube>().gameObject;
        }

        CurrentCube = this;

        GetComponent<Renderer>().material.color = GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    internal void Stop()
    {
        moveSpeed = 0f;

        float hangover = transform.position.x - LastCube.transform.position.x;

        if (Mathf.Abs(hangover) >= LastCube.transform.localScale.x)
        {
            LastCube = null;

            CurrentCube = null;

            GameManager.Instance.Failed();

            return;
        }

        if(Mathf.Abs(hangover) <= perfectComboValue)
        {
            GameManager.Instance.OnComboChanged?.Invoke(GameManager.Instance.combo);

            GameManager.Instance.combo++;
        }
        else
        {
            GameManager.Instance.combo = 0;

            GameManager.Instance.OnComboChanged?.Invoke(GameManager.Instance.combo);
        }

        float direction = hangover > 0 ? 1f : -1f;
        
        SplitCubeOnX(hangover, direction);

        LastCube = this.gameObject;

        PlayerController.Instance.target = LastCube.transform;
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);

        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = LastCube.transform.position.x + (hangover / 2);

        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);

        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);

        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockXPosition, float fallingBlockSize)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);

        cube.transform.position = new Vector3(fallingBlockXPosition, transform.position.y, transform.position.z);

        cube.AddComponent<Rigidbody>();

        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        Destroy(cube.gameObject, 1f);
    }

    private void Update()
    {
        if(MoveDirection == MoveDirection.Left)
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        }
        else
        {
            transform.position += transform.right * -1 * Time.deltaTime * moveSpeed;
        }
    }
}
