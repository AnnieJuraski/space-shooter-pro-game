using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speedLaser = 10.0f;

    public bool IsEnemyLaser = false;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        var direction = IsEnemyLaser ? Vector3.down : Vector3.up;
        transform.Translate(direction * _speedLaser * Time.deltaTime);

        var reachedLimit = IsEnemyLaser ? transform.position.y < -5.5f : transform.position.y > 5.4f;

        if (reachedLimit)
        {
            if (transform.parent)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void AssignEnemy()
    {
        IsEnemyLaser = true;   
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsEnemyLaser == true && other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
        
    }
}
