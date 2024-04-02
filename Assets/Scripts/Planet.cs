using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planet : MonoBehaviour {

    PlanetData data;

    //physics values
    private Rigidbody rb;
    private TrailRenderer trail;
    private const float TIME_SCALE = .0001f;

    //resources for planet loading
    private const string PLANET_RESOURCES_PATH = "PlanetLoadInfo";
    private static GameObject _planetPrefab;
    private static GameObject planetPrefab {
        get {
            if (_planetPrefab == null)
                _planetPrefab = Resources.Load("PlanetPrefab") as GameObject;
            return _planetPrefab;
        }
    }

    private static TextureMapper _planetTextures;
    private static TextureMapper planetTextures {
        get {
            if (_planetTextures == null)
                _planetTextures = Resources.Load(PLANET_RESOURCES_PATH + "/PlanetTextures").GetComponent<TextureMapper>();
            return _planetTextures;
        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate() {
        //planet orbit
        float angleOfOrbitPerFrame = 360 / (data.orbit * Time.fixedDeltaTime);
        float currAngleOfOrbit = angleOfOrbitPerFrame * Time.time * TIME_SCALE;

        float xDisp = Mathf.Cos(currAngleOfOrbit) * data.distFromSun;
        float zDisp = Mathf.Sin(currAngleOfOrbit) * data.distFromSun;
        rb.MovePosition(new Vector3(xDisp, 0, zDisp));

        //planet rotation
        float angleOfRotPerFrame = 360 / (data.rot * Time.fixedDeltaTime);
        float currAngleOfRot = angleOfRotPerFrame * Time.time * TIME_SCALE;
        rb.MoveRotation(Quaternion.AngleAxis(currAngleOfRot, Vector3.up));
    }

    private void OnMouseOver() { InfoPanel.Instance.UpdateUI(data); }

    public static Planet MakeNewPlanet(PlanetData newPlanetData) {
        //instantiate new planet
        Vector3 startingPos = new Vector3(newPlanetData.distFromSun, 0, 0);
        Planet newPlanet = Instantiate(planetPrefab, startingPos, Quaternion.identity).GetComponent<Planet>();

        //adjust planet properties to match spawn data
        newPlanet.gameObject.name = newPlanetData.name;
        newPlanet.data = newPlanetData;
        newPlanet.transform.localScale = new Vector3(newPlanetData.scale, newPlanetData.scale, newPlanetData.scale);

        //if a texture has been chosen for planet, assign texture
        Texture pTex = planetTextures.GetPlanetTexture(newPlanetData.name);
        if (pTex == null)
            return newPlanet;

        MaterialPropertyBlock matBlock = new MaterialPropertyBlock(); //using a material property block instead of different materials to reduce draw calls
        matBlock.SetTexture("_MainTex", pTex);
        newPlanet.GetComponent<MeshRenderer>().SetPropertyBlock(matBlock);

        return newPlanet;
    }

}
