using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// This Class is responsible to
// - Start the game (delayed)
// - Listen to dying bird
// - Shut down the game
// - Spawn pipes

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isRandom = true;
    [SerializeField] private SpawnController spawnController;
    private GameObject _birdPrefab;
    private List<GameObject> _countPrefabs;
    private Coroutine _spawning;

    private void Awake()
    {
        _birdPrefab = Resources.Load<GameObject>("Prefabs/Bird");
        _countPrefabs = new List<GameObject>() {
            Resources.Load<GameObject>("Prefabs/3"),
            Resources.Load<GameObject>("Prefabs/2"),
            Resources.Load<GameObject>("Prefabs/1"),
        };
    }

    private void Start()
    {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        bool isDelayed = false;
        if (!isDelayed) {
            isDelayed = true;
            foreach (var counterPrefab in _countPrefabs) {
                var numberPrefab = Instantiate(counterPrefab, new Vector3(0, 0, -1), quaternion.identity);
                yield return new WaitForSeconds(1);
                numberPrefab.SetActive(false);
                Destroy(numberPrefab, 5);
            }
        }
        var bird = Instantiate(_birdPrefab, Vector3.zero, quaternion.identity);
        bird.GetComponent<BirdController>().die.AddListener(GameOver);
        _spawning = StartCoroutine(Spawn());
        yield return null;
    }


    IEnumerator Spawn()
    {
        while (true)
        {
            if (isRandom)
            {
                spawnController.SpawnAtRandom();
            }
            else
            {
                spawnController.SpawnAtDefault();
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void GameOver()
    {
        StopCoroutine(_spawning);
        Debug.Log("Game over");
        Application.Quit();
    }
}
