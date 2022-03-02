using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimate : MonoBehaviour
{

    private TMP_Text m_TextComponent;
    private Mesh mesh;
    private Vector3[] vertices;
    public float fadetime = 1500f;
    private bool showText = false;
    private bool showing = false;
    void Awake()
    {
        m_TextComponent = GetComponent<TMP_Text>();
    }


    void Start()
    {
        
    }

    private void Update()
    {
        if (showText && !showing)
        {
            StartCoroutine(AnimateText());
        }
        else if (!showText && showing)
        {
            StartCoroutine(FadeOut());
            StopCoroutine(AnimateText());
        }
    }

    public void Toggle()
    {
        showText = !showText;
    }

    IEnumerator AnimateText()
    {
        showing = true;
        float waitTime = 0;
        while (waitTime < 1)
        {
            m_TextComponent.color = Color.Lerp(Color.clear, Color.white, waitTime);
            yield return null;
            waitTime += Time.deltaTime / fadetime;
        }
        yield return new WaitForSeconds(5);

        while (true)
        {
            m_TextComponent.ForceMeshUpdate();
            TMP_TextInfo textInfo = m_TextComponent.textInfo;
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                TMP_CharacterInfo info = textInfo.characterInfo[i];
                if (!info.isVisible)
                {
                    continue;
                }
                var verts = textInfo.meshInfo[info.materialReferenceIndex].vertices;
                for (int j = 0; j < 4; j++)
                {
                    var orig = verts[info.vertexIndex + j];
                    verts[info.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 3f, 0);
                }
            }
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                m_TextComponent.UpdateGeometry(meshInfo.mesh, i);
            }
            mesh = m_TextComponent.mesh;
            vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 offset = Wobble(Time.time + i);

                vertices[i] = vertices[i] + offset;
            }
            mesh.vertices = vertices;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeOut()
    {
        showing = false;
        float waitTime = 0;
        while (waitTime < 1)
        {
            m_TextComponent.color = Color.Lerp(Color.white, Color.clear, waitTime);
            yield return null;
            waitTime += Time.deltaTime / fadetime;
        }
    }
    IEnumerator AnimateVertexColors()
    {
        // Force the text object to update right away so we can have geometry to modify right from the start.
        m_TextComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = m_TextComponent.textInfo;
        int currentCharacter = 0;

        Color32[] newVertexColors;
        Color32 c0 = m_TextComponent.color;

        while (true)
        {
            int characterCount = textInfo.characterCount;

            // If No Characters then just yield and wait for some text to be added
            if (characterCount == 0)
            {
                yield return new WaitForSeconds(0.25f);
                continue;
            }

            // Get the index of the material used by the current character.
            int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            newVertexColors = textInfo.meshInfo[materialIndex].colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;

            // Only change the vertex color if the text element is visible.
            if (textInfo.characterInfo[currentCharacter].isVisible)
            {
                c0 = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

                newVertexColors[vertexIndex + 0] = c0;
                newVertexColors[vertexIndex + 1] = c0;
                newVertexColors[vertexIndex + 2] = c0;
                newVertexColors[vertexIndex + 3] = c0;

                // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
                m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                // This last process could be done to only update the vertex data that has changed as opposed to all of the vertex data but it would require extra steps and knowing what type of renderer is used.
                // These extra steps would be a performance optimization but it is unlikely that such optimization will be necessary.
            }

            currentCharacter = (currentCharacter + 1) % characterCount;

            yield return new WaitForSeconds(0.05f);
        }
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 0.5f), Mathf.Cos(time * 0.1f));
    }

}
