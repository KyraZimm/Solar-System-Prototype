using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    [SerializeField] TextAsset planetCSV;

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of GameManager on {Instance.gameObject.name} was destroyed and replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    private void Start() {
        InstantiatePlanetsFromCSV(planetCSV);
    }

    private void InstantiatePlanetsFromCSV(TextAsset csv) {
        var lines = csv.text.Split('\n');
        for (int i = 0; i < lines.Length; i++) {
            PlanetData pData = new PlanetData(lines[i]);
            Planet.MakeNewPlanet(pData);
        }
    }


}
