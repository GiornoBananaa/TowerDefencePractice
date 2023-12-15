using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    void PlayClickSound()
    {
        AudioManager.Instance.Play("button_click");
    }
    private void Awake()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }
}


