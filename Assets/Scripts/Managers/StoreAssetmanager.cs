using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class StoreAssetmanager : MonoBehaviour
{
    public static StoreAssetmanager Instance { get; private set; }

    public JSONNode itemsAvailable;

    [System.Serializable]
    public struct StoreItemIcon
    {
        public string name;
        public Texture image;
    }

    public List<StoreItemIcon> storeItemIcons;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Texture getItemIcon(string name)
    {
        int index = storeItemIcons.FindIndex(a => a.name.Contains(name));
        return storeItemIcons[index].image;
    }
}
