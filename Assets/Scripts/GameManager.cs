using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"An earlier instance of GameManager on {Instance.gameObject.name} was destroyed and replaced by one on {gameObject.name}.");
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    private void Start() {
        InstantiatePlanets();
    }

    private void InstantiatePlanets() {
        PlanetData[] planets = PlanetCSVReader.GetAllPlanets();
        foreach (PlanetData planet in planets)
            Planet.MakeNewPlanet(planet);
    }

}
