using UnityEngine;

public class GStatisticView : GView
{
	public const int MAXIMAL_ROBOTS_NUMBER = 49;
	private const int DISPLAY_AREA_SIZE_RELATIVE_TO_SHORTEST_SCREEN_SIDE = 100;

	private GStatisticRobotViewPool robots_gsrvp;
	private GBackButtonView backButtonView_gbbv;
	private GValueDisplayView valueDisplayView_gvdv;

	public GStatisticView()
		: base()
	{
		this.robots_gsrvp = new GStatisticRobotViewPool(GStatisticView.MAXIMAL_ROBOTS_NUMBER);
		this.backButtonView_gbbv = new GBackButtonView(0, 0, 25, 25);
		this.valueDisplayView_gvdv = new GValueDisplayView(0, 0, 25, 25);
	}

	public GValueDisplayView getValueDisplayView()
	{
		return this.valueDisplayView_gvdv;
	}

	public GStatisticRobotViewPool getRobotsPool()
	{
		return this.robots_gsrvp;
	}

	public GBackButtonView getBackButtonView()
	{
		return this.backButtonView_gbbv;
	}

	public override void validate()
	{
		this.adjust();

		GStatisticRobotViewPool robots_gsrvp = this.robots_gsrvp;
		GStatisticModel statisticModel_gsm = (GStatisticModel) this.getModel();
		
		int robotsNumber_int = statisticModel_gsm.getAssembledRobotsNumber();

		if(robotsNumber_int == 0)
		{
			robotsNumber_int = 1;
		}

		robots_gsrvp.drop();

		//AREA PARAMETERS CALCULATING...
		float sidesRatio_num = GScreen.getSidesRatio();
		float areaWidth_num = GStatisticView.DISPLAY_AREA_SIZE_RELATIVE_TO_SHORTEST_SCREEN_SIDE * sidesRatio_num;
		float areaHeight_num = GStatisticView.DISPLAY_AREA_SIZE_RELATIVE_TO_SHORTEST_SCREEN_SIDE;
		float offsetX_num = (100 - areaWidth_num) / 2;
		float offsetY_num = 0;
		//...AREA PARAMETERS CALCULATING


		int rowsNumber_int = (int) Mathf.Sqrt( (float) robotsNumber_int);
		int columnsNumber_int = robotsNumber_int / rowsNumber_int;

		if((float)robotsNumber_int % (float)rowsNumber_int > 0f)
		{
			columnsNumber_int++;
		}

		float robotWidth_num = areaWidth_num / columnsNumber_int;
		float robotHeight_num = robotWidth_num / sidesRatio_num;

		float paddingWidth_num;
		float paddingHeight_num;

		switch (robotsNumber_int)
		{
			case 1:
			{
				paddingWidth_num = robotWidth_num * 0.25f;
				paddingHeight_num = robotHeight_num * 0.25f;
			}
			break;
			case 2:
			case 4:
			{
				paddingWidth_num = robotWidth_num * 0.125f;
				paddingHeight_num = robotHeight_num * 0.125f;
			}
			break;
			default:
			{
				paddingWidth_num = robotWidth_num * 0.05f;
				paddingHeight_num = robotHeight_num * 0.05f;
			}
			break;
		}

		//CENTRALIZING...
		offsetX_num += (areaWidth_num - (robotWidth_num * columnsNumber_int)) / 2;
		offsetY_num += (areaHeight_num - (robotHeight_num * rowsNumber_int)) / 2;
		//...CENTRALIZING

		int robotIndex_int = 0;

		for( int y = 0; y < rowsNumber_int; y++ )
		{
			for( int x = 0; x < columnsNumber_int; x++ )
			{
				if(robotIndex_int < robotsNumber_int)
				{
					GStatisticRobotView robot_gsrv = robots_gsrvp.add();
					robot_gsrv.setX(offsetX_num + x * robotWidth_num + robotWidth_num / 2);
					robot_gsrv.setY(offsetY_num + y * robotHeight_num + robotHeight_num / 2);
					robot_gsrv.setWidth(robotWidth_num - paddingWidth_num * 2);
					robot_gsrv.setHeight(robotHeight_num - paddingHeight_num * 2);
					robot_gsrv.show(true);
				}
				else
				{
					break;
				}

				robotIndex_int++;
			}
		}

		if(statisticModel_gsm.getStateId() == GStatisticModel.STATISTIC_STATE_ID_INCREMENTATION)
		{
			robots_gsrvp.getRobot(robots_gsrvp.length()-1).show(false);
		}

		//HIDE ROBOT IMMEDIATELY IF ONLY PLATFORM DISPLAY REQUIRED...
		if(statisticModel_gsm.getAssembledRobotsNumber() == 0)
		{
			robots_gsrvp.getRobot(0).hide(true);
		}
		//...HIDE ROBOT IMMEDIATELY IF ONLY PLATFORM DISPLAY REQUIRED

		this.setCounterValue(statisticModel_gsm.getAssembledRobotsNumber());

		this.backButtonView_gbbv.setIsRequired(statisticModel_gsm.getStateId() != GStatisticModel.STATISTIC_STATE_ID_DECREMENTATION);
	}

	public void setCounterValue(int aValue_int)
	{
		this.valueDisplayView_gvdv.setTargetValue(aValue_int+"");
	}

	public void adjust()
	{
		//BACK BUTTON...
		GButtonView backButtonView_gbv = this.backButtonView_gbbv;
		float leftAreaWidth_num = (100 - GStatisticView.DISPLAY_AREA_SIZE_RELATIVE_TO_SHORTEST_SCREEN_SIDE * GScreen.getSidesRatio())/2;
		float backButtonPadding_num = leftAreaWidth_num * 0.15f;
		float backButtonWidth_num = leftAreaWidth_num - backButtonPadding_num * 2;
		float backButtonHeight_num = backButtonWidth_num / GScreen.getSidesRatio();

		float backButtonX_num = backButtonPadding_num;
		float backButtonY_num = 50 - backButtonHeight_num / 2;

		if(backButtonHeight_num > 30f)
		{
			backButtonHeight_num = 30f;
			backButtonWidth_num = backButtonHeight_num * GScreen.getSidesRatio();
			backButtonX_num = leftAreaWidth_num / 2 - backButtonWidth_num / 2;
			backButtonY_num = 50 - backButtonHeight_num / 2;
		}

		backButtonView_gbv.setXY(
			backButtonX_num,
			backButtonY_num);

		backButtonView_gbv.setWidth(backButtonWidth_num / GScreen.getSidesRatio());
		backButtonView_gbv.setHeight(backButtonHeight_num);
		//...BACK BUTTON

		//VALUE DISPLAY...
		GValueDisplayView valueDisplayView_gvdv = this.valueDisplayView_gvdv;
		float rightAreaWidth_num = (100 - GStatisticView.DISPLAY_AREA_SIZE_RELATIVE_TO_SHORTEST_SCREEN_SIDE * GScreen.getSidesRatio())/2;
		float valueDisplayWidth_num = rightAreaWidth_num;
		float valueDisplayHeight_num = 25;

		valueDisplayView_gvdv.setXY(
			100 - valueDisplayWidth_num,
			50);

		valueDisplayView_gvdv.setWidth(valueDisplayWidth_num);
		valueDisplayView_gvdv.setHeight(valueDisplayHeight_num);
		valueDisplayView_gvdv.setPadding(valueDisplayWidth_num * 0.15f);
		//...VALUE DISPLAY
	}

	public void update()
	{
		this.robots_gsrvp.update();
		this.backButtonView_gbbv.update();
		this.valueDisplayView_gvdv.update();
	}

	public void hideNextRobot()
	{
		GStatisticController statisticController_gsc = (GStatisticController) this.getController();
		GStatisticRobotViewPool robots_gsrvp = this.robots_gsrvp;

		this.robots_gsrvp.getRobot(this.getVisibleRobotsNumber() - 1).hide(false);
		statisticController_gsc.onRobotStartHiding();
	}

	public int getVisibleRobotsNumber()
	{
		return this.robots_gsrvp.getVisibleRobotsNumber();
	}
}