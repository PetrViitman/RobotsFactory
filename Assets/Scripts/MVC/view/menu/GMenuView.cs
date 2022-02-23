using UnityEngine;

public class GMenuView : GView
{
	private GButtonView[] buttons_gbv_arr;
	private GOptionsButtonView optionsButton_gobv;
	private GContinueButtonView continueButton_gbv;
	private GGuideButtonView guideButton_gbv;
	private GAutoAssembleButtonView autoAssembleButton_gbv;
	private GStatisticButtonView statisticButton_gbv;
	private GPlayButtonView playButton_gbv;

	public GMenuView()
		: base()
	{
		this.optionsButton_gobv = new GOptionsButtonView(0,0,25,25);
		
		this.continueButton_gbv = new GContinueButtonView(0,0,25,25);
		this.guideButton_gbv = new GGuideButtonView(0,0,25,25);
		this.autoAssembleButton_gbv = new GAutoAssembleButtonView(0,0,25,25);
		this.statisticButton_gbv = new GStatisticButtonView(0,0,25,25);
		this.playButton_gbv = new GPlayButtonView(0,0,25,25);
	}

	public GButtonView[] getButtonViews()
	{
		return this.buttons_gbv_arr;
	}

	public GButtonView getOptionsButtonView()
	{
		return this.optionsButton_gobv;
	}

	public override void validate()
	{
		if(!GMain.isGameReady())
		{
			return;
		}

		GGameController gameController_gc = GMain.getGameController();
		GGameModel gameModel_gc = (GGameModel) gameController_gc.getModel();

		GGameplayController gameplayController_gc = GMain.getGameplayController();
		GGameplayModel gameplayModel_gc = (GGameplayModel) gameplayController_gc.getModel();
		
		GRoundModel roundModel_grm = (GRoundModel) GMain.getGameController().getRoundController().getModel();

		if(
			gameModel_gc.getPreviousStateId() == GStatesPoweredModel.STATE_ID_INVALID ||
			roundModel_grm.getStateId() == GRoundModel.ROUND_STATE_ID_COMPLETED
			)
		{
			this.buttons_gbv_arr = new GButtonView[]
			{
				this.playButton_gbv,
			};
		}
		else
		{
			switch(gameplayModel_gc.getStateId())
			{
				case GGameplayModel.GAMEPLAY_STATE_ID_HINT_REWIND:
				case GGameplayModel.GAMEPLAY_STATE_ID_HINT_REWIND_TRANSITION:
				case GGameplayModel.GAMEPLAY_STATE_ID_HINT:
				{
					this.buttons_gbv_arr = new GButtonView[]
					{
						this.continueButton_gbv,
						this.guideButton_gbv,
						this.statisticButton_gbv
					};
				}
				break;
				default:
				{
					this.buttons_gbv_arr = new GButtonView[]
					{
						this.continueButton_gbv,
						this.guideButton_gbv,
						this.autoAssembleButton_gbv,
						this.statisticButton_gbv
					};
				}
				break;
			}
		}

		this.adjust();
	}

	public void adjust()
	{
		int buttonsNumber_int = this.buttons_gbv_arr.Length;
		float widthPerButton_num = 90 / buttonsNumber_int;
		float buttonMaximalWidth_num = 30;

		if(buttonsNumber_int == 1)
		{
			buttonMaximalWidth_num = 45;
		}

		if(widthPerButton_num / GScreen.getSidesRatio() > buttonMaximalWidth_num)
		{
			widthPerButton_num = buttonMaximalWidth_num * GScreen.getSidesRatio();
		}

		float totalWidth_num = buttonsNumber_int * widthPerButton_num;

		float buttonPadding_num = widthPerButton_num * 0.075f;
		float buttonSize_num = (widthPerButton_num - buttonPadding_num * 2) / GScreen.getSidesRatio();

		float x_num = 50 - totalWidth_num / 2;

		for( int i = 0; i < this.buttons_gbv_arr.Length; i++ )
		{
			GButtonView buttonView_gbv = this.buttons_gbv_arr[i];

			buttonView_gbv.setXY(
				x_num + buttonPadding_num,
				50 - buttonSize_num / 2);
			buttonView_gbv.setWidth(buttonSize_num);
			buttonView_gbv.setHeight(buttonSize_num);

			x_num += widthPerButton_num;
		}

		//OPTIONS BUTTON...
		GButtonView optionsButtonView_gbv = (GButtonView) this.optionsButton_gobv;
		float optionsButtonSize_num = 15f;
		float optionsButtonPadding_num = optionsButtonSize_num * 0.35f;

		optionsButtonView_gbv.setXY(100 - (optionsButtonSize_num + optionsButtonPadding_num) * GScreen.getSidesRatio(), optionsButtonPadding_num);
		optionsButtonView_gbv.setWidth(optionsButtonSize_num);
		optionsButtonView_gbv.setHeight(optionsButtonSize_num);

		//...OPTIONS BUTTON
		/*
		float buttonSize_num = 25f;
		float offsetX_num = buttonSize_num * 1.35f * GScreen.getSidesRatio();

		//GUIDE BUTTON...
		GButtonView guideButton_ggbv = this.buttons_gbv_arr[0];
		guideButton_ggbv.setXY(
			50 - offsetX_num - buttonSize_num / 2 * GScreen.getSidesRatio(),
			50 - buttonSize_num / 2);
		guideButton_ggbv.setWidth(buttonSize_num);
		guideButton_ggbv.setHeight(buttonSize_num);
		//...GUIDE BUTTON

		//AUTOASSEMBLE BUTTON...
		GButtonView assembleButton_gbv = this.buttons_gbv_arr[1];
		assembleButton_gbv.setXY(
			50 - buttonSize_num / 2 * GScreen.getSidesRatio(),
			50 - buttonSize_num / 2);
		assembleButton_gbv.setWidth(buttonSize_num);
		assembleButton_gbv.setHeight(buttonSize_num);
		//...AUTOASSEMBLE BUTTON

		//STATISTIC BUTTON...
		GButtonView guideButton_gbv = this.buttons_gbv_arr[2];
		guideButton_gbv.setXY(
			50 + offsetX_num - buttonSize_num / 2 * GScreen.getSidesRatio(),
			50 - buttonSize_num / 2);
		guideButton_gbv.setWidth(buttonSize_num);
		guideButton_gbv.setHeight(buttonSize_num);
		//...STATISTIC BUTTON

		*/
	}

	public void update()
	{
		for( int i = 0; i < this.buttons_gbv_arr.Length; i++ )
		{
			this.buttons_gbv_arr[i].update();
		}

		this.optionsButton_gobv.update();
	}
}