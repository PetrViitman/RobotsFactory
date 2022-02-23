using UnityEngine;
using System.IO;

public static class GAtlasGenerator
{
	private const int DEFAULT_ATLAS_SIDE_SIZE = 2048;
	private const string TEXT_FILE_EXTENTION = ".txt";
	private const string TEXTURE_FILE_EXTENTION = ".png";

	private static Texture2D atlasTexture_t2d;
	private static string unpackedSourcesPathPrefix_str;
	private static StreamWriter writer_sw;
	private static string atlasName_str;


	private static string getOutputPath()
	{
		return "Assets/Resources/Textures/atlases/" + atlasName_str;
	}

	private static void generateBlankAtlas(string aAtlasName_str, int aWidth_int, int aHeight_int, string aUnpackedSourcesPathPrefix_str)
	{
		GAtlasGenerator.atlasName_str = aAtlasName_str;
		GAtlasGenerator.unpackedSourcesPathPrefix_str = aUnpackedSourcesPathPrefix_str;

		//FILLING BLANK TEXTURE WITH TRANSPARENCY...
		Texture2D texture_t2d = new Texture2D(aWidth_int, aHeight_int);
		
		for( int  y = 0 ; y < aHeight_int; y++ )
		{
			for( int  x = 0 ; x < aWidth_int; x++ )
			{
				texture_t2d.SetPixel(x, y, Color.clear);
			}
		}
		//...FILLING BLANK TEXTURE WITH TRANSPARENCY

		GAtlasGenerator.atlasTexture_t2d = texture_t2d;

		File.Delete(GAtlasGenerator.getOutputPath() + GAtlasGenerator.TEXT_FILE_EXTENTION);
		GAtlasGenerator.writer_sw = new StreamWriter(GAtlasGenerator.getOutputPath() + GAtlasGenerator.TEXT_FILE_EXTENTION, true);
	}

	private static void saveAtlas()
	{
		GAtlasGenerator.atlasTexture_t2d.Apply();
		byte[] bytes_byte_arr = GAtlasGenerator.atlasTexture_t2d.EncodeToPNG();
		System.IO.File.WriteAllBytes(GAtlasGenerator.getOutputPath() + GAtlasGenerator.TEXTURE_FILE_EXTENTION, bytes_byte_arr);
		
		GAtlasGenerator.writer_sw.Close();
	}

	private static void addPixelSector(string aName_str, int aX_int, int aY_int)
	{
		string areaDescriptor_str = aName_str;
		areaDescriptor_str += " " + (aX_int - GAtlas.ATLAS_SEGMENT_PADDING);
		areaDescriptor_str += " " + (aY_int - GAtlas.ATLAS_SEGMENT_PADDING);
		areaDescriptor_str += " " + (GAtlas.ATLAS_SEGMENT_PADDING * 2 + 1);
		areaDescriptor_str += " " + (GAtlas.ATLAS_SEGMENT_PADDING * 2 + 1);

		GAtlasGenerator.writer_sw.WriteLine(areaDescriptor_str);
	}

	private static void addPicture(string aName_str, int aX_int, int aY_int)
	{
		Texture2D texture_t2d = (Texture2D)Resources.Load(GAtlasGenerator.unpackedSourcesPathPrefix_str + aName_str);

		int width_int = texture_t2d.width;
		int height_int = texture_t2d.height;

		int offsetX_int = aX_int * width_int;
		int offsetY_int = aY_int * height_int;

		for( int  y = 0 ; y < texture_t2d.height; y++ )
		{
			for( int  x = 0 ; x < texture_t2d.width; x++ )
			{
				Color targetColor_c = texture_t2d.GetPixel(x, texture_t2d.height - y);

				GAtlasGenerator.atlasTexture_t2d.SetPixel(
					offsetX_int + x,
					GAtlasGenerator.atlasTexture_t2d.height - offsetY_int - y,
					targetColor_c);
			}
		}

		string areaDescriptor_str = aName_str;
		areaDescriptor_str += " " + offsetX_int;
		areaDescriptor_str += " " + offsetY_int;
		areaDescriptor_str += " " + width_int;
		areaDescriptor_str += " " + height_int;

		GAtlasGenerator.writer_sw.WriteLine(areaDescriptor_str);
	}

	public static void generateAtlasFont()
	{
		GAtlasGenerator.generateBlankAtlas(
			"font",
			GAtlasGenerator.DEFAULT_ATLAS_SIDE_SIZE,
			GAtlasGenerator.DEFAULT_ATLAS_SIDE_SIZE,
			"Textures/unpacked/FONT/");

		GAtlasGenerator.addPicture("A", 0, 0);
		GAtlasGenerator.addPicture("B", 1, 0);
		GAtlasGenerator.addPicture("C", 2, 0);
		GAtlasGenerator.addPicture("D", 3, 0);
		GAtlasGenerator.addPicture("E", 4, 0);
		GAtlasGenerator.addPicture("F", 5, 0);
		GAtlasGenerator.addPicture("G", 6, 0);
		GAtlasGenerator.addPicture("H", 7, 0);

		GAtlasGenerator.addPicture("I", 0, 1);
		GAtlasGenerator.addPicture("J", 1, 1);
		GAtlasGenerator.addPicture("K", 2, 1);
		GAtlasGenerator.addPicture("L", 3, 1);
		GAtlasGenerator.addPicture("M", 4, 1);
		GAtlasGenerator.addPicture("N", 5, 1);
		GAtlasGenerator.addPicture("O", 6, 1);
		GAtlasGenerator.addPicture("P", 7, 1);

		GAtlasGenerator.addPicture("Q", 0, 2);
		GAtlasGenerator.addPicture("R", 1, 2);
		GAtlasGenerator.addPicture("S", 2, 2);
		GAtlasGenerator.addPicture("T", 3, 2);
		GAtlasGenerator.addPicture("U", 4, 2);
		GAtlasGenerator.addPicture("V", 5, 2);
		GAtlasGenerator.addPicture("W", 6, 2);
		GAtlasGenerator.addPicture("X", 7, 2);

		GAtlasGenerator.addPicture("Y", 0, 3);
		GAtlasGenerator.addPicture("Z", 1, 3);
		GAtlasGenerator.addPicture("0", 2, 3);
		GAtlasGenerator.addPicture("1", 3, 3);
		GAtlasGenerator.addPicture("2", 4, 3);
		GAtlasGenerator.addPicture("3", 5, 3);
		GAtlasGenerator.addPicture("4", 6, 3);
		GAtlasGenerator.addPicture("5", 7, 3);

		GAtlasGenerator.addPicture("6", 0, 4);
		GAtlasGenerator.addPicture("7", 1, 4);
		GAtlasGenerator.addPicture("8", 2, 4);
		GAtlasGenerator.addPicture("9", 3, 4);

		GAtlasGenerator.addPicture("double_dots", 15, 7);
		GAtlasGenerator.addPicture("dot_center", 14, 15);

		GAtlasGenerator.addPixelSector("pixel", 1919, 639);

		GAtlasGenerator.saveAtlas();
	}

	public static void generateAtlasObjects()
	{
		GAtlasGenerator.generateBlankAtlas(
			"objects",
			GAtlasGenerator.DEFAULT_ATLAS_SIDE_SIZE,
			GAtlasGenerator.DEFAULT_ATLAS_SIDE_SIZE,
			"Textures/unpacked/OBJECTS/");

		GAtlasGenerator.addPicture("platform", 0, 0);
		GAtlasGenerator.addPicture("body", 1, 0);
		GAtlasGenerator.addPicture("hand", 2, 0);
		GAtlasGenerator.addPicture("head", 6, 0);
		GAtlasGenerator.addPicture("leg", 7, 0);
		GAtlasGenerator.addPicture("arm", 8, 0);
		GAtlasGenerator.addPicture("head_wide", 5, 0);
		GAtlasGenerator.addPicture("claw", 6, 0);
		GAtlasGenerator.addPicture("leg_curve", 7, 0);
		GAtlasGenerator.addPicture("arm_up", 0, 1);
		GAtlasGenerator.addPicture("pelvic", 1, 2);
		GAtlasGenerator.addPicture("hand_up", 2, 1);
		GAtlasGenerator.addPicture("head_round", 5, 1);
		GAtlasGenerator.addPicture("tentacle_top", 6, 1);
		GAtlasGenerator.addPicture("tentacle_middle", 7, 1);
		GAtlasGenerator.addPicture("tentacle_bottom", 0, 2);
		GAtlasGenerator.addPicture("pelvic_tentacle", 1, 2);
		GAtlasGenerator.addPicture("head_spider", 2, 2);
		GAtlasGenerator.addPicture("arm_spider_top", 3, 2);
		GAtlasGenerator.addPicture("arm_spider_middle", 4, 2);


		GAtlasGenerator.addPicture("body_blick", 1, 7-0);
		GAtlasGenerator.addPicture("hand_blick", 2, 7-0);
		GAtlasGenerator.addPicture("head_blick", 6, 7-0);
		GAtlasGenerator.addPicture("leg_blick", 7, 7-0);
		GAtlasGenerator.addPicture("arm_blick", 8, 7-0);
		GAtlasGenerator.addPicture("head_wide_blick", 5, 7-0);
		GAtlasGenerator.addPicture("claw_blick", 6, 7-0);
		GAtlasGenerator.addPicture("leg_curve_blick", 7, 7-0);
		GAtlasGenerator.addPicture("arm_up_blick", 0, 7-1);
		GAtlasGenerator.addPicture("pelvic_blick", 1, 15-2);
		GAtlasGenerator.addPicture("hand_up_blick", 2, 7-1);
		GAtlasGenerator.addPicture("head_round_blick", 5, 7-1);
		GAtlasGenerator.addPicture("tentacle_top_blick", 6, 7-1);
		GAtlasGenerator.addPicture("tentacle_middle_blick", 7, 7-1);
		GAtlasGenerator.addPicture("tentacle_bottom_blick", 0, 7-2);
		GAtlasGenerator.addPicture("pelvic_tentacle_blick", 1, 7-2);
		GAtlasGenerator.addPicture("head_spider_blick", 2, 7-2);
		GAtlasGenerator.addPicture("arm_spider_top_blick", 3, 7-2);
		GAtlasGenerator.addPicture("arm_spider_middle_blick", 4, 7-2);

		GAtlasGenerator.saveAtlas();
	}

	public static void generateAtlasObjectsBlicks()
	{
		GAtlasGenerator.generateBlankAtlas(
			"objects_blicks",
			GAtlasGenerator.DEFAULT_ATLAS_SIDE_SIZE,
			GAtlasGenerator.DEFAULT_ATLAS_SIDE_SIZE,
			"Textures/unpacked/OBJECTS/");

		GAtlasGenerator.addPicture("body_blick", 1, 0);
		GAtlasGenerator.addPicture("hand_blick", 2, 0);
		GAtlasGenerator.addPicture("head_blick", 6, 0);
		GAtlasGenerator.addPicture("leg_blick", 7, 0);
		GAtlasGenerator.addPicture("arm_blick", 8, 0);
		GAtlasGenerator.addPicture("head_wide_blick", 5, 0);
		GAtlasGenerator.addPicture("claw_blick", 6, 0);
		GAtlasGenerator.addPicture("leg_curve_blick", 7, 0);
		GAtlasGenerator.addPicture("arm_up_blick", 0, 1);
		GAtlasGenerator.addPicture("pelvic_blick", 1, 2);
		GAtlasGenerator.addPicture("hand_up_blick", 2, 1);
		GAtlasGenerator.addPicture("arm_2_blick", 3, 1);
		GAtlasGenerator.addPicture("arm_up_2_blick", 4, 1);
		GAtlasGenerator.addPicture("head_round_blick", 5, 1);
		GAtlasGenerator.addPicture("tentacle_top_blick", 6, 1);
		GAtlasGenerator.addPicture("tentacle_middle_blick", 7, 1);
		GAtlasGenerator.addPicture("tentacle_bottom_blick", 0, 2);
		GAtlasGenerator.addPicture("pelvic_tentacle_blick", 1, 2);
		GAtlasGenerator.addPicture("head_spider_blick", 2, 2);
		GAtlasGenerator.addPicture("arm_spider_top_blick", 3, 2);
		GAtlasGenerator.addPicture("arm_spider_middle_blick", 4, 2);

		GAtlasGenerator.saveAtlas();
	}

	public static void generateAtlasInterface()
	{
		GAtlasGenerator.generateBlankAtlas(
			"interface",
			GAtlasGenerator.DEFAULT_ATLAS_SIDE_SIZE,
			GAtlasGenerator.DEFAULT_ATLAS_SIDE_SIZE,
			"Textures/unpacked/INTERFACE/");

		GAtlasGenerator.addPicture("robot_icon", 0, 0);
		GAtlasGenerator.addPicture("robot_icon_blick", 0, 1);
		GAtlasGenerator.addPicture("robot_icon_platform", 1, 0);
		GAtlasGenerator.addPicture("button", 2, 0);
		GAtlasGenerator.addPicture("button_options", 7, 0);

		GAtlasGenerator.addPicture("firefly", 31, 4);


		GAtlasGenerator.saveAtlas();
	}
}