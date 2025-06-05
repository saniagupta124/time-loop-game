using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private GameObject door;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource ambientSource;

    [SerializeField] private AudioClip babyAmbient;
    [SerializeField] private AudioClip newAmbient;
    [SerializeField] private AudioClip nurseryClip;
    [SerializeField] private AudioClip kitchenClip;
    [SerializeField] private AudioClip truckClip;
    [SerializeField] private AudioClip schoolClip;

    private static PlayerCollision instance;
    private static Vector3 nextSpawnPosition = Vector3.zero;
    private static bool hasSpawnPosition = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Prevent duplicate player objects
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        if (audioSource != null) DontDestroyOnLoad(audioSource);
        if (ambientSource != null) DontDestroyOnLoad(ambientSource);
       // if (GameObject.Find("GameObject") != null) DontDestroyOnLoad(GameObject.Find("GameObject"));

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.height = 1.8f;
    }

    private void OnTriggerEnter(Collider other)
    {
        string sceneToLoad = "";

        if (other.CompareTag("nurseryDoor"))
        {
            controller.height = 1.4f;
            PlayAudio(ambientSource, babyAmbient);
            PlayAudio(audioSource, nurseryClip);
        }
        else if (other.CompareTag("bedroomDoor"))
        {
            controller.height = 1.7f;
            PauseAudio(audioSource);
        }
        else if (other.CompareTag("bigBedroomDoor"))
        {
            controller.height = 2f;
            PauseAudio(audioSource);
        }
        else if (other.CompareTag("kitchenDoor"))
        {
            PlayAudio(audioSource, kitchenClip);
        }
        else if (other.CompareTag("livingDoor"))
        {
            PauseAudio(audioSource);
            PlayAudio(ambientSource, newAmbient);
        }

        if (other.CompareTag("Door")) sceneToLoad = "Neighborhood";
        else if (other.CompareTag("TravelDoor")) sceneToLoad = "TravelerProject";
        else if (other.CompareTag("schoolDoor")) sceneToLoad = "SchoolHallway";
        else if (other.CompareTag("Door2")) sceneToLoad = "Backrooms";
        else if (other.CompareTag("DoorOffice")) sceneToLoad = "Office";
        else if (other.CompareTag("DoorChaos")) sceneToLoad = "Chaos";
        else if (other.CompareTag("DoorEnd")) sceneToLoad = "End";
        else if (other.CompareTag("DoorLong")) sceneToLoad = "Long";
        else if (other.CompareTag("Door3")) sceneToLoad = "Backrooms";
        else if (other.CompareTag("Truck"))
        {
            door = FindInactiveObject();
            StartCoroutine(CloseDoorSequence());
            return;
        }

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            nextSpawnPosition = GetSpawnPosition(sceneToLoad, other.tag);
            hasSpawnPosition = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    GameObject FindInactiveObject()
    {
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.name == "doorClosed")
            {
                return obj; // Return the first match
            }
        }
        return null; // If not found, return null
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (hasSpawnPosition)
        {
            StartCoroutine(SetPlayerPosition());
        }
    }

    IEnumerator SetPlayerPosition()
    {
        yield return new WaitForEndOfFrame();

        if (controller != null)
        {
            controller.enabled = false;
            transform.position = nextSpawnPosition;
            controller.enabled = true;
        }
        else
        {
            transform.position = nextSpawnPosition;
        }

        hasSpawnPosition = false;
    }

    private Vector3 GetSpawnPosition(string sceneName, string triggerTag)
    {
        Debug.Log(sceneName + " " + triggerTag);
        if (sceneName == "Neighborhood")
        {
            gameObject.GetComponent<CharacterController>().height = 2f;
            if (triggerTag == "Door") return new Vector3(-6.33f, 1.65f, -43.13f);
        }
        if (sceneName == "TravelerProject" && triggerTag == "TravelDoor") return new Vector3(16.17318f, 1.08f, 1.8466f);
        if (sceneName == "SchoolHallway" && triggerTag == "Truck")
        {
            gameObject.GetComponent<CharacterController>().height = 2f;
            return new Vector3(36.35f, 2.73f, 0.09051989f);
        }
        if (sceneName == "SchoolHallway" && triggerTag == "schoolDoor")
        {
            gameObject.GetComponent<CharacterController>().height = 3f;
            return new Vector3(-229.1f, 2f, 132.81f);
        }
        if (sceneName == "Backrooms" && triggerTag == "Door2")
        {
            gameObject.GetComponent<CharacterController>().height = 2f;
            return new Vector3(-17.67f, 2f, -17.59f);
        }
        if (sceneName == "Backrooms" && triggerTag == "Door3")
        {
            gameObject.GetComponent<CharacterController>().height = 2f;
            return new Vector3(-17.67f, 2f, -17.59f);
        }

        if (sceneName == "Office" && triggerTag == "DoorOffice")
        {
            gameObject.GetComponent<CharacterController>().height = 2f;
            return new Vector3(15.01f, 2f, 10.45f);
        }
        if (sceneName == "Chaos" && triggerTag == "DoorChaos") return new Vector3(-0.5701742f, 34.7f, -37.42f);
        if (sceneName == "End" && triggerTag == "DoorEnd") return new Vector3(69f, -89.12f, 446f);
        if (sceneName == "Long" && triggerTag == "DoorLong") return new Vector3(9.18f, 0.3f, 16.8f);
        
        return Vector3.zero;
    }

    IEnumerator CloseDoorSequence()
    {
        door.SetActive(true);
        yield return new WaitForSeconds(1f);
        PlayAudio(audioSource, truckClip);
        yield return new WaitForSeconds(6f);
        nextSpawnPosition = GetSpawnPosition("SchoolHallway", "Truck");
        hasSpawnPosition = true;
        PauseAudio(audioSource);
        SceneManager.LoadScene("SchoolHallway");
    }

    private void PlayAudio(AudioSource source, AudioClip clip)
    {
        if (source != null && clip != null && !source.isPlaying)
        {
            source.clip = clip;
            source.Play();
        }
    }

    private void PauseAudio(AudioSource source)
    {
        if (source != null && source.isPlaying)
        {
            source.Pause();
        }
    }
}
