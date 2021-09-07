using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //                  STRUCTURE 
    // PARAMETERS - for tuning, typically in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables
    
    // Level Load Delay Timer
    [SerializeField] float delay = 0.75f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    
    AudioSource audioSource;
    Movement movement;
    
    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }
    
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into a friendly!");
                break;
            
            case "Finish":
                StartSuccessSequence();
                break;
            
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        // TODO: Add particle FX upon crash
        movement.enabled = false;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        
        audioSource.PlayOneShot(crash);
        Invoke("ReloadLevel", delay);
        
    }

    void StartSuccessSequence()
    {
        // TODO: Add particle FX upon success
        movement.enabled = false;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", delay);
    }
    
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Loop back to the first level if there is no next level.
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
