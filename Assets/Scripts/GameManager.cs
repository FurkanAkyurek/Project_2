using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action<int> OnComboChanged;

    [SerializeField] private CubeSpawner[] spawners;

    private int spawnerIndex;

    private CubeSpawner currentSpawner;

    public GameState State = GameState.Initial;

    [HideInInspector] public int combo = 0;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] comboSounds;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;

        OnComboChanged += PlayComboSound;
    }

    private void Update()
    {
        if(State == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (MovingCube.CurrentCube != null)
                {
                    MovingCube.CurrentCube.Stop();
                }

                if (spawners.Length > spawnerIndex)
                {
                    currentSpawner = spawners[spawnerIndex];

                    currentSpawner.SpawnCube();

                    spawnerIndex++;
                }
            }
        }
    }

    public void PlayComboSound(int combo)
    {
        audioSource.PlayOneShot(comboSounds[combo]);
    }

    public void Failed()
    {
        UIManager.Instance.LoseSequence();

        State = GameState.Fail;
    }
    public void Finish()
    {
        UIManager.Instance.WinSequence();

        State = GameState.Finish;
    }
}
public enum GameState
{
    Initial,
    Playing,
    Fail,
    Finish
}
