using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController: MonoBehaviour
{
    [SerializeField] private Vector2 spawnPoint;

    [SerializeField] private float randomScale = 0.5f;
    [SerializeField] private float velocity = 1f;
    [SerializeField] private float gap = 2f;
    [SerializeField] private float heightOffset = 0f;

    private GameObject pipePrefab;

    private void Awake()
    {
        pipePrefab = Resources.Load<GameObject>("Prefabs/Pipe");
    }

    public void spawnAtDefault()
    {
        var pipe = Instantiate(pipePrefab, spawnPoint, new Quaternion());
        var pipeController = pipe.GetComponent<PipeController>();
        pipeController.SetPipeConfiguration(heightOffset, gap);
        pipeController.StartMovement(velocity);
    }

    public void SpawnAtRandom()
    {
        var pipe = Instantiate(pipePrefab, spawnPoint, new Quaternion());
        var pipeController = pipe.GetComponent<PipeController>();
        pipeController.SetPipeConfiguration(heightOffset + (Random.value - 0.5f) * randomScale, gap);
        pipeController.StartMovement(velocity);
    }
}
