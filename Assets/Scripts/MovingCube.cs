using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    [SerializeField] private float moveSpeed = 1f;

    private void OnEnable()
    {
        if(LastCube == null)
        {
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>();

            LastCube.GetComponent<Renderer>().material.color = GetRandomColor();
        }

        if (gameObject.CompareTag("MovingCube"))
        {
            CurrentCube = this;

            GetComponent<Renderer>().material.color = GetRandomColor();
        }
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    internal void Stop()
    {
        moveSpeed = 0f;

        float hangover = transform.position.x - LastCube.transform.position.x;

        float direction = hangover > 0 ? 1f : -1f;

        SplitCubeOnX(hangover, direction);
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

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = new Vector3(cubeEdge, transform.position.y, transform.position.z);

        sphere.transform.localScale = Vector3.one * 0.1f;

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
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }
}
