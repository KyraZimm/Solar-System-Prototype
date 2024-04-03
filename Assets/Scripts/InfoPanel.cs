using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InfoPanel : MonoBehaviour {
    public static InfoPanel Instance { get; private set; }
    public bool IsVisible { get { return gameObject.activeSelf; } }

    [SerializeField] TMP_Text planetName;
    [SerializeField] Image image;
    [SerializeField] TMP_Text info;

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of InfoPanel on {Instance.gameObject.name} was destroyed and replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;

        ToggleVisibility(false);
    }

    public void ToggleVisibility(bool visible) { gameObject.SetActive(visible); }
    public void ToggleVisibility() { gameObject.SetActive(!IsVisible); }

    public void UpdateUI(PlanetData newPlanetUI) {
        planetName.text = newPlanetUI.name;
    }

}
