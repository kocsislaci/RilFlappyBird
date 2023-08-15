using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isRandom = true;
    [SerializeField] private SpawnController _spawnController;
    private GameObject birdPrefab;
    private Coroutine spawning;
    
    private void Awake()
    {
        birdPrefab = Resources.Load<GameObject>("Prefabs/Bird");
    }

    private void Start()
    {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        bool isDelayed = false;
        if (!isDelayed)
        {
            isDelayed = true;
            yield return new WaitForSeconds(1);
        }
        var bird = Instantiate(birdPrefab, Vector3.zero, quaternion.identity);
        bird.GetComponent<BirdController>().die.AddListener(GameOver);
        spawning = StartCoroutine(Spawn());
        yield return null;
    }

    
    IEnumerator Spawn()
    {
        while (true)
        {
            if (isRandom)
            {
                _spawnController.SpawnAtRandom();
            }
            else
            {
                _spawnController.spawnAtDefault();
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void GameOver()
    {
        StopCoroutine(spawning);
        Debug.Log("Game over");
        Application.Quit();
    }
}
