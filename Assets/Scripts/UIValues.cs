using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIValues : MonoBehaviour {
    public static UIValues Instance { get; private set; }

    [SerializeField] TMP_Text planetName;

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of UIValues on {Instance.gameObject.name} was destroyed and replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    public void UpdateUI(PlanetData newPlanetUI) {
        planetName.text = newPlanetUI.name;
    }
}
