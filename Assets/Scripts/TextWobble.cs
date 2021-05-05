using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWobble : MonoBehaviour
{
    private TMP_Text tmpText;
    private Mesh mesh;
    private Vector3[] vertices;
    private float timer = 0f;
    private int lastUpdate = 0;
    private Color[] lastColors;
    readonly Color[] colorArray = new Color[] {
        new Color(1, 0, 0, 1),
        new Color(0, 1, 0, 1),
        new Color(.16f, .4f, .84f, 1),
        new Color(1, 0.87f, 0, 1),
        new Color(1, 0, 1, 1),
        new Color(0, 1, 1, 1),
        new Color(1, 1, 1, 1),
    };

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        tmpText.ForceMeshUpdate();
        mesh = tmpText.mesh;
        vertices = mesh.vertices;

        Color[] colors = mesh.colors;
        List<Color> newColors = new List<Color>(colorArray);

        for (int i = 0; i < tmpText.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo info = tmpText.textInfo.characterInfo[i];

            int idx = info.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            vertices[idx] += offset;
            vertices[idx + 1] += offset;
            vertices[idx + 2] += offset;
            vertices[idx + 3] += offset;

            if (char.IsLetter(info.character))
            {
                if (newColors.Count <= 0)
                {
                    newColors = new List<Color>(colorArray);
                }

                Color c = newColors[Random.Range(0, (newColors.Count))];
                newColors.Remove(c);

                colors[idx] = colors[idx + 1] = colors[idx + 2] = colors[idx + 3] = c;
            }
        }
        timer += Time.deltaTime; ;
        if ((((((int)(timer % 60)) % 2) == 0) && lastUpdate != ((int)(timer % 60))) || lastColors == null)
        {
            lastColors = colors;
            lastUpdate = ((int)(timer % 60));
        }

        mesh.colors = lastColors;
        mesh.vertices = vertices;

        //tmpText.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time), Mathf.Cos(time));
    }
}
