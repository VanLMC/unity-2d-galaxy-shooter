using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupId = 0;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other){
            if(other.tag != "Player") return;
            Player player = other.GetComponent<Player>();

            if(player == null) return;

            switch(_powerupId) 
            {
            case 0:
                player.TripleShotPowerupOn();
                break;
            case 1:
                 player.SppedBoostPowerupOn();
                break;
            case 2:
                player.ShieldPowerupOn();
                break;
            }
     
            Destroy(this.gameObject);
    }
}
