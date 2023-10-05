using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private Bullet bulletPrefab;

    private Rigidbody2D _rigidbody;
    private BulletSpawnPoint _bulletSpawnPoint;
    private float thrustSpeed = 2.0f;
    private float turnSpeed = .15f;
    private bool _thrusting;
    private float _turnDirection;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>();
    }

    private void Update() {
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

}
