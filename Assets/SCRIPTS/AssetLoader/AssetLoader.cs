using System.Collections;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public static AssetLoader Instance;

    public string prefabName = "";

    public float duration = 30.0f;

    private GameObject memoryPrefab;
    private GameObject sceneInstance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("Double instance of singleton!");
    }

    void Start()
    {
        LoadAsset();
    }

    public void LoadAsset(string name = "")
    {
        StartCoroutine(LoadAndUnload(name));
    }

    IEnumerator LoadAndUnload(string name)
    {
        if (name == "") name = prefabName;

        memoryPrefab = Resources.Load<GameObject>(name);

        if (memoryPrefab != null)
        {
            sceneInstance = Instantiate(memoryPrefab, transform.position, transform.rotation);
        }
        else
        {
            yield break;
        }

        yield return new WaitForSeconds(duration);

        if (sceneInstance != null)
        {
            Destroy(sceneInstance);
        }

        memoryPrefab = null;

        Resources.UnloadUnusedAssets();
    }
}