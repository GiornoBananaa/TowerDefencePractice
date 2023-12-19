using System;
using UnityEngine;

namespace TutorialSystem
{
    [Serializable]
    public class DialogWindow
    {
        [field:SerializeField] public GameObject FocusObject;
        [TextArea][field:SerializeField] public string Text;
    }
}
