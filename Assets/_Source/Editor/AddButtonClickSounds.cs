using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AddButtonClickSounds : ScriptableObject
{
    [MenuItem("Tools/AddButtonSoundInScene")]
    static void AddSoundForButton()
    {
        DeleteSoundForButton();
        GameObject[] go;
        go = FindObjectsOfType(typeof(GameObject),true) as GameObject[];
        
        foreach (GameObject child in go)
        {
            if (child.TryGetComponent<Button>(out Button bt))
            {
                child.AddComponent<ButtonSound>();
                Debug.Log($"{child.name} button added sound success!");
            }
        }
    }

    [MenuItem("Tools/ClearAllButtonSoundInScene")]
    static void DeleteSoundForButton()
    {
        GameObject[] go;
        go = FindObjectsOfType(typeof(GameObject),true) as GameObject[];
        foreach (GameObject child in go)
        {
            if (child.GetComponent<ButtonSound>() != null)
            {
                DestroyImmediate(child.GetComponent<ButtonSound>());
                Debug.Log($"{child.name} button removes sound successfully!");
            }
        }
    }
}
