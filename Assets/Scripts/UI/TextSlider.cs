using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class TextSlider : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public Slider Slider { get; private set; }

        private void Awake()
        {
            Slider = GetComponent<Slider>();
            OnValueChanged(Slider.value);
            Slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            text.text = $"{value}";
        }
    }
}
