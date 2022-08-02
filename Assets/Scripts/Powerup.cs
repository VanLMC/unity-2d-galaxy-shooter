using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupId = 0;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other){
            if(other.tag != "Player") return;
            Player player = other.GetComponent<Player>();
            if(player == null) return;
            
            if(powerupId == 0){
                player.TripleShotPowerupOn();
            }
            else if (powerupId == 1) {
              player.SppedBoostPowerupOn();
            }
            else if (powerupId == 2) {
                //enable shields
            }
     
     
            Destroy(this.gameObject);
    }
}
