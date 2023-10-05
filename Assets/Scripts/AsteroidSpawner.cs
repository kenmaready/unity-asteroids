using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private float spawnRate = 2.0f;
    [SerializeField] private int spawnAmount = 1;
    [SerializeField] private float spawnDistance = 15.0f;
    [SerializeField] private float trajectoryVariance = 15.0f;

    private void Start() {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn() {
        for (int i = 0; i < this.spawnAmount; i++) {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPOS = this.transform.position + spawnDirection;
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPOS, rotation);

            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.Launch(rotation * -spawnDirection);
        }
    }
}
