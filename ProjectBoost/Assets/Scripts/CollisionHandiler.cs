using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandiler : MonoBehaviour
{

    [SerializeField] float CrashDelay = 2f;
    [SerializeField] float NextLevelDelay = 1f;
    [SerializeField] AudioClip finish;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;


    bool isTransitioning = false;
    bool collisionDisable = false;

    AudioSource ad;
    void Start()
    {
        ad = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondtoDebugKeys();
    }

    void RespondtoDebugKeys()
    {
        if(Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisable)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Object is frriendly");
                break;
            case "Finish":
                Debug.Log("Ayy you landed");
                Startnextlevel();
                break;
            case "Fuel":
                Debug.Log("Fuel picked up");
                break;
            default:
                Debug.Log("You blew it");
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        ad.Stop();
        GetComponent<Movement>().enabled = false;
        ad.PlayOneShot(crash);
        crashParticles.Play();
        Invoke("ReloadLevel", CrashDelay);
    }
    void Startnextlevel()
    {
        isTransitioning = true;
        ad.Stop();
        GetComponent<Movement>().enabled = false;
        ad.PlayOneShot(finish);
        successParticles.Play();
        Invoke("NextLevel", NextLevelDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void NextLevel()
    {    
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
