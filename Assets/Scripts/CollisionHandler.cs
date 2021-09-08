using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //                  STRUCTURE 
    // PARAMETERS - for tuning, typically in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables
    
    // Level Load Delay Timer
    [SerializeField] float delay = 1.5f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    
    AudioSource audioSource;

    bool isTransitioning = false;
    
    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) { return; }
        
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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
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
