using UnityEngine;
using UnityEngine.SceneManagement;

public class MakePersistenet : MonoBehaviour
{
        private static MakePersistenet instance;

    void Start()
        {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
