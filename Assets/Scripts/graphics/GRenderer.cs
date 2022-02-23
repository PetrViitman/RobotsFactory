using UnityEngine;

public class GRenderer
{
	private const int CAMERA_HEIGHT_IN_UNITS = 10;
	private const float PERCENTS_PER_UNIT = 100f / GRenderer.CAMERA_HEIGHT_IN_UNITS;

	private static GInteractiveRectangleView aInteractiveRectangleView_girv;
	private static GameObject[] renderableObjects_go_arr = new GameObject[1000];
	private static int totalRenderableObjectsNumber_int = 0;
	private static int lastRenderedObjectIndex_int = -1;
	private static Color color_c = new Color(1, 1, 1, 1);
	private static float zIndex_num;

	public const int TEXT_ALIGN_MODE_ID_LEFT = 0;
	public const int TEXT_ALIGN_MODE_ID_CENTER = 1;
	public const int TEXT_ALIGN_MODE_ID_RIGHT = 2;


	//ASSETS...
	//FONT...
	//LETTERS...
	private static GAtlas ATLAS_FONT = GStorage.getAtlas("font");
	private static GRenderableObjectsPool SPRITE_A = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("A"));
	private static GRenderableObjectsPool SPRITE_B = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("B"));
	private static GRenderableObjectsPool SPRITE_C = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("C"));
	private static GRenderableObjectsPool SPRITE_D = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("D"));
	private static GRenderableObjectsPool SPRITE_E = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("E"));
	private static GRenderableObjectsPool SPRITE_F = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("F"));
	private static GRenderableObjectsPool SPRITE_G = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("G"));
	private static GRenderableObjectsPool SPRITE_H = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("H"));
	private static GRenderableObjectsPool SPRITE_I = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("I"));
	private static GRenderableObjectsPool SPRITE_J = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("J"));
	private static GRenderableObjectsPool SPRITE_K = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("K"));
	private static GRenderableObjectsPool SPRITE_L = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("L"));
	private static GRenderableObjectsPool SPRITE_M = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("M"));
	private static GRenderableObjectsPool SPRITE_N = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("N"));
	private static GRenderableObjectsPool SPRITE_O = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("O"));
	private static GRenderableObjectsPool SPRITE_P = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("P"));
	private static GRenderableObjectsPool SPRITE_Q = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("Q"));
	private static GRenderableObjectsPool SPRITE_R = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("R"));
	private static GRenderableObjectsPool SPRITE_S = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("S"));
	private static GRenderableObjectsPool SPRITE_T = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("T"));
	private static GRenderableObjectsPool SPRITE_U = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("U"));
	private static GRenderableObjectsPool SPRITE_V = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("V"));
	private static GRenderableObjectsPool SPRITE_W = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("W"));
	private static GRenderableObjectsPool SPRITE_X = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("X"));
	private static GRenderableObjectsPool SPRITE_Y = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("Y"));
	private static GRenderableObjectsPool SPRITE_Z = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("Z"));
	//...LETTERS

	//DIGITS...
	private static GRenderableObjectsPool SPRITE_0 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("0"));
	private static GRenderableObjectsPool SPRITE_1 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("1"));
	private static GRenderableObjectsPool SPRITE_2 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("2"));
	private static GRenderableObjectsPool SPRITE_3 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("3"));
	private static GRenderableObjectsPool SPRITE_4 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("4"));
	private static GRenderableObjectsPool SPRITE_5 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("5"));
	private static GRenderableObjectsPool SPRITE_6 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("6"));
	private static GRenderableObjectsPool SPRITE_7 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("7"));
	private static GRenderableObjectsPool SPRITE_8 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("8"));
	private static GRenderableObjectsPool SPRITE_9 = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("9"));
	//...DIGITS

	private static GRenderableObjectsPool SPRITE_DOUBLE_DOTS = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("double_dots"));
	private static GRenderableObjectsPool SPRITE_DOT_CENTER = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("dot_center"));

	private static GRenderableObjectsPool SPRITE_PIXEL = new GRenderableObjectsPool(GRenderer.ATLAS_FONT.getSprite("pixel"));
	//...FONT


	//OBJECTS...
	private static GAtlas ATLAS_OBJECTS = GStorage.getAtlas("objects");
	private static GRenderableObjectsPool SPRITE_PLATFORM = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("platform"));
	private static GRenderableObjectsPool SPRITE_BODY = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("body"));
	private static GRenderableObjectsPool SPRITE_HAND = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("hand"));
	private static GRenderableObjectsPool SPRITE_HEAD = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("head"));
	private static GRenderableObjectsPool SPRITE_LEG = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("leg"));
	private static GRenderableObjectsPool SPRITE_ARM = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("arm"));
	private static GRenderableObjectsPool SPRITE_HEAD_WIDE = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("head_wide"));
	private static GRenderableObjectsPool SPRITE_CLAW = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("claw"));
	private static GRenderableObjectsPool SPRITE_LEG_CURVE = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("leg_curve"));
	private static GRenderableObjectsPool SPRITE_ARM_UP = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("arm_up"));
	private static GRenderableObjectsPool SPRITE_PELVIC = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("pelvic"));
	private static GRenderableObjectsPool SPRITE_HAND_UP = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("hand_up"));
	private static GRenderableObjectsPool SPRITE_HEAD_ROUND = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("head_round"));
	private static GRenderableObjectsPool SPRITE_TENTACLE_TOP = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("tentacle_top"));
	private static GRenderableObjectsPool SPRITE_TENTACLE_MIDDLE = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("tentacle_middle"));
	private static GRenderableObjectsPool SPRITE_TENTACLE_BOTTOM = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("tentacle_bottom"));
	private static GRenderableObjectsPool SPRITE_PELVIC_TENTACLE = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("pelvic_tentacle"));
	private static GRenderableObjectsPool SPRITE_HEAD_SPIDER = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("head_spider"));
	private static GRenderableObjectsPool SPRITE_ARM_SPIDER_TOP = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("arm_spider_top"));
	private static GRenderableObjectsPool SPRITE_ARM_SPIDER_MIDDLE = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("arm_spider_middle"));
	//...OBJECTS

	//OBJECTS BLICKS...
	//private static GAtlas ATLAS_OBJECTS = GStorage.getAtlas("objects_blicks");
	private static GRenderableObjectsPool SPRITE_BODY_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("body_blick"));
	private static GRenderableObjectsPool SPRITE_HAND_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("hand_blick"));
	private static GRenderableObjectsPool SPRITE_HEAD_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("head_blick"));
	private static GRenderableObjectsPool SPRITE_LEG_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("leg_blick"));
	private static GRenderableObjectsPool SPRITE_ARM_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("arm_blick"));
	private static GRenderableObjectsPool SPRITE_HEAD_WIDE_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("head_wide_blick"));
	private static GRenderableObjectsPool SPRITE_CLAW_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("claw_blick"));
	private static GRenderableObjectsPool SPRITE_LEG_CURVE_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("leg_curve_blick"));
	private static GRenderableObjectsPool SPRITE_ARM_UP_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("arm_up_blick"));
	private static GRenderableObjectsPool SPRITE_PELVIC_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("pelvic_blick"));
	private static GRenderableObjectsPool SPRITE_HAND_UP_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("hand_up_blick"));
	private static GRenderableObjectsPool SPRITE_HEAD_ROUND_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("head_round_blick"));
	private static GRenderableObjectsPool SPRITE_TENTACLE_TOP_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("tentacle_top_blick"));
	private static GRenderableObjectsPool SPRITE_TENTACLE_MIDDLE_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("tentacle_middle_blick"));
	private static GRenderableObjectsPool SPRITE_TENTACLE_BOTTOM_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("tentacle_bottom_blick"));
	private static GRenderableObjectsPool SPRITE_PELVIC_TENTACLE_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("pelvic_tentacle_blick"));
	private static GRenderableObjectsPool SPRITE_HEAD_SPIDER_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("head_spider_blick"));
	private static GRenderableObjectsPool SPRITE_ARM_SPIDER_TOP_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("arm_spider_top_blick"));
	private static GRenderableObjectsPool SPRITE_ARM_SPIDER_MIDDLE_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_OBJECTS.getSprite("arm_spider_middle_blick"));
	//...OBJECTS BLICKS

	//INTERFACE...
	private static GAtlas ATLAS_INTERFACE = GStorage.getAtlas("interface");
	private static GRenderableObjectsPool SPRITE_ROBOT_ICON_PLATFORM = new GRenderableObjectsPool(GRenderer.ATLAS_INTERFACE.getSprite("robot_icon_platform"));
	private static GRenderableObjectsPool SPRITE_ROBOT_ICON = new GRenderableObjectsPool(GRenderer.ATLAS_INTERFACE.getSprite("robot_icon"));
	private static GRenderableObjectsPool SPRITE_ROBOT_ICON_BLICK = new GRenderableObjectsPool(GRenderer.ATLAS_INTERFACE.getSprite("robot_icon_blick"));
	private static GRenderableObjectsPool SPRITE_BUTTON = new GRenderableObjectsPool(GRenderer.ATLAS_INTERFACE.getSprite("button"));
	private static GRenderableObjectsPool SPRITE_BUTTON_OPTIONS = new GRenderableObjectsPool(GRenderer.ATLAS_INTERFACE.getSprite("button_options"));
	private static GRenderableObjectsPool SPRITE_FIREFLY = new GRenderableObjectsPool(GRenderer.ATLAS_INTERFACE.getSprite("firefly"));
	//...INTERFACE

	private static GRenderableObjectsPool[] SPRITE_POOLS =
	{
		GRenderer.SPRITE_A,
		GRenderer.SPRITE_B,
		GRenderer.SPRITE_C,
		GRenderer.SPRITE_D,
		GRenderer.SPRITE_E,
		GRenderer.SPRITE_F,
		GRenderer.SPRITE_G,
		GRenderer.SPRITE_H,
		GRenderer.SPRITE_I,
		GRenderer.SPRITE_J,
		GRenderer.SPRITE_K,
		GRenderer.SPRITE_L,
		GRenderer.SPRITE_M,
		GRenderer.SPRITE_N,
		GRenderer.SPRITE_O,
		GRenderer.SPRITE_P,
		GRenderer.SPRITE_Q,
		GRenderer.SPRITE_R,
		GRenderer.SPRITE_S,
		GRenderer.SPRITE_T,
		GRenderer.SPRITE_U,
		GRenderer.SPRITE_V,
		GRenderer.SPRITE_W,
		GRenderer.SPRITE_X,
		GRenderer.SPRITE_Y,
		GRenderer.SPRITE_Z,
		GRenderer.SPRITE_0,
		GRenderer.SPRITE_1,
		GRenderer.SPRITE_2,
		GRenderer.SPRITE_3,
		GRenderer.SPRITE_4,
		GRenderer.SPRITE_5,
		GRenderer.SPRITE_6,
		GRenderer.SPRITE_7,
		GRenderer.SPRITE_8,
		GRenderer.SPRITE_9,
		GRenderer.SPRITE_DOUBLE_DOTS,
		GRenderer.SPRITE_DOT_CENTER,
		GRenderer.SPRITE_PIXEL,

		GRenderer.SPRITE_PLATFORM,
		GRenderer.SPRITE_BODY,
		GRenderer.SPRITE_HAND,
		GRenderer.SPRITE_HEAD,
		GRenderer.SPRITE_LEG,
		GRenderer.SPRITE_ARM,
		GRenderer.SPRITE_HEAD_WIDE,
		GRenderer.SPRITE_CLAW,
		GRenderer.SPRITE_LEG_CURVE,
		GRenderer.SPRITE_ARM_UP,
		GRenderer.SPRITE_PELVIC,
		GRenderer.SPRITE_HAND_UP,
		GRenderer.SPRITE_HEAD_ROUND,
		GRenderer.SPRITE_TENTACLE_TOP,
		GRenderer.SPRITE_TENTACLE_MIDDLE,
		GRenderer.SPRITE_TENTACLE_BOTTOM,
		GRenderer.SPRITE_PELVIC_TENTACLE,
		GRenderer.SPRITE_HEAD_SPIDER,
		GRenderer.SPRITE_ARM_SPIDER_TOP,
		GRenderer.SPRITE_ARM_SPIDER_MIDDLE,

		GRenderer.SPRITE_BODY_BLICK,
		GRenderer.SPRITE_HAND_BLICK,
		GRenderer.SPRITE_HEAD_BLICK,
		GRenderer.SPRITE_LEG_BLICK,
		GRenderer.SPRITE_ARM_BLICK,
		GRenderer.SPRITE_HEAD_WIDE_BLICK,
		GRenderer.SPRITE_CLAW_BLICK,
		GRenderer.SPRITE_LEG_CURVE_BLICK,
		GRenderer.SPRITE_ARM_UP_BLICK,
		GRenderer.SPRITE_PELVIC_BLICK,
		GRenderer.SPRITE_HAND_UP_BLICK,
		GRenderer.SPRITE_HEAD_ROUND_BLICK,
		GRenderer.SPRITE_TENTACLE_TOP_BLICK,
		GRenderer.SPRITE_TENTACLE_MIDDLE_BLICK,
		GRenderer.SPRITE_TENTACLE_BOTTOM_BLICK,
		GRenderer.SPRITE_PELVIC_TENTACLE_BLICK,
		GRenderer.SPRITE_HEAD_SPIDER_BLICK,
		GRenderer.SPRITE_ARM_SPIDER_TOP_BLICK,
		GRenderer.SPRITE_ARM_SPIDER_MIDDLE_BLICK,

		GRenderer.SPRITE_ROBOT_ICON,
		GRenderer.SPRITE_ROBOT_ICON_BLICK,
		GRenderer.SPRITE_ROBOT_ICON_PLATFORM,
		GRenderer.SPRITE_BUTTON,
		GRenderer.SPRITE_BUTTON_OPTIONS,
		GRenderer.SPRITE_FIREFLY
	};
	//...ASSETS

	private static GameObject getNextRenderableObject(GRenderableObjectsPool aObjectsPool_grop)
	{
		GRenderer.lastRenderedObjectIndex_int++;
		GameObject gameObject_go = aObjectsPool_grop.getNextRenderableObject();// GRenderer.renderableObjects_go_arr[GRenderer.lastRenderedObjectIndex_int];

		if(gameObject_go == null)
		{
			//NEW RENDERABLE OBJECT CREATION...
			gameObject_go = new GameObject();
			gameObject_go.AddComponent<SpriteRenderer>();
			SpriteRenderer spriteRenderer_sr = gameObject_go.GetComponent<SpriteRenderer>();
			spriteRenderer_sr.sprite = aObjectsPool_grop.getSprite();
			//...NEW RENDERABLE OBJECT CREATION

			//ADDING NEW OBJECT TO ARRAYS...
			aObjectsPool_grop.addObject(gameObject_go);

			GRenderer.renderableObjects_go_arr[GRenderer.totalRenderableObjectsNumber_int] = gameObject_go;
			GRenderer.totalRenderableObjectsNumber_int++;
			//...ADDING NEW OBJECT TO ARRAYS
		}

		gameObject_go.GetComponent<Renderer>().enabled = true;

		return gameObject_go;
	}

	public static void setColor(int aRed_int, int aGreen_int, int aBlue_int, float aAlpha_num)
	{
		GRenderer.color_c = new Color(aRed_int / 255f, aGreen_int / 255f, aBlue_int / 255f, aAlpha_num);
	}

	public static void setColor(int aRed_int, int aGreen_int, int aBlue_int)
	{
		GRenderer.setColor(aRed_int, aGreen_int, aBlue_int, 1f);
	}

	public static void setColor(GColor aColor_gc)
	{
		GRenderer.setColor(aColor_gc.getRed(), aColor_gc.getGreen(), aColor_gc.getBlue(), 1f);
	}

	public static void setColor(GColor aColor_gc, float aAlpha_num)
	{
		GRenderer.setColor(aColor_gc.getRed(), aColor_gc.getGreen(), aColor_gc.getBlue(), aAlpha_num);
	}

	public static void dropColor()
	{
		GRenderer.setColor(255, 255, 255);
	}

	public static void clearScreen()
	{
		for(int i = 0; i < GRenderer.totalRenderableObjectsNumber_int; i++)
		{
			GRenderer.renderableObjects_go_arr[i].GetComponent<Renderer>().enabled = false;
		}

		GRenderer.lastRenderedObjectIndex_int = -1;
		GRenderer.zIndex_num = 0f;

		for(int i = 0; i < GRenderer.SPRITE_POOLS.Length; i++)
		{
			GRenderer.SPRITE_POOLS[i].drop();
		}
	}

	public static void fillRect(GRenderableObjectsPool aRenderableObjectsPool_grop, float aX_num, float aY_num, float aWidth_num, float aHeight_num)
	{//89 31 oben //22 85 unter
		GameObject gameObject_go = GRenderer.getNextRenderableObject(aRenderableObjectsPool_grop);
		SpriteRenderer spriteRenderer_sr = gameObject_go.GetComponent<SpriteRenderer>();
		float actualWidthInPercents = spriteRenderer_sr.bounds.size.x * GRenderer.PERCENTS_PER_UNIT;
		float actualHeightInPercents = spriteRenderer_sr.bounds.size.y * GRenderer.PERCENTS_PER_UNIT;

		float scaleFactorX_num = aWidth_num / actualWidthInPercents / GScreen.getSidesRatio();
		float scaleFactorY_num = aHeight_num / actualHeightInPercents;

		spriteRenderer_sr.flipX = (scaleFactorX_num < 0f);
		spriteRenderer_sr.flipY = (scaleFactorY_num < 0f);

		scaleFactorX_num = Mathf.Abs(scaleFactorX_num);
		scaleFactorY_num = Mathf.Abs(scaleFactorY_num);

		gameObject_go.transform.position = new Vector3(
			(aX_num / GRenderer.CAMERA_HEIGHT_IN_UNITS - GRenderer.CAMERA_HEIGHT_IN_UNITS / 2f) / GScreen.getSidesRatio(), 
			GRenderer.CAMERA_HEIGHT_IN_UNITS - aY_num / GRenderer.CAMERA_HEIGHT_IN_UNITS - GRenderer.CAMERA_HEIGHT_IN_UNITS / 2f,
			GRenderer.zIndex_num);
		
		gameObject_go.transform.localScale =  new Vector2(
			gameObject_go.transform.localScale.x * scaleFactorX_num, 
			gameObject_go.transform.localScale.y * scaleFactorY_num);

		spriteRenderer_sr.color = GRenderer.color_c;

		GRenderer.zIndex_num -= 0.01f;
  	}

	public static void fillRectCenter(GRenderableObjectsPool aRenderableObjectsPool_grop, float aX_num, float aY_num, float aWidth_num, float aHeight_num)
  	{
		GRenderer.fillRect(
			aRenderableObjectsPool_grop,
			aX_num - aWidth_num / 2,
			aY_num - aHeight_num / 2,
			aWidth_num,
			aHeight_num);
	}

	private static float getSymbolTextureScaleFactorX(char aSymbol_char)
	{
		switch (char.ToUpper(aSymbol_char))
		{
			case ':':	return 0.5f;
			default:	return 1;
		}
	}

	private static GRenderableObjectsPool getSymbolSpritesPool(char aSymbol_char)
	{
		switch (char.ToUpper(aSymbol_char))
		{
			case 'A': return GRenderer.SPRITE_A;
			case 'B': return GRenderer.SPRITE_B;
			case 'C': return GRenderer.SPRITE_C;
			case 'D': return GRenderer.SPRITE_D;
			case 'E': return GRenderer.SPRITE_E;
			case 'F': return GRenderer.SPRITE_F;
			case 'G': return GRenderer.SPRITE_G;
			case 'H': return GRenderer.SPRITE_H;
			case 'I': return GRenderer.SPRITE_I;
			case 'J': return GRenderer.SPRITE_J;
			case 'K': return GRenderer.SPRITE_K;
			case 'L': return GRenderer.SPRITE_L;
			case 'M': return GRenderer.SPRITE_M;
			case 'N': return GRenderer.SPRITE_N;
			case 'O': return GRenderer.SPRITE_O;
			case 'P': return GRenderer.SPRITE_P;
			case 'Q': return GRenderer.SPRITE_Q;
			case 'R': return GRenderer.SPRITE_R;
			case 'S': return GRenderer.SPRITE_S;
			case 'T': return GRenderer.SPRITE_T;
			case 'U': return GRenderer.SPRITE_U;
			case 'V': return GRenderer.SPRITE_V;
			case 'W': return GRenderer.SPRITE_W;
			case 'X': return GRenderer.SPRITE_X;
			case 'Y': return GRenderer.SPRITE_Y;
			case 'Z': return GRenderer.SPRITE_Z;
			case '0': return GRenderer.SPRITE_0;
			case '1': return GRenderer.SPRITE_1;
			case '2': return GRenderer.SPRITE_2;
			case '3': return GRenderer.SPRITE_3;
			case '4': return GRenderer.SPRITE_4;
			case '5': return GRenderer.SPRITE_5;
			case '6': return GRenderer.SPRITE_6;
			case '7': return GRenderer.SPRITE_7;
			case '8': return GRenderer.SPRITE_8;
			case '9': return GRenderer.SPRITE_9;
			case ':': return GRenderer.SPRITE_DOUBLE_DOTS;
			case '*': return GRenderer.SPRITE_DOT_CENTER;
			default: return null;
		}
	}

	private static float getSymbolSpacing(char aSymbol_char)
	{
		switch (char.ToUpper(aSymbol_char))
		{
			case '1':
			case 'I':	return 0.75f;
			case 'M':
			case 'W':	return 1.24f;
			case 'X':	return 0.81f;
			case '*':	return 1f;
			case ':':	return 0.35f;
			case ' ':	return 0.5f;
			default:	return 1;
		}
	}

	private static float getTextTotalSpacing(string aText_str)
	{
		float textSpacing_num = 0;

		for( int i = 0; i < aText_str.Length; i++ )
		{
			textSpacing_num += GRenderer.getSymbolSpacing(aText_str[i]);
		}

		return textSpacing_num;
	}

	public static void drawText(string aText_str, float aX_num, float aY_num, float aScale_num, int aAlignModeId_int)
	{
		float sidesRatio_num = GScreen.getSidesRatio();

		float textTotalSpacing_num = GRenderer.getTextTotalSpacing(aText_str) * sidesRatio_num * aScale_num;

		float x_num = aX_num;
		float y_num = aY_num;

		switch(aAlignModeId_int)
		{
			case GRenderer.TEXT_ALIGN_MODE_ID_CENTER:
			{
				x_num -= textTotalSpacing_num / 2;
			}
			break;
			case GRenderer.TEXT_ALIGN_MODE_ID_RIGHT:
			{
				x_num -= textTotalSpacing_num;
			}
			break;
		}

		for( int i = 0; i < aText_str.Length; i++ )
		{	
			char symbol = aText_str[i];
			GRenderableObjectsPool pool_grop = GRenderer.getSymbolSpritesPool(symbol);
			float spacing_num = GRenderer.getSymbolSpacing(symbol) * sidesRatio_num * aScale_num * 0.5f;
			
			x_num += spacing_num;

			if(pool_grop != null)
			{
				GRenderer.fillRectCenter(
					pool_grop,
					x_num,
					y_num,
					aScale_num * sidesRatio_num * GRenderer.getSymbolTextureScaleFactorX(symbol),
					aScale_num);
			}

			x_num += spacing_num;
		}
	}

	private static float getTextScaleCorrection(string aText_str, float aScale_num, float aMaximalWidth_num, float aPadding_num)
	{
		float scale_num = aScale_num;
		float textTotalSpacing_num = 
			GRenderer.getTextTotalSpacing(aText_str) * 
			GScreen.getSidesRatio() * 
			aScale_num;

		float maximalWidth_num = aMaximalWidth_num - aPadding_num * 2;

		if(textTotalSpacing_num > maximalWidth_num)
		{
			scale_num = aScale_num * maximalWidth_num / textTotalSpacing_num;
		}

		return scale_num;
	}

	private static float getTextScaleCorrection(string aText_str, float aScale_num, float aMaximalWidth_num)
	{
		return GRenderer.getTextScaleCorrection(aText_str, aScale_num, aMaximalWidth_num, 0f);
	}

	public static void drawTextIntoWidth(string aText_str, float aX_num, float aY_num, float aMaximalWidth_num, float aScale_num, float aPadding_num)
	{
		float scale_num = GRenderer.getTextScaleCorrection(aText_str, aScale_num, aMaximalWidth_num, aPadding_num);

		GRenderer.drawText(
			aText_str,
			aX_num + ( aMaximalWidth_num / 2),
			aY_num,
			scale_num,
			GRenderer.TEXT_ALIGN_MODE_ID_CENTER);
	}

	public static void drawRobotHeadView(GRobotHeadView aRobotHeadView_grhv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HEAD,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.04f,
			width_num * 0.475f,
			height_num * 0.475f * 2f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HEAD_BLICK,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.04f,
			width_num * 0.475f,
			height_num * 0.475f * 2f);
	}

	public static void drawRobotBodyView(GRobotBodyView aRobotBodyView_grbv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{	
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_BODY,
			aOffsetX_num,
			aOffsetY_num - height_num * 0.05f,
			width_num * 0.935f,
			height_num * 0.935f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_BODY_BLICK,
			aOffsetX_num,
			aOffsetY_num - height_num * 0.05f,
			width_num * 0.935f,
			height_num * 0.935f);
	}

	public static void drawRobotLegsView(GRobotLegsView aRobotLegsView_grlv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();
		float scaleFactor_num = 0.4f;

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_LEG,
			aOffsetX_num + width_num * scaleFactor_num * 0.55f,
			aOffsetY_num - height_num * scaleFactor_num * 0.35f,
			width_num * scaleFactor_num,
			height_num * scaleFactor_num * 2f);

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_LEG,
			aOffsetX_num - width_num * scaleFactor_num * 0.55f,
			aOffsetY_num - height_num * scaleFactor_num * 0.35f,
			width_num * scaleFactor_num,
			height_num * scaleFactor_num * 2f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_LEG_BLICK,
			aOffsetX_num + width_num * scaleFactor_num * 0.55f,
			aOffsetY_num - height_num * scaleFactor_num * 0.35f,
			width_num * scaleFactor_num,
			height_num * scaleFactor_num * 2f);

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_LEG_BLICK,
			aOffsetX_num - width_num * scaleFactor_num * 0.55f,
			aOffsetY_num - height_num * scaleFactor_num * 0.35f,
			width_num * scaleFactor_num,
			height_num * scaleFactor_num * 2f);
	}

	public static void drawRobotArmLeftView(GRobotArmLeftView aRobotArmLeftView_gralv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM,
			aOffsetX_num + width_num * 0.125f,
			aOffsetY_num,
			width_num * 0.95f * 0.5f,
			height_num * 0.95f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_BLICK,
			aOffsetX_num + width_num * 0.125f,
			aOffsetY_num,
			width_num * 0.95f * 0.5f,
			height_num * 0.95f);
	}

	public static void drawRobotArmRightView(GRobotArmRightView aRobotArmRightView_grarv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = -aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM,
			aOffsetX_num + width_num * 0.125f,
			aOffsetY_num,
			width_num * 0.95f * 0.5f,
			height_num * 0.95f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_BLICK,
			aOffsetX_num + width_num * 0.125f,
			aOffsetY_num,
			width_num * 0.95f * 0.5f,
			height_num * 0.95f);
	}

	public static void drawRobotFistLeftView(GRobotFistLeftView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HAND,
			aOffsetX_num + width_num * 0.085f,
			aOffsetY_num - height_num * 0.225f,
			width_num * 0.6f,
			height_num * 0.6f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HAND_BLICK,
			aOffsetX_num + width_num * 0.085f,
			aOffsetY_num - height_num * 0.225f,
			width_num * 0.6f,
			height_num * 0.6f);
	}

	public static void drawRobotFistRightView(GRobotFistRightView aRobotFistRightView_grfrv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = -aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HAND,
			aOffsetX_num + width_num * 0.085f,
			aOffsetY_num - height_num * 0.225f,
			width_num * 0.6f,
			height_num * 0.6f);


		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HAND_BLICK,
			aOffsetX_num + width_num * 0.085f,
			aOffsetY_num - height_num * 0.225f,
			width_num * 0.6f,
			height_num * 0.6f);

	}

	public static void drawRobotClawView(GRobotClawView aRobotBodyView_grbv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{	
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_CLAW,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.175f,
			width_num * 0.7515f,
			height_num * 0.7515f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_CLAW_BLICK,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.175f,
			width_num * 0.7515f,
			height_num * 0.7515f);
	}

	public static void drawRobotHeadWideView(GRobotHeadWideView aRobotHeadView_grhv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HEAD_WIDE,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.07f,
			width_num * 0.81f,
			height_num * 0.81f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HEAD_WIDE_BLICK,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.07f,
			width_num * 0.81f,
			height_num * 0.81f);
	}

	public static void drawRobotPelvicView(GRobotPelvicView aRobotHeadView_grhv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_PELVIC,
			aOffsetX_num,
			aOffsetY_num - height_num * 0.25f,
			width_num * 0.975f,
			height_num * 0.975f * 0.5f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_PELVIC_BLICK,
			aOffsetX_num,
			aOffsetY_num - height_num * 0.25f,
			width_num * 0.975f,
			height_num * 0.975f * 0.5f);
	}

	public static void drawRobotLegCurveLeftView(GRobotLegCurveLeftView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_LEG_CURVE,
			aOffsetX_num + width_num * 0.175f,
			aOffsetY_num - height_num * 0.08f,
			width_num * 0.825f,
			height_num * 0.825f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_LEG_CURVE_BLICK,
			aOffsetX_num + width_num * 0.175f,
			aOffsetY_num - height_num * 0.08f,
			width_num * 0.825f,
			height_num * 0.825f);
	}

	public static void drawRobotLegCurveRightView(GRobotLegCurveRightView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_LEG_CURVE,
			aOffsetX_num - width_num * 0.175f,
			aOffsetY_num - height_num * 0.08f,
			width_num * -0.825f,
			height_num * 0.825f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_LEG_CURVE_BLICK,
			aOffsetX_num - width_num * 0.175f,
			aOffsetY_num - height_num * 0.08f,
			width_num * -0.825f,
			height_num * 0.825f);
	}

	public static void drawRobotArmUpLeftView(GRobotArmUpLeftView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_UP,
			aOffsetX_num + width_num * 0.155f,
			aOffsetY_num - height_num * 0.135f,
			width_num * 0.7075f,
			height_num * 0.7075f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_UP_BLICK,
			aOffsetX_num + width_num * 0.155f,
			aOffsetY_num - height_num * 0.135f,
			width_num * 0.7075f,
			height_num * 0.7075f);
	}

	public static void drawRobotArmUpRightView(GRobotArmUpRightView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_UP,
			aOffsetX_num - width_num * 0.155f,
			aOffsetY_num - height_num * 0.135f,
			width_num * -0.7075f,
			height_num * 0.7075f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_UP_BLICK,
			aOffsetX_num - width_num * 0.155f,
			aOffsetY_num - height_num * 0.135f,
			width_num * -0.7075f,
			height_num * 0.7075f);
	}

	public static void drawRobotHandUpView(GRobotHandUpView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HAND_UP,
			aOffsetX_num + width_num * 0.0825f,
			aOffsetY_num + height_num * 0.1725f,
			width_num * 0.65f,
			height_num * 0.65f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HAND_UP_BLICK,
			aOffsetX_num + width_num * 0.0825f,
			aOffsetY_num + height_num * 0.1725f,
			width_num * 0.65f,
			height_num * 0.65f);
	}

	public static void drawRobotHeadRoundView(GRobotHeadRoundView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HEAD_ROUND,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.04f,
			width_num * 0.875f,
			height_num * 0.875f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HEAD_ROUND_BLICK,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.04f,
			width_num * 0.875f,
			height_num * 0.875f);
	}

	public static void drawRobotTentacleTopLeftView(GRobotTentacleTopLeftView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_TOP,
			aOffsetX_num + width_num * 0.0225f,
			aOffsetY_num + height_num * 0.015f,
			width_num * 0.925f,
			height_num * 0.925f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_TOP_BLICK,
			aOffsetX_num + width_num * 0.0225f,
			aOffsetY_num + height_num * 0.015f,
			width_num * 0.925f,
			height_num * 0.925f);
	}

	public static void drawRobotTentacleTopRightView(GRobotTentacleTopRightView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_TOP,
			aOffsetX_num - width_num * 0.0225f,
			aOffsetY_num + height_num * 0.015f,
			width_num * -0.925f,
			height_num * 0.925f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_TOP_BLICK,
			aOffsetX_num - width_num * 0.0225f,
			aOffsetY_num + height_num * 0.015f,
			width_num * -0.925f,
			height_num * 0.925f);
	}

	public static void drawRobotTentacleMiddleLeftView(GRobotTentacleMiddleLeftView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_MIDDLE,
			aOffsetX_num,
			aOffsetY_num - height_num * 0.03f,
			width_num * 0.825f,
			height_num * 0.825f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_MIDDLE_BLICK,
			aOffsetX_num + width_num * 0f,
			aOffsetY_num - height_num * 0.03f,
			width_num * 0.825f,
			height_num * 0.825f);
	}

	public static void drawRobotTentacleMiddleRightView(GRobotTentacleMiddleRightView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_MIDDLE,
			aOffsetX_num,
			aOffsetY_num - height_num * 0.03f,
			width_num * -0.825f,
			height_num * 0.825f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_MIDDLE_BLICK,
			aOffsetX_num - width_num * 0f,
			aOffsetY_num - height_num * 0.03f,
			width_num * -0.825f,
			height_num * 0.825f);
	}

	public static void drawRobotPelvicTentacleView(GRobotPelvicTentacleView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_PELVIC_TENTACLE,
			aOffsetX_num,
			aOffsetY_num - height_num * 0.03f,
			width_num * 0.96f,
			height_num * 0.96f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_PELVIC_TENTACLE_BLICK,
			aOffsetX_num,
			aOffsetY_num - height_num * 0.03f,
			width_num * 0.96f,
			height_num * 0.96f);
	}

	public static void drawRobotTentacleBottomLeftView(GRobotTentacleBottomLeftView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_BOTTOM,
			aOffsetX_num + width_num * 0.0225f,
			aOffsetY_num - height_num * 0.03f,
			width_num * 0.975f,
			height_num * 0.975f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_BOTTOM_BLICK,
			aOffsetX_num + width_num * 0.0225f,
			aOffsetY_num - height_num * 0.03f,
			width_num * 0.975f,
			height_num * 0.975f);
	}

	public static void drawRobotTentacleBottomRightView(GRobotTentacleBottomRightView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_BOTTOM,
			aOffsetX_num - width_num * 0.0225f,
			aOffsetY_num - height_num * 0.03f,
			width_num * -0.975f,
			height_num * 0.975f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_TENTACLE_BOTTOM_BLICK,
			aOffsetX_num - width_num * 0.0225f,
			aOffsetY_num - height_num * 0.03f,
			width_num * -0.975f,
			height_num * 0.975f);
	}

	public static void drawRobotHeadSpiderView(GRobotHeadSpiderView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HEAD_SPIDER,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.05f,
			width_num * 0.85f,
			height_num * 0.85f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_HEAD_SPIDER_BLICK,
			aOffsetX_num,
			aOffsetY_num + height_num * 0.05f,
			width_num * 0.85f,
			height_num * 0.85f);
	}

	public static void drawRobotArmSpiderMiddleLeftView(GRobotArmSpiderMiddleLeftView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_SPIDER_MIDDLE,
			aOffsetX_num + width_num * 0.025f,
			aOffsetY_num - height_num * 0.01f,
			width_num * 1f,
			height_num * 1f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_SPIDER_MIDDLE_BLICK,
			aOffsetX_num + width_num * 0.025f,
			aOffsetY_num - height_num * 0.01f,
			width_num * 1f,
			height_num * 1f);
	}

	public static void drawRobotArmSpiderMiddleRightView(GRobotArmSpiderMiddleRightView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_SPIDER_MIDDLE,
			aOffsetX_num - width_num * 0.025f,
			aOffsetY_num - height_num * 0.01f,
			width_num * -1f,
			height_num * 1f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_SPIDER_MIDDLE_BLICK,
			aOffsetX_num - width_num * 0.025f,
			aOffsetY_num - height_num * 0.01f,
			width_num * -1f,
			height_num * 1f);
	}

	public static void drawRobotArmSpiderTopLeftView(GRobotArmSpiderTopLeftView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_SPIDER_TOP,
			aOffsetX_num + width_num * 0.025f,
			aOffsetY_num - height_num * 0.01f,
			width_num * 1f,
			height_num * 1f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_SPIDER_TOP_BLICK,
			aOffsetX_num + width_num * 0.025f,
			aOffsetY_num - height_num * 0.01f,
			width_num * 1f,
			height_num * 1f);
	}

	public static void drawRobotArmSpiderTopRightView(GRobotArmSpiderTopRightView aRobotFistLeftView_grflv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		float width_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float height_num = aCellView_gcv.getHeight();

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_SPIDER_TOP,
			aOffsetX_num - width_num * 0.025f,
			aOffsetY_num - height_num * 0.01f,
			width_num * -1f,
			height_num * 1f);

		GRenderer.setColor(255,255,255, 0.26f);
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ARM_SPIDER_TOP_BLICK,
			aOffsetX_num - width_num * 0.025f,
			aOffsetY_num - height_num * 0.01f,
			width_num * -1f,
			height_num * 1f);
	}

	//drawRobotArmSpiderMiddleTopView

	public static void drawRobotDetailView(GRobotDetailView aRobotDetailView_grdv, float aOffsetX_num, float aOffsetY_num, GCellView aCellView_gcv)
	{
		//153 ... 136 75 49		114
		//100% ... 88.88% 49% 32% 	74.5%
		//255 ... 226 125 81		190

		GRenderer.setColor(GGameView.getInterfaceColor());

		if(aRobotDetailView_grdv is GRobotHeadView)
		{
			GRenderer.drawRobotHeadView((GRobotHeadView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotBodyView)
		{
			GRenderer.drawRobotBodyView((GRobotBodyView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotLegsView)
		{
			GRenderer.drawRobotLegsView((GRobotLegsView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotArmLeftView)
		{
			GRenderer.drawRobotArmLeftView((GRobotArmLeftView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotArmRightView)
		{
			GRenderer.drawRobotArmRightView((GRobotArmRightView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotFistLeftView)
		{
			GRenderer.drawRobotFistLeftView((GRobotFistLeftView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotFistRightView)
		{
			GRenderer.drawRobotFistRightView((GRobotFistRightView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotClawView)
		{
			GRenderer.drawRobotClawView((GRobotClawView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotHeadWideView)
		{
			GRenderer.drawRobotHeadWideView((GRobotHeadWideView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotPelvicView)
		{
			GRenderer.drawRobotPelvicView((GRobotPelvicView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotLegCurveLeftView)
		{
			GRenderer.drawRobotLegCurveLeftView((GRobotLegCurveLeftView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotLegCurveRightView)
		{
			GRenderer.drawRobotLegCurveRightView((GRobotLegCurveRightView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotArmUpLeftView)
		{
			GRenderer.drawRobotArmUpLeftView((GRobotArmUpLeftView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotArmUpRightView)
		{
			GRenderer.drawRobotArmUpRightView((GRobotArmUpRightView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotHandUpView)
		{
			GRenderer.drawRobotHandUpView((GRobotHandUpView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotHeadRoundView)
		{
			GRenderer.drawRobotHeadRoundView((GRobotHeadRoundView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotTentacleTopLeftView)
		{
			GRenderer.drawRobotTentacleTopLeftView((GRobotTentacleTopLeftView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotTentacleTopRightView)
		{
			GRenderer.drawRobotTentacleTopRightView((GRobotTentacleTopRightView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotTentacleMiddleLeftView)
		{
			GRenderer.drawRobotTentacleMiddleLeftView((GRobotTentacleMiddleLeftView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotTentacleMiddleRightView)
		{
			GRenderer.drawRobotTentacleMiddleRightView((GRobotTentacleMiddleRightView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotPelvicTentacleView)
		{
			GRenderer.drawRobotPelvicTentacleView((GRobotPelvicTentacleView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotTentacleBottomLeftView)
		{
			GRenderer.drawRobotTentacleBottomLeftView((GRobotTentacleBottomLeftView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotTentacleBottomRightView)
		{
			GRenderer.drawRobotTentacleBottomRightView((GRobotTentacleBottomRightView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotHeadSpiderView)
		{
			GRenderer.drawRobotHeadSpiderView((GRobotHeadSpiderView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotArmSpiderMiddleLeftView)
		{
			GRenderer.drawRobotArmSpiderMiddleLeftView((GRobotArmSpiderMiddleLeftView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotArmSpiderMiddleRightView)
		{
			GRenderer.drawRobotArmSpiderMiddleRightView((GRobotArmSpiderMiddleRightView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotArmSpiderTopLeftView)
		{
			GRenderer.drawRobotArmSpiderTopLeftView((GRobotArmSpiderTopLeftView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}
		else if(aRobotDetailView_grdv is GRobotArmSpiderTopRightView)
		{
			GRenderer.drawRobotArmSpiderTopRightView((GRobotArmSpiderTopRightView)aRobotDetailView_grdv, aOffsetX_num, aOffsetY_num, aCellView_gcv);
		}

		GRenderer.setColor(255, 255, 255);
	}

	public static void drawCellView(GCellView aCellView_gcv, float aX_num, float aY_num)
	{
		if(aCellView_gcv.isActive())
		{
			GRenderer.setColor(255, 200, 200);
		}
		else
		{
			GRenderer.setColor(255, 255, 255);
		}

		float cellWidth_num = aCellView_gcv.getWidth() * GScreen.getSidesRatio();
		float cellHeight_num = aCellView_gcv.getHeight();

		float x_num = aX_num;
		float y_num = aY_num + aCellView_gcv.getFloatingHeightValue() * cellHeight_num * 0.035f;

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_PLATFORM,
			x_num + cellWidth_num * 0.5f,
			y_num + cellHeight_num * 0.5f,
			cellWidth_num * 0.95f, 
			cellHeight_num * 0.95f);

		GRenderer.setColor(255, 255, 255);

		GRenderer.drawRobotDetailView(
			aCellView_gcv.getRobotDetailView(),
			x_num + cellWidth_num / 2f,
			y_num + cellHeight_num / 2f,
			aCellView_gcv);
	}

	public static void drawCellView(GCellView aCellView_gcv)
	{
		GRenderer.drawCellView(aCellView_gcv, aCellView_gcv.getX(), aCellView_gcv.getY());
	}

	public static void tileGridViewActiveRow(GGridView aGridView_ggv)
	{
		float cellWidth_num = aGridView_ggv.getActiveCell().getWidth() * GScreen.getSidesRatio();
		int gridWidth_int = aGridView_ggv.getColumnsNumber();
		int activeRowIndex_int = aGridView_ggv.getActiveRowIndex();

		//RIGHT...
		float x_num = aGridView_ggv.getActiveRowRightBorderX();
		int i = 0;

		while(x_num < 100)
		{
			GCellView cellView_gcv = aGridView_ggv.getCellViews()[activeRowIndex_int][i];

			GRenderer.drawCellView(
				cellView_gcv, 
				x_num, 
				cellView_gcv.getY());

			x_num += cellWidth_num;
			i++;

			if(i >= gridWidth_int)
			{
				i = 0;
			}
		}
		//...RIGHT
		
		//LEFT...
		x_num = aGridView_ggv.getActiveRowLeftBorderX() - cellWidth_num;
		i = gridWidth_int -1;

		while(x_num > -cellWidth_num)
		{
			GCellView cellView_gcv = aGridView_ggv.getCellViews()[activeRowIndex_int][i];

			GRenderer.drawCellView(
				cellView_gcv, 
				x_num, 
				cellView_gcv.getY());

			x_num -= cellWidth_num;
			i--;

			if(i < 0)
			{
				i = gridWidth_int -1;
			}
		}
		//...LEFT
	}

	public static void tileGridViewActiveColumn(GGridView aGridView_ggv)
	{
		float cellHeight_num = aGridView_ggv.getActiveCell().getHeight();
		int gridHeight_int = aGridView_ggv.getRowsNumber();
		int activeColumnIndex_int = aGridView_ggv.getActiveColumnIndex();

		//BOTTOM...
		float y_num = aGridView_ggv.getActiveColumnBottomBorderY();
		int i = 0;

		while(y_num < 100)
		{
			GCellView cellView_gcv = aGridView_ggv.getCellViews()[i][activeColumnIndex_int];

			GRenderer.drawCellView(
				cellView_gcv, 
				cellView_gcv.getX(), 
				y_num);
			y_num += cellHeight_num;
			i++;

			if(i >= gridHeight_int)
			{
				i = 0;
			}
		}
		//...BOTTOM

		//TOP...
		y_num = aGridView_ggv.getActiveColumnTopBorderY() - cellHeight_num;
		i = gridHeight_int -1;

		while(y_num > -cellHeight_num)
		{
			GCellView cellView_gcv = aGridView_ggv.getCellViews()[i][activeColumnIndex_int];

			GRenderer.drawCellView(
				cellView_gcv, 
				cellView_gcv.getX(), 
				y_num);
			y_num -= cellHeight_num;
			i--;

			if(i < 0)
			{
				i = gridHeight_int -1;
			}
		}
		//...TOP
	}

	public static void drawGridViewCover(GGridView aGridView_ggv)
	{
		GRenderer.setColor(0,0,0);

		GRenderer.fillRect(
			GRenderer.SPRITE_PIXEL,
			0,
			0,
			aGridView_ggv.getX(), 
			100);

		float gridTotalWidth_num = aGridView_ggv.getWidth();

		GRenderer.fillRect(
			GRenderer.SPRITE_PIXEL,
			aGridView_ggv.getX() + gridTotalWidth_num,
			0,
			100 - gridTotalWidth_num, 
			100);
	}

	public static void drawGridView(GGridView aGridView_ggv)
	{
		GCellView cellView_gcv = aGridView_ggv.getActiveCell();

		if(cellView_gcv != null)
		{
			switch(cellView_gcv.getDirectionId())
			{
				case GCellView.DIRECTION_ID_HORIZONTAL:
				{
					GRenderer.tileGridViewActiveRow(aGridView_ggv);
				}
				break;
				case GCellView.DIRECTION_ID_VERTICAL:
				{
					GRenderer.tileGridViewActiveColumn(aGridView_ggv);
				}
				break;
			}
		}

		GCellView[][] cellViews_gcv_arr_arr = aGridView_ggv.getCellViews();

		for(int y = 0; y < aGridView_ggv.getColumnsNumber(); y++)
		{
			for(int x = 0; x < aGridView_ggv.getRowsNumber(); x++)
			{
				GRenderer.drawCellView(cellViews_gcv_arr_arr[y][x]);
			}
		}

		if(
			aGridView_ggv.getStateId() != GGridView.STATE_ID_LISTENING &&
			!GScreen.isPortraitMode()
			)
		{
			GRenderer.drawGridViewCover(aGridView_ggv);
		}
	}

	public static void drawFireflyView(GFireflyView aFireflyView_gfv)
	{

		GRenderer.setColor(GGameView.getInterfaceColor(), aFireflyView_gfv.getAlpha());

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_FIREFLY,
			aFireflyView_gfv.getX(),
			aFireflyView_gfv.getY(),
			GFireflyView.FIREFLY_SIZE * GScreen.getSidesRatio(), 
			GFireflyView.FIREFLY_SIZE);
		
		GRenderer.setColor(255, 255, 255);
	}

	public static void drawFirefliesView(GFirefliesView aFirefliesView_gfv)
	{
		GFireflyViewPool fireflies_gfvp = aFirefliesView_gfv.getFirefliesPool();

		for( int i = 0; i < fireflies_gfvp.length(); i++ )
		{
			GRenderer.drawFireflyView(fireflies_gfvp.getFirefly(i));
		}
	}

	public static void drawFirefliesView()
	{
		GGameView gameView_ggv = (GGameView) GMain.getGameController().getView();
		GRenderer.drawFirefliesView(gameView_ggv.getFirefliesView());
	}

	public static void drawTransitionView(GTransitionView aTransitionView_gtv)
	{
		int stateId_int = aTransitionView_gtv.getStateId();

		if(stateId_int == GTransitionView.STATE_ID_AWAITING)
		{
			return;
		}

		GRenderer.setColor(0, 0, 0, aTransitionView_gtv.getAlpha());
		GRenderer.fillRect(GRenderer.SPRITE_PIXEL, 0, 0, 100, 100);
	}

	public static void drawInteractiveRectangleView(GInteractiveRectangleView aInteractiveRectangleView_girv)
	{
		if(aInteractiveRectangleView_girv.isActive())
		{
			GRenderer.setColor(255,100,0);
		}
		else
		{
			GRenderer.setColor(200,100,0);
		}

		GRenderer.fillRect(
			GRenderer.SPRITE_PIXEL,
			aInteractiveRectangleView_girv.getX(),
			aInteractiveRectangleView_girv.getY(),
			aInteractiveRectangleView_girv.getWidth() * GScreen.getSidesRatio(),
			aInteractiveRectangleView_girv.getHeight());
	}

	public static void drawButtonView(GButtonView aButtonView_gbv)
	{
		if(!aButtonView_gbv.isRequired())
		{
			return;
		}

		GRenderer.setColor(GGameView.getInterfaceColor(), aButtonView_gbv.getAlpha());

		float x_num = aButtonView_gbv.getX();
		float y_num = aButtonView_gbv.getY();
		float width_num = aButtonView_gbv.getWidth() * GScreen.getSidesRatio();
		float height_num = aButtonView_gbv.getHeight();

		//BACKGROUND...
		GRenderer.fillRect(
			GRenderer.SPRITE_BUTTON,
			x_num,
			y_num,
			width_num,
			height_num);
		//...BACKGROUND

		//CAPTION...
		GRenderer.setColor(0, 0, 0);
		drawTextIntoWidth(
			aButtonView_gbv.getCaption(),
			x_num,
			y_num + height_num / 2,
			width_num,
			50,
			width_num * 0.2f);
		//...CAPTION


		//SUBCAPTION...
		string[] strings_str_arr = aButtonView_gbv.getSubcaption().Split(' ');
		
		GRenderer.setColor(255, 255, 255);
		float offsetY_num = 0f;

		for( int i = 0; i < strings_str_arr.Length; i++ )
		{
			string caption_str = strings_str_arr[i];
			float scale_num = GRenderer.getTextScaleCorrection(caption_str, 10f, width_num * 0.7f, 0);

			offsetY_num += scale_num;

			if(offsetY_num < 3)
			{
				offsetY_num = 3;
			}

			drawText(
				caption_str,
				x_num + width_num * 0.5f,
				y_num + height_num + offsetY_num,
				scale_num,
				GRenderer.TEXT_ALIGN_MODE_ID_CENTER);

			offsetY_num += scale_num * 0.5f;
		}
		//...SUBCAPTION
	}

	public static void drawButtonOptionsView(GButtonView aButtonView_gbv)
	{
		GRenderer.setColor(GGameView.getInterfaceColor(), aButtonView_gbv.getAlpha());

		float x_num = aButtonView_gbv.getX();
		float y_num = aButtonView_gbv.getY();
		float width_num = aButtonView_gbv.getWidth() * GScreen.getSidesRatio();
		float height_num = aButtonView_gbv.getHeight();

		//BACKGROUND...
		GRenderer.fillRect(
			GRenderer.SPRITE_BUTTON_OPTIONS,
			x_num,
			y_num,
			width_num,
			height_num);
		//...BACKGROUND
	}

	private static void drawStatisticRobotView(GStatisticRobotView aRobot_gsrv)
	{
		float width_num = aRobot_gsrv.getWidth();
		float height_num = aRobot_gsrv.getHeight();
		float x_num = aRobot_gsrv.getX();
		float y_num = aRobot_gsrv.getY()  + aRobot_gsrv.getFloatingHeightValue() * height_num * 0.05f;

		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ROBOT_ICON_PLATFORM,
			x_num,
			y_num + height_num * 0.06f,
			width_num * 0.98f,
			height_num * 0.98f);

		GRenderer.setColor(GGameView.getInterfaceColor(), aRobot_gsrv.getAlpha());
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ROBOT_ICON,
			x_num + width_num * 0.005f,
			y_num,
			width_num,
			height_num);

		GRenderer.setColor(255,255,255, 0.26f * aRobot_gsrv.getAlpha());
		GRenderer.fillRectCenter(
			GRenderer.SPRITE_ROBOT_ICON_BLICK,
			x_num + width_num * 0.005f,
			y_num,
			width_num,
			height_num);
		GRenderer.setColor(255,255,255);
	}

	public static void drawMenuView(GMenuView aMenuView_gmv)
	{
		GRenderer.drawFirefliesView();

		GButtonView[] buttons_gbv_arr = aMenuView_gmv.getButtonViews();

		GRenderer.drawButtonOptionsView(aMenuView_gmv.getOptionsButtonView());

		for( int i = 0; i < buttons_gbv_arr.Length; i++ )
		{
			GRenderer.drawButtonView(buttons_gbv_arr[i]);
		}

		GRenderer.setColor(85, 85, 85);

		GGameController gameController_ggc = GMain.getGameController();
		GGameModel gameModel_ggm = (GGameModel) gameController_ggc.getModel();
		
		GRenderer.drawTextIntoWidth(
			"score: " + gameModel_ggm.getScore(),
			0,
			95,
			100,
			3,
			0);

		GRenderer.setColor(255,255,255);
	}

	public static void drawStatisticView(GStatisticView aStatisticView_gsv)
	{

		//AREA PARAMETERS CALCULATING...
		float sidesRatio_num = GScreen.getSidesRatio();
		float areaWidth_num = 100 * sidesRatio_num;
		float areaHeight_num = 100;
		float offsetX_num = (100 - areaWidth_num) / 2;
		float offsetY_num = 0;
		//...AREA PARAMETERS CALCULATING

		//PLACEHOLDER...
		/*
		GRenderer.setColor(30, 30, 30);
		GRenderer.fillRect(
			GRenderer.SPRITE_PIXEL,
			offsetX_num,
			offsetY_num,
			areaWidth_num,
			areaHeight_num);
		GRenderer.setColor(255, 255, 255);*/
		//...PLACEHOLDER

		//ROBOTS...

		GStatisticRobotViewPool robotsPool_grp = aStatisticView_gsv.getRobotsPool();
		int robotsNumber_int = robotsPool_grp.length();

		for( int i = 0; i < robotsNumber_int; i++ )
		{	
			GRenderer.drawStatisticRobotView(robotsPool_grp.getRobot(i));
		}

		GRenderer.setColor(255, 255, 255);
		//...ROBOTS

		GRenderer.drawFirefliesView();

		//ROBOTS COUNTER...
		GRenderer.drawValueDisplayView(aStatisticView_gsv.getValueDisplayView());
		//...ROBOTS COUNTER

		//BUTTON BACK...
		GRenderer.drawButtonView(aStatisticView_gsv.getBackButtonView());
		//...BUTTON BACK
	}

	public static void drawValueDisplayView(GValueDisplayView aValueDisplayView_gvdv)
	{
		GColor color_gc = aValueDisplayView_gvdv.getColor();
		GRenderer.setColor(
			color_gc.getRed(),
			color_gc.getGreen(),
			color_gc.getBlue(),
			aValueDisplayView_gvdv.getAlpha());

		GRenderer.drawTextIntoWidth(
			aValueDisplayView_gvdv.getValue(),
			aValueDisplayView_gvdv.getX(),
			aValueDisplayView_gvdv.getY(),
			aValueDisplayView_gvdv.getWidth(),
			20f,
			aValueDisplayView_gvdv.getPadding());

		GRenderer.setColor(255, 255, 255);
	}

    public static void drawGameplayView(GGameplayView aGameplayView_ggv)
	{
		GRenderer.drawGridView(aGameplayView_ggv.getGridView());
		GRenderer.drawFirefliesView();
		GRenderer.drawButtonView(aGameplayView_ggv.getQuitButtonView());
		GRenderer.drawValueDisplayView(aGameplayView_ggv.getValueDisplayView());
	}

	public static void drawGameView(GGameView aGameView_ggv)
	{
		switch (aGameView_ggv.getStateId())
		{
			case GGameModel.GAME_STATE_ID_GAMEPLAY:
			{
				GRenderer.drawGameplayView(aGameView_ggv.getGameplayView());
			}
			break;
			case GGameModel.GAME_STATE_ID_MENU_PAUSE:
			{
				GRenderer.drawMenuView(aGameView_ggv.getMenuView());
			}
			break;
			case GGameModel.GAME_STATE_ID_STATISTICS:
			{
				GRenderer.drawStatisticView(aGameView_ggv.getStatisticView());
			}
			break;
		}

		GRenderer.drawTransitionView(aGameView_ggv.getTransitionView());
	}

	public static void drawPortraitMode()
	{
		string[] strings_str_arr = new string[]
		{
			"portrait mode",
			"is not supported",
			"yet"
		};
		
		GRenderer.setColor(255, 255, 255);
		float width_num = 50f;
		float height_num = 0f;
		float offsetY_num = 45f;
		float x_num = 25;
		float y_num = 0;

		for( int i = 0; i < strings_str_arr.Length; i++ )
		{
			string caption_str = strings_str_arr[i];
			height_num += GRenderer.getTextScaleCorrection(caption_str, 10f, width_num, 0);
		}

		for( int i = 0; i < strings_str_arr.Length; i++ )
		{
			string caption_str = strings_str_arr[i];
			float scale_num = GRenderer.getTextScaleCorrection(caption_str, 10f, width_num, 0);

			offsetY_num += scale_num;

			drawText(
				caption_str,
				x_num + width_num * 0.5f,
				y_num + offsetY_num - height_num/2,
				scale_num,
				GRenderer.TEXT_ALIGN_MODE_ID_CENTER);

			offsetY_num += scale_num * 0.5f;
		}
		//...SUBCAPTION
	}
}