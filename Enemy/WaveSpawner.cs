using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { Spawning, Waiting, Counting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5;
    public float waveCountdown = 0;

    private SpawnState state = SpawnState.Counting;
    private Vector2 screenBounds;

    void Start()
    {
       
    }

    private void Update()
    {
        if (state == SpawnState.Waiting)
        {
            BeginNewRound();
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1 / _wave.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void BeginNewRound()
    {
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        //All waves complete - this currently just repeats the waves.
        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    void SpawnEnemy (Transform _enemy)
    {
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        Vector3 spawnLocation;

        Vector2 screenBL = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector2 screenBR = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Vector2 screenTR = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector2 screenTL = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));

        switch (Random.Range(0, 4))
        {
            //above
            case 0:
                spawnLocation = new Vector3(Random.Range(screenBL.x, screenBR.x), screenBL.y - 1, 0);
                break;

            //below
            case 1:
                spawnLocation = new Vector3(Random.Range(screenTL.x, screenTR.x), screenTL.y + 1, 0);
                break;

            //left
            case 2:
                spawnLocation = new Vector3(screenBL.x - 1, Random.Range(screenBL.y, screenTL.y), 0);
                break;

            //right
            case 3:
                spawnLocation = new Vector3(screenBR.x + 1, Random.Range(screenBR.y, screenTR.y), 0);
                break;

            default:
                spawnLocation = new Vector3(screenBR.x - 1, Random.Range(screenBR.y, screenTR.y), 0);
                break;
        }

        //Debug.Log(screenBounds);
        //float x = Random.Range(screenBounds.x + 1, screenBounds.x + 2);
        //float y = Random.Range(screenBounds.y + 1, screenBounds.y + 2);
        //Vector3 spawnLocation = new Vector3(x * (Random.Range(0, 2) * 2 - 1), y * (Random.Range(0, 2) * 2 - 1), 0);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        Instantiate(_enemy, spawnLocation, rotation);
    }
}
