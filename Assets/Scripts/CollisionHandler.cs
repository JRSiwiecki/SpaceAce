using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into a friendly!");
                break;
            
            case "Finish":
                Debug.Log("Bumped into the finish!");
                break;
            
            default:
                Debug.Log("Bumped into an obstacle!");
                break;
        }
    }
}
