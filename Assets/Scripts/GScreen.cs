using UnityEngine;

public static class GScreen
{
	public static float screenWidthInPixels_num;
	public static float screenHeightInPixels_num;
	public static float pixelsPerWidthPercent_num;
	public static float pixelsPerHeightPercent_num;
	public static float screenSidesRatio_num;

	public static void adjust()
	{
		GScreen.screenWidthInPixels_num = Screen.width;
		GScreen.screenHeightInPixels_num = Screen.height;
		GScreen.pixelsPerWidthPercent_num = GScreen.screenWidthInPixels_num / 100;
		GScreen.pixelsPerHeightPercent_num = GScreen.screenHeightInPixels_num / 100;
		GScreen.screenSidesRatio_num = GScreen.screenHeightInPixels_num / GScreen.screenWidthInPixels_num;

		if(GMain.isGameReady())
		{
			GMain.getGameController().onScreenAdjusted();
		}
	}

	public static float toPercentageX(float aX_num)
	{
		return aX_num / GScreen.pixelsPerWidthPercent_num;
    }

	public static float toPercentageY(float aY_num)
	{
		return aY_num / GScreen.pixelsPerHeightPercent_num;
	}

	public static float getSidesRatio()
	{
		return GScreen.screenSidesRatio_num;
	}

	public static bool isPortraitMode()
	{
		return GScreen.screenHeightInPixels_num > GScreen.screenWidthInPixels_num;
	}

	public static bool isLandscapeMode()
	{
		return !GScreen.isPortraitMode();
	}

	public static void update()
	{
		if(
			GScreen.screenWidthInPixels_num != Screen.width ||
			GScreen.screenHeightInPixels_num != Screen.height
			)
		{
			GScreen.adjust();
		}
	}
}