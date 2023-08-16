using System.Collections;
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
    private Coroutine _spawning;

    private void Awake()
    {
        _birdPrefab = Resources.Load<GameObject>("Prefabs/Bird");
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
