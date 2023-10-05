using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float speed = 500f;
    private float maxLifetime = 2.0f;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 direction) {
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(this.gameObject);
    }
}
