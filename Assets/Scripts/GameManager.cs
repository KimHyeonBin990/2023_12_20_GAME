using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Invoke the StartStandbyTransition method after 70 seconds
        Invoke("StartStandbyTransition", 70f);
    }

    // Call this method to initiate the transition to the Standby scene
    public void StartStandbyTransition()
    {
        SceneManager.LoadScene("end");
    }
}

public class Player : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Your player collision logic here

        // For example, you can trigger the transition when the player collides
        // with something by calling the GameManager method
        GameManager.instance.StartStandbyTransition();
    }
}

public class Monster : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Your monster collision logic here

        // For example, you can trigger the transition when the monster collides
        // with something by calling the GameManager method
        GameManager.instance.StartStandbyTransition();
    }
}
