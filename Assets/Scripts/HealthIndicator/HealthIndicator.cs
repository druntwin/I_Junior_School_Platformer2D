using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _healthSmoothSlider;
    [SerializeField] private Color _color;
    [SerializeField] private float _smoothStep;
    [SerializeField] private float _smoothSpeed;

    private float _maxHealth;
    private float _newValue;

    private void Start()
    {
        _healthText.color = _color;
    }

    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void UpdateIndicator(float healthMount)
    {
        _newValue = healthMount / _maxHealth;

        if (_healthText != null)
        {
            _healthText.text = $"Health {healthMount} / {_maxHealth}";
        }

        if (_healthSlider != null)
        {
            _healthSlider.value = _newValue;
        }

        if( _healthSmoothSlider != null)
        {
            StartCoroutine(UpdateSmoothSlider());            
        }
    }

    IEnumerator UpdateSmoothSlider()
    {
        var waitSomeTime = new WaitForSeconds(_smoothSpeed);
        float minValuesDifference = 0.001f;

        while (_healthSmoothSlider.value != _newValue)
        {
            _healthSmoothSlider.value = Mathf.SmoothStep(_healthSmoothSlider.value, _newValue, _smoothStep);

            if (Mathf.Abs(_healthSmoothSlider.value - _newValue) < minValuesDifference)
                _healthSmoothSlider.value = _newValue;

            yield return waitSomeTime;
        }
    }
}
