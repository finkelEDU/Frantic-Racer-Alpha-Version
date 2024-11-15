using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private float handling = 100;
    [SerializeField] private float acceleratorMultiplier = 0;

    private bool btn_up, btn_down, btn_left, btn_right, btn_restart;

    public RaceControlX raceControlX;

    private AudioSource playerAudio;
    public AudioClip finishSound;
    public AudioClip engineIdle;
    public AudioClip engineMoving;

    public bool raceActive = true;

    public int playerCheckpoint = 1;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAudio.loop = true;
        playerAudio.Play();
    }

    void Update()
    {
        if(btn_restart)
        {
            Debug.Log("WHAT");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(raceActive)
        {
            btn_left = Input.GetKey(KeyCode.LeftArrow);
            btn_right = Input.GetKey(KeyCode.RightArrow);
            btn_restart = Input.GetKeyDown(KeyCode.R);

            transform.Translate(-(Vector3.forward * maxSpeed * Time.deltaTime * acceleratorMultiplier));

            if(btn_left)
            {
                transform.Rotate(Vector3.up, -(handling * Time.deltaTime));
            }

            if(btn_right)
            {
                transform.Rotate(Vector3.up, (handling * Time.deltaTime));
            }

            if(acceleratorMultiplier <= 0)
            {
                if(playerAudio.clip != engineIdle)
                {
                    playerAudio.clip = engineIdle;
                    playerAudio.PlayDelayed(0);
                }
            }

            if(acceleratorMultiplier > 0)
            {
                if(playerAudio.clip != engineMoving)
                {
                    playerAudio.Stop();
                    playerAudio.clip = engineMoving;
                    playerAudio.PlayDelayed(0);
                }
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        btn_up = Input.GetKey(KeyCode.UpArrow);
        btn_down = Input.GetKey(KeyCode.DownArrow);

        if(collision.gameObject.CompareTag("Wall"))
        {
            acceleratorMultiplier = Mathf.Max(-(acceleratorMultiplier * 0.2f), -(acceleratorMultiplier * 0.8f));
        }

        if(collision.gameObject.CompareTag("Grass") || collision.gameObject.CompareTag("Road"))
        {
            if(raceActive)
            {
                if(btn_up)
                {
                    if(acceleratorMultiplier < 1)
                    {
                        acceleratorMultiplier += 0.02f;

                        if(acceleratorMultiplier > 1)
                        {
                            acceleratorMultiplier = 1;
                        }
                    }
                }
                else if(acceleratorMultiplier > 0)
                {
                    acceleratorMultiplier -= 0.01f;

                    if(btn_down)
                    {
                        acceleratorMultiplier -= 0.03f;
                    }

                    if(acceleratorMultiplier < 0)
                    {
                        acceleratorMultiplier = 0;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            CheckPointX check = other.gameObject.GetComponent<CheckPointX>();

            if(check.checkpointNumber == playerCheckpoint)
            {
                playerCheckpoint++;
            }

            if(playerCheckpoint > 5)
            {
                RaceEnd();
            }
        }
    }

    void RaceEnd()
    {
        playerAudio.loop = false;
        playerAudio.Stop();
        playerAudio.PlayOneShot(finishSound, 1.0f);
        raceControlX.raceActive = false;
        raceActive = false;
        acceleratorMultiplier = 0;
    }
}