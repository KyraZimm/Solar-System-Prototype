using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMapper : MonoBehaviour {
    [System.Serializable] public class AssignedTexture {
        [HideInInspector] public string key;
        public Texture3D texture;
        public AssignedTexture(string name) { key = name; }
    }

    [SerializeField] private List<AssignedTexture> textures;

#if UNITY_EDITOR
    private void OnValidate() {
        //if planets have been added/removed, update texture sheet
        string[] allPlanetNames = PlanetCSVReader.GetPlanetNames_EditorOnly();
        int diffInLists = textures.Count - allPlanetNames.Length;
        if (diffInLists > 0)
            textures.RemoveRange((textures.Count - 1) - diffInLists, diffInLists);

        for (int i = 0; i < allPlanetNames.Length; i++) {
            if (textures.Count <= i || textures[i].key != allPlanetNames[i])
                textures.Insert(i, new AssignedTexture(allPlanetNames[i]));
        }
    }
#endif

    public Texture3D GetPlanetTexture(string planetName) {
        foreach (AssignedTexture texture in textures)
            if (texture.key == planetName)
                return texture.texture;

        return null;
    }

}
