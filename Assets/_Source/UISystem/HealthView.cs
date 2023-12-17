using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _sliderFill;
        [SerializeField] private Color _fullHpColor;
        [SerializeField] private Color _lowHpColor;
        
        
        public void ChangeHeath(float hpPercentage)
        {
            _slider.value = hpPercentage;
            _sliderFill.color = Color.Lerp(_lowHpColor,_fullHpColor,hpPercentage);
        }
    }
}
