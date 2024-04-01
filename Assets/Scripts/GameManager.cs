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
        string[] lines = csv.text.Split('\n');
        for (int i = 1; i < lines.Length; i++) {
            string[] info = lines[i].Split(',');
            PlanetData pData = new PlanetData(info[0], float.Parse(info[1]), float.Parse(info[2]), float.Parse(info[3]), float.Parse(info[4]));

            Planet newPlanet = Planet.MakeNewPlanet(pData);
        }
    }

}
