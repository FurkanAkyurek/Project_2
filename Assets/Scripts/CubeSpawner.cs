using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube cubePrefab;

    [SerializeField] private MoveDirection moveDirection;

    public void SpawnCube()
    {
        MovingCube cube = Instantiate(cubePrefab).GetComponent<MovingCube>();

        if(MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("StartCube"))
        {
            cube.transform.position = new Vector3(transform.position.x, MovingCube.LastCube.gameObject.transform.position.y, transform.position.z);
        }
        else
        {
            cube.transform.position = transform.position;
        }

        cube.MoveDirection = moveDirection;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}
public enum MoveDirection
{
    Left,
    Right
}