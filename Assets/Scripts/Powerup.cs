using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupId = 0;


   [SerializeField]
    private AudioClip _audioClip;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -6.9f){
           Destroy(this.gameObject);
        }
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
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
    }

}
