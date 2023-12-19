using System;
using TMPro;
using UnityEngine;

namespace TutorialSystem
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private DialogWindow[] _dialogWindows = new DialogWindow[2];
        [SerializeField] private GameObject _dialogPanel;
        [SerializeField] private TMP_Text _dialogText;
        private int _dialogWindowNumber;

        private void Awake()
        {
            _dialogText.text = _dialogWindows[_dialogWindowNumber].Text;
        }

        private void NextText()
        {
            //if(_dialogWindows.Length)
            _dialogWindowNumber++;
            _dialogText.text = _dialogWindows[_dialogWindowNumber].Text;
        }
    }
}
