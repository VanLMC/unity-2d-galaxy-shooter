using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    private float _LEFT_HORIZONTAL_BOUNDARY = -9.5f;
    private float _RIGHT_HORIZONTAL_BOUNDARY = 9.5f;
    private float _TOP_VERTICAL_BOUNDARY = 4.31f;
    private float _BOTTOM_VERTICAL_BOUNDARY = -4.31f;

    private float fireRate = 0.25f;
    private float nextFireTime = 0.0f;

    public bool canTripleShot = false;

    public int lives = 3;

    [SerializeField]
    private GameObject shotPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;

    [SerializeField]
    private GameObject explosionAnimation;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update(){
        Movement();
        Shoot();

    }

    private void Shoot(){
        float currentTime = Time.time;
        bool canFire = currentTime >= nextFireTime;
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)) && canFire){
            if(canTripleShot){
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else {
                Instantiate(shotPrefab, transform.position + new Vector3(0, 1.32f,0), Quaternion.identity); //Quaternion is used to represent rotation
            }
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Movement(){
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

            if(transform.position.x >= _RIGHT_HORIZONTAL_BOUNDARY){
            transform.position = new Vector3(_LEFT_HORIZONTAL_BOUNDARY, transform.position.y, 0 );
            }
            else if(transform.position.x <= _LEFT_HORIZONTAL_BOUNDARY){
            transform.position = new Vector3(_RIGHT_HORIZONTAL_BOUNDARY, transform.position.y, 0 );
            }

            else if(transform.position.y >= _TOP_VERTICAL_BOUNDARY){
                transform.position = new Vector3(transform.position.x, _TOP_VERTICAL_BOUNDARY, 0);
            }
            else if(transform.position.y <= _BOTTOM_VERTICAL_BOUNDARY){
                transform.position = new Vector3(transform.position.x, _BOTTOM_VERTICAL_BOUNDARY, 0);
            }
    }

    public void Damage() {
        lives -= 1;
        if(lives < 1){
            Die();
        }
    }

    public void Die(){
        Instantiate(explosionAnimation, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void SppedBoostPowerupOn() {
        _speed += 5.0f;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void TripleShotPowerupOn() {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        _speed = 5.0f;
    }

}

