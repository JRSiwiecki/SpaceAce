using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Level Load Delay Timer
    [SerializeField] float delay = 0.75f;
    AudioSource audioSource;
    
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
        // TODO: Add SFX upon crash
        // TODO: Add particle FX upon crash
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        Invoke("ReloadLevel", delay);
        
    }

    void StartSuccessSequence()
    {
        // TODO: Add SFX upon success
        // TODO: Add particle FX upon success
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
