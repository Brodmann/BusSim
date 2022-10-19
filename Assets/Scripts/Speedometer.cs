using System;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Rigidbody bus;
    [SerializeField] private TextMeshProUGUI speedText;
    
    private float tmp;
    
    void Update()
    {
        tmp = Mathf.Sqrt(Mathf.Pow(bus.velocity.sqrMagnitude, 1.3f));
        speedText.text = tmp.ToString("0") + "km/h";
    }
}
