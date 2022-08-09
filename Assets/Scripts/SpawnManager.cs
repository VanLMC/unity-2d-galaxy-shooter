using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerups;

    private float SCREEN_STARTING_POSITION = 6.69f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    private IEnumerator SpawnEnemyRoutine(){
        while(true) {
            float randomNumber = Random.Range(-8.51f, 8.5f);
            Instantiate(_enemyShipPrefab, new Vector3(randomNumber, SCREEN_STARTING_POSITION,  0), Quaternion.identity);
             yield return new WaitForSeconds(1f);
        }
    }


    private IEnumerator PowerupSpawnRoutine(){
        while(true) {
            int randomPowerUpNumber = Random.Range(0, 3);
            float randomXAxisPosition = Random.Range(-8.51f, 8.5f);
            Instantiate(_powerups[randomPowerUpNumber], new Vector3(randomXAxisPosition, SCREEN_STARTING_POSITION, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
