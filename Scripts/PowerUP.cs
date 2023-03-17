using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int PowerupID;
    [SerializeField]
    private AudioClip _clip;

    
    void Update()
    {
        Movement();
    }


    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5.8f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {

                switch (PowerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostPowerupActive();
                        break;

                    case 2:
                        player.ShieldsActive();
                        break;
                }

                Destroy(this.gameObject);
            }

        }
    }
}
