using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandler : MonoBehaviour
{
    AudioSource audioSource;
    
    float delay = 1f;
    [SerializeField] AudioClip failure;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem failurePaticles;
    [SerializeField] ParticleSystem successParticles;

    bool isTransitioning;
    bool collisionEnabled;

    void Start(){
        isTransitioning = true;
        collisionEnabled = true;
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        RespondToDebugKeys();
    }
    
    void RespondToDebugKeys(){

        if(Input.GetKeyDown(KeyCode.L)){
            loadNextLevel();
        }else if(Input.GetKeyDown(KeyCode.C)){
            collisionEnabled = !collisionEnabled;
        }

    }

    void OnCollisionEnter(Collision other)
    {   
        if(!collisionEnabled){
            return;
        }

        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("On the Starting Stage");
                break;

            case "Finish":
                NextLevelSequence();
                break;
        
            default:
                StartCrashSequence();
                break;
       } 
    }

    void StartCrashSequence(){
        if(isTransitioning){
            audioSource.Stop();
            audioSource.PlayOneShot(failure);
            failurePaticles.Play();
            isTransitioning = false;  
        }
        GetComponent<Mover>().enabled = false;
        Invoke("respawnScene",delay);   
    }

    void respawnScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevelSequence(){
        if(isTransitioning){
            audioSource.Stop();
            audioSource.PlayOneShot(success);
            successParticles.Play();
            isTransitioning = false;
        }
        GetComponent<Mover>().enabled = false;
        Invoke("loadNextLevel",delay); 
    }

    void loadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
