using UnityEngine;

public class GGameplayView : GView
{
	private GQuitButtonView quitButtonView_gqbv;
	private GValueDisplayView valueDisplayView_gvdv;

	public GGameplayView()
		: base()
	{
		this.quitButtonView_gqbv = new GQuitButtonView(0,0,25,25);
		this.valueDisplayView_gvdv = new GValueDisplayView(0, 0, 25, 25);
		this.valueDisplayView_gvdv.setShowingHidingDurationInFrames(7);
		this.valueDisplayView_gvdv.setRGB(128, 128, 128);
	}

	public GGridView getGridView()
	{
		return (GGridView) GMain.getGameplayController().getAssemblyController().getGridController().getView();
	}

	public GValueDisplayView getValueDisplayView()
	{
		return this.valueDisplayView_gvdv;
	}

	//BUTTONS...
	public GQuitButtonView getQuitButtonView()
	{
		return this.quitButtonView_gqbv;
	}
	//...BUTTONS

	public void adjust()
	{
		//GRID...
		GGridView gridView_ggv = this.getGridView();
		gridView_ggv.centralize();
		//...GRID

		//QUIT BUTTON...
		GButtonView quitButtonView_gbv = this.quitButtonView_gqbv;
		float leftAreaWidth_num = gridView_ggv.getX();
		float buttonPadding_num = leftAreaWidth_num * 0.2f;
		float buttonWidth_num = leftAreaWidth_num - buttonPadding_num * 2;
		float buttonHeight_num = buttonWidth_num / GScreen.getSidesRatio();
		float buttonX_num = buttonPadding_num;

		buttonHeight_num = 15f;
		buttonWidth_num = buttonHeight_num * GScreen.getSidesRatio();
		buttonX_num = leftAreaWidth_num / 2f - buttonWidth_num / 2f;
		
		float quitButtonY_num = 15 - buttonHeight_num / 2;

		quitButtonView_gbv.setWidth(buttonWidth_num / GScreen.getSidesRatio());
		quitButtonView_gbv.setHeight(buttonHeight_num);
		//...QUIT BUTTON

		//TIMER...
		GValueDisplayView valueDisplayView_gvdv = this.valueDisplayView_gvdv;
		float rightAreaWidth_num = 100 - (gridView_ggv.getX() + gridView_ggv.getWidth());
		float timerWidth_num = rightAreaWidth_num;
		if(timerWidth_num > 20f)
		{
			timerWidth_num = 20f;
		}

		valueDisplayView_gvdv.setXY(
			leftAreaWidth_num / 2 - timerWidth_num/2,
			15);

		valueDisplayView_gvdv.setWidth(timerWidth_num);
		valueDisplayView_gvdv.setPadding(timerWidth_num * 0.05f);

		quitButtonView_gbv.setXY(
			100 - rightAreaWidth_num / 2 - buttonWidth_num / 2,
			quitButtonY_num);
		//...TIMER
	}

	public void setDisplayValue(string aFormatedRemainingTime_str)
	{
		this.valueDisplayView_gvdv.setTargetValue(aFormatedRemainingTime_str);
	}

	public void update()
	{
		this.quitButtonView_gqbv.update();
		this.valueDisplayView_gvdv.update();
		this.getGridView().update();
	}
}