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

    private float _fireRate = 0.25f;
    private float _nextFireTime = 0.0f;

    private bool _canTripleShot = false;
    public bool _hasShield = false;

    private int _lives = 3;

    [SerializeField]
    private GameObject _shotPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _explosionAnimation;

    [SerializeField]
    private GameObject _shieldsGameObject;

    [SerializeField]
    private GameObject[] _engineFails;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(_uiManager == null) return;

        _uiManager.UpdateLives(_lives);

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        Movement();
        Shoot();
    }

    private void Shoot(){
        float currentTime = Time.time;
        bool canFire = currentTime >= _nextFireTime;
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)) && canFire){
            if(_canTripleShot){
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else {
                Instantiate(_shotPrefab, transform.position + new Vector3(0, 1.32f,0), Quaternion.identity); //Quaternion is used to represent rotation
            }
            _nextFireTime = Time.time + _fireRate;
            _audioSource.Play();
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
        if(_hasShield) {
            ShieldPowerupOff();
            return;
        }

        _lives--;
        ShowFailedEngine();
        if(_lives < 1){
            Die();
        }
        _uiManager.UpdateLives(_lives);
    }

    private void ShowFailedEngine(){
        int randomNumber = Random.Range(0, 2);
        GameObject randomEngine =_engineFails[randomNumber];
        bool isRandomEngineActive = randomEngine.activeSelf;
        Debug.Log(randomNumber);
        if(isRandomEngineActive){
            int newNumber = randomNumber == 0 ? 1 : 0;
            Debug.Log(newNumber);
            _engineFails[newNumber].SetActive(true);
            return;
        }
        randomEngine.SetActive(true);
    }

    public void Die(){
        Instantiate(_explosionAnimation, this.transform.position, Quaternion.identity);
        _gameManager.EndGame();
        Destroy(this.gameObject);
    }

    public void SppedBoostPowerupOn() {
        _speed += 5.0f;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void TripleShotPowerupOn() {
        _canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        _canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        _speed = 5.0f;
    }

    public void ShieldPowerupOn(){
        _hasShield = true;
        _shieldsGameObject.SetActive(true);
    }

    private void ShieldPowerupOff(){
        _hasShield = false;
        _shieldsGameObject.SetActive(false);
    }

}

