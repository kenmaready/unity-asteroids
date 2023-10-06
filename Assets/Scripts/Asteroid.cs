using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private ParticleSystem asteroidExplosion;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 3.5f;
    public float maxLifetime = 40.0f;

    private int ScoreValue { get {
        switch(size) {
            case var _ when size < 0.5:
                return 400;
            case var _ when size < 0.8:
                return 300;
            case var _ when size < 1.1:
                return 200;
            case var _ when size < 1.4:
                return 100;
            default:
                return 50;
        }
    }}

    private void Awake() {
        gm = FindObjectOfType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Start() {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;
        _rigidbody.mass = this.size;
    }

    public void Launch(Vector2 direction) {
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player") {
            ParticleSystem explosion = Instantiate(this.asteroidExplosion, other.GetContact(0).point, Quaternion.identity);
            Destroy(explosion, 0.5f);
            gm.IncreaseScore(ScoreValue);
            if ((this.size * 0.5) >= (this.minSize * 0.66)) {
                SplitAsteroid();
            }

            Destroy(gameObject);
        }
    }

    private void SplitAsteroid() {
        for (int i = 0; i < 2; i++) {
            Vector3 position = this.transform.position;
            position += (Vector3)Random.insideUnitCircle * 5f;

            float variance = Random.Range(-10.0f, 10.0f);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            
            Asteroid newAsteroid = Instantiate(this, this.transform.position, this.transform.rotation);
            newAsteroid.size = this.size * 0.5f;

            newAsteroid.Launch(Random.insideUnitCircle.normalized * this.speed);
        }
    }

}
