using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] float minSpawnInterval = 0.8f;
    [SerializeField] float maxSpawnInterval = 2f;

    [SerializeField] float minSpeed = 10f;
    [SerializeField] float maxSpeed = 20f;

    float episodeSpeed;

    JumperAgent agent;

    public float ObstacleSpeed { get; set; }

    public bool IsRunning { get; set; } = true;
    public int SpawnCount { get; private set; }

    void Start()
    {
        agent = transform.parent.Find("Agent").GetComponent<JumperAgent>();
        PickEpisodeSpeed();
        StartCoroutine(DoCheck());
    }

    private void PickEpisodeSpeed()
    {
        episodeSpeed = Random.Range(minSpeed, maxSpeed);
    }

    public void ResetSpawner()
    {
        SpawnCount = 0;
        PickEpisodeSpeed();
    }

    IEnumerator DoCheck()
    {
        while(true)
        {
            if (IsRunning)
                SpawnObstacle();
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    private void SpawnObstacle()
    {
        var obstacle = Instantiate(Resources.Load<Obstacle>("Obstacle"));
        obstacle.Speed = episodeSpeed;
        //devilspawn.transform.localPosition = Vector3.zero;
        obstacle.transform.parent = transform;
        obstacle.transform.localPosition = Vector3.zero;
        SpawnCount++;
        agent.CheckEpisodeDuration(SpawnCount);
    }
}
