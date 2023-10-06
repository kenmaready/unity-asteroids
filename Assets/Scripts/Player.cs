using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private Bullet bulletPrefab;
    private ParticleSystem explosion;

    private GameManager gm;

    private Rigidbody2D _rigidbody;
    private BulletSpawnPoint _bulletSpawnPoint;
    private float thrustSpeed = 2.0f;
    private float turnSpeed = .15f;
    private bool _thrusting;
    private float _turnDirection;
    private bool _frozen = false;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>();
        explosion = GetComponentInChildren<ParticleSystem>();

        gm = FindObjectOfType<GameManager>();
        if (gm == null) {
            Debug.LogError("No GameManager Object could be found by Player.");
        }
    }

    private void Update() {
        if (_frozen) return;

        _thrusting = (Input.GetKey(KeyCode.UpArrow));

        if (Input.GetKey(KeyCode.LeftArrow)) {
            _turnDirection = -1.0f;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            _turnDirection = 1.0f;
        } else {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) Fire();
    }

    private void FixedUpdate() {
        if (_thrusting) {
            _rigidbody.AddForce(this.transform.up * thrustSpeed);
        }

        if (_turnDirection != 0.0f) {
            _rigidbody.AddTorque(-_turnDirection * turnSpeed);
        }
    }

    private void Fire() {
        Bullet bullet = Instantiate(this.bulletPrefab, _bulletSpawnPoint.transform.position, this.transform.rotation);
        bullet.Fire(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Asteroid") {
            FreezeControls();
            StartCoroutine(PlayerDeathSequence());
        }
    }

    private void FreezeControls() {
        _thrusting = false;
        _frozen = true;
    }

    private IEnumerator PlayerDeathSequence() {
        explosion.Play();
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        renderer.color = Color.red;

        for (int i = 0; i < 8; i++) {
            yield return new WaitForSeconds(0.25f);
            renderer.color = Color.white;

            yield return new WaitForSeconds(0.25f);
            renderer.color = Color.red;
        }

        renderer.color = Color.white;
        gm.OnPlayerDeath();
    }

    public void Respawn() {
        StartCoroutine(SafeRespawnSequence());
        this.transform.position = Vector3.zero;
        this.transform.eulerAngles = Vector3.up;
        _frozen = false;
    }

    private IEnumerator SafeRespawnSequence() {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 6; i++) {
            renderer.color = Color.gray;
            yield return new WaitForSeconds(0.25f);
            
            renderer.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }

        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

}
