using System.IO;
using UnityEngine;

public static class GStorage
{
	private const string PATH_TO_RESOURCES = "Assets/Resources/";
	private const string PATH_TO_ATLASES = "Textures/atlases/";
	private const string PATH_TO_TEMPLATES = "templates/template_";
	private const string TEXT_FILE_EXTENTION = ".txt";

	public static GProblemModel getProblem(GTemplateDescriptor aTemplateDescriptor_gtd, int aLineIndex_int)
	{
		GGridModel gridModel_ggm = new GGridModel(aTemplateDescriptor_gtd.getIdsMap());

		TextAsset textFile_ta = (TextAsset) Resources.Load((GStorage.PATH_TO_TEMPLATES + aTemplateDescriptor_gtd.getId()), typeof(TextAsset));
		string line_str = textFile_ta.text.Split("\n"[0])[aLineIndex_int];

		//PARSING STRING...
		int length_int = line_str.Length / 3;
		GProblemStateModelPool statesPool_gpsmp = new GProblemStateModelPool(length_int);

		for( int i = 0; i < length_int; i++ )
		{
			int characterPosition_int = 3 * i;

			int actionTypeId_int = int.Parse(line_str[characterPosition_int] + "");
			int entryPointIndex_int = int.Parse(line_str[characterPosition_int + 1] + "");
			int delta_int = int.Parse(line_str[characterPosition_int + 2] + "");

			gridModel_ggm.applyAction(actionTypeId_int, entryPointIndex_int, delta_int);
			statesPool_gpsmp.add(gridModel_ggm.getIdsMap(), new int[]{actionTypeId_int, entryPointIndex_int, delta_int});
		}
		//...PARSING STRING

		return new GProblemModel(aTemplateDescriptor_gtd, statesPool_gpsmp);
	}

	public static void saveProblemStatesPool(GProblemStateModelPool aProblemStatesPool_gpsmp, int aTemplateIndex_int)
	{
		string path_str = GStorage.PATH_TO_RESOURCES + GStorage.PATH_TO_TEMPLATES + aTemplateIndex_int + GStorage.TEXT_FILE_EXTENTION;
		StreamWriter writer_sw = new StreamWriter(path_str, true);
		string actions_str = "";

		for( int i = 0; i < aProblemStatesPool_gpsmp.length(); i++ )
		{
			GProblemStateModel problemStateModel_gpsm = aProblemStatesPool_gpsmp.getState(i);
			int[] action_int_arr = problemStateModel_gpsm.getAction();

			for( int j = 0; j < 3; j++ )
			{
				actions_str += action_int_arr[j];
			}
		}

		writer_sw.WriteLine(actions_str);
		writer_sw.Close();
		//...WRITE
	}

	public static GAtlas getAtlas(string aAtlasName_str)
	{
		Texture2D texture_t2d = (Texture2D) Resources.Load((GStorage.PATH_TO_ATLASES + aAtlasName_str), typeof(Texture2D));
		TextAsset textFile_ta = (TextAsset) Resources.Load((GStorage.PATH_TO_ATLASES + aAtlasName_str), typeof(TextAsset));
	
		string[] splittedStrings_str_arr = textFile_ta.text.Split("\n"[0]);
		string[] descriptors_str_arr = new string[splittedStrings_str_arr.Length - 1];

		for( int i = 0; i < descriptors_str_arr.Length; i++ )
		{
			descriptors_str_arr[i] = splittedStrings_str_arr[i];
		}

		return new GAtlas(texture_t2d, descriptors_str_arr);
	}
}