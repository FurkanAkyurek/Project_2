using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    public static FinishArea Instance;

    public Transform finishRef;

    private void Awake()
    {
        Instance = this;
    }
}
