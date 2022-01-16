using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
using Unity.EditorCoroutines.Editor;
#endif
using Sirenix.OdinInspector;

public class ThumbnailSaver :MonoBehaviour
{
#if UNITY_EDITOR
	[Button("Create Brush Thumbnails")]
	public void CreateBrushThumbnails()
    {
		EditorCoroutineUtility.StartCoroutine(CreateThumbnailForAllSkins(), this);
    }

	public IEnumerator CreateThumbnailForAllSkins()
    {
		int nameIndex = 0;

		SkinData[] skins = new List<SkinData>(Resources.LoadAll<SkinData>("Skins")).ToArray();
        for (int i = 0; i < skins.Length; i++)
        {
			Renderer[] renderers = skins[i].Brush.m_Prefab.GetComponent<Brush>().m_Renderers.ToArray();
			AssetPreview.SetPreviewTextureCacheSize(1024);
			for (int t = 0; t < skins[i].Color.m_Colors.Count; t++)
			{
				for (int z = 0; z < renderers.Length; z++)
                {
               
					Debug.Log(skins[i].Color.m_Colors[t]);
					renderers[z].sharedMaterial.color = skins[i].Color.m_Colors[t];
					EditorUtility.SetDirty(skins[i].Brush);
					EditorUtility.SetDirty(renderers[z].sharedMaterial);
					EditorUtility.SetDirty(renderers[z]);
				}
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
				yield return EditorCoroutineUtility.StartCoroutine(CreateColorThumbnail(skins[i].Brush.m_Prefab, "Thumbnail" + nameIndex++), this);
			}
        }
	}

	public IEnumerator CreateColorThumbnail(GameObject prefab, string fileName)
	{
		Texture2D newTexture = null;
		while(newTexture == null)
        {
			newTexture = AssetPreview.GetAssetPreview(prefab);
			yield return null;
		}
		Color bgColor = newTexture.GetPixel(0, 0);
		Color colorAlpha = new Color(0, 0, 0, 0);
		for (int i = 0; i < newTexture.width; i++)
		{
			for (int z = 0; z < newTexture.height; z++)
			{
				if (newTexture.GetPixel(i, z) == bgColor)
				{
					newTexture.SetPixel(i, z, colorAlpha);
				}
			}
		}
		newTexture.Apply();
		byte[] bytes = newTexture.EncodeToPNG();

		// For testing purposes, also write to a file in the project folder
		File.WriteAllBytes(Application.dataPath + "/SavedImages/" +fileName+ ".png", bytes);
	}
#endif
}
