using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    PlanetData data;
    private Rigidbody rb;
    private static GameObject planetPrefab {
        /*get {
            if (m_value == null)
                m_value = Resources.Load("PlanetPrefab") as GameObject;
            return m_value;
        }*/
        get { return Resources.Load("PlanetPrefab") as GameObject; }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate() {
        float angleToOrbit = 360 / (data.orbit * Time.fixedDeltaTime);
        transform.Rotate(0, 0, angleToOrbit);

        //planet rotation
        rb.MoveRotation(Quaternion.AngleAxis((data.rot * Time.fixedDeltaTime), Vector3.up));
    }

    private void OnMouseOver() { UIValues.Instance.UpdateUI(data); }

    public static Planet MakeNewPlanet(PlanetData newPlanetData) {
        Vector3 startingPos = new Vector3(newPlanetData.distFromSun, 0, 0);
        Planet newPlanet = Instantiate(planetPrefab, startingPos, Quaternion.identity).GetComponent<Planet>();
        newPlanet.gameObject.name = newPlanetData.name;
        newPlanet.data = newPlanetData;
        newPlanet.transform.localScale = new Vector3(newPlanetData.scale, newPlanetData.scale, newPlanetData.scale);
        return newPlanet;
    }

}
