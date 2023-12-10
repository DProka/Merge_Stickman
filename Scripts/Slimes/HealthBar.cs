using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider _healthBar;
    public Slider _attackBar;
    public Canvas canvas;

    public void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    public void UpdateHealthBar(float _health, float _maxHealth)
    {
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _health;
    }

    public void UpdateAttackBar(float _attackCharge, float _attackSpeed)
    {
        _attackBar.maxValue = _attackSpeed;
        _attackBar.value = _attackCharge;
    }
}
