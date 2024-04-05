using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleSlider : MonoBehaviour {
    private void Awake() {
        Slider slider = GetComponent<Slider>();
        if (slider == null) {
            Debug.LogError($"There is a TimeScaleSlider component on {gameObject.name}, but there is no UI Slider root component on this GameObject.");
            return;
        }

        slider.onValueChanged.AddListener(AdjustTimeScale);
    }

    private void AdjustTimeScale(float timeScale) { Planet.ChangeTimeScale(timeScale);  }
}
