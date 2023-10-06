using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] ParticleSystem explosion;

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
            Explode();
        }
    }

    private void FreezeControls() {
        _thrusting = false;
        _frozen = true;
    }

    private void Explode() {
        StartCoroutine(FlashRed());
        Instantiate(this.explosion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject, 4.0f);
    }

    private IEnumerator FlashRed() {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        renderer.color = Color.red;

        while (true) {
            yield return new WaitForSeconds(0.25f);
            renderer.color = Color.white;

            yield return new WaitForSeconds(0.25f);
            renderer.color = Color.red;
        }

    }

}
