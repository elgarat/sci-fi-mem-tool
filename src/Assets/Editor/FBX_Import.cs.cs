using UnityEngine;
using UnityEditor;
using System.Collections;

public class FBX_Import : AssetPostprocessor {

	public const float importScale = 100.0f;

	void OnPreprocessModel(){
		ModelImporter importer = assetImporter as ModelImporter;
		importer.globalScale = importScale;
	}
}
