using UnityEngine;

public class GGameView : GView
{
	private GFirefliesView firefliesView_gfv;
	private GTransitionView transitionView_gtv;

	public static readonly GColor INTERFACE_COLOR_ORANGE = new GColor(255, 119, 52);
	public static readonly GColor INTERFACE_COLOR_RED = new GColor(245, 64, 62);
	public static readonly GColor INTERFACE_COLOR_YELLOW = new GColor(249, 180, 57);
	public static readonly GColor INTERFACE_COLOR_BLUE = new GColor(87, 130, 219);
	public static readonly GColor INTERFACE_COLOR_VIOLET = new GColor(130, 87, 219);
	
	public static readonly GColor[] INTERFACE_COLORS = new GColor[]
	{
		//GGameView.INTERFACE_COLOR_ORANGE,
		GGameView.INTERFACE_COLOR_RED,
		GGameView.INTERFACE_COLOR_YELLOW,
		GGameView.INTERFACE_COLOR_VIOLET
	};

	//public static GColor gameCalor_gc = new GColor(255, 119, 52);
	public static GColor previousInterfaceCalor_gc = new GColor(0, 0, 0);
	public static GColor interfaceCalor_gc = new GColor(255, 255, 255);

	public static GColor getInterfaceColor()
	{
		return GGameView.interfaceCalor_gc;
	}

	public static void setInterfaceColor(GColor aColor_gc)
	{
		GGameView.previousInterfaceCalor_gc.copy(GGameView.interfaceCalor_gc);
		GGameView.interfaceCalor_gc.copy(aColor_gc);
	}

	public static void setRandomInterfaceColor()
	{
		GColor[] colors_gc_arr = GGameView.INTERFACE_COLORS;
		int randomIndex_int = Random.Range(0, colors_gc_arr.Length);

		GGameView.setInterfaceColor(colors_gc_arr[randomIndex_int]);

		if(GGameView.interfaceCalor_gc.isEqual(GGameView.previousInterfaceCalor_gc))
		{
			randomIndex_int++;

			if(randomIndex_int == colors_gc_arr.Length)
			{
				randomIndex_int = 0;
			}

			GGameView.setInterfaceColor(colors_gc_arr[randomIndex_int]);
		}
	}

	public GGameView()
		: base()
	{
		this.firefliesView_gfv = new GFirefliesView();
		this.transitionView_gtv = new GTransitionView();
		GGameView.setRandomInterfaceColor();
	}

	//MODEL STATE ID PUBLIC ACCESS...
	public int getStateId()
	{
		GGameModel aGameModel_ggm = (GGameModel) this.getModel();
		return aGameModel_ggm.getStateId();
	}
	//...MODEL STATE ID PUBLIC ACCESS

	public GGameplayView getGameplayView()
	{
		return (GGameplayView) GMain.getGameplayController().getView();
	}

	public GFirefliesView getFirefliesView()
	{
		return this.firefliesView_gfv;
	}

	public GTransitionView getTransitionView()
	{
		return this.transitionView_gtv;
	}

	public GStatisticView getStatisticView()
	{
		return (GStatisticView) GMain.getGameController().getStatisticController().getView();
	}

	public GMenuView getMenuView()
	{
		return (GMenuView) GMain.getGameController().getMenuController().getView();
	}

	public void adjust()
	{
		switch (this.getStateId())
		{
			case GGameModel.GAME_STATE_ID_GAMEPLAY:
			{
				this.getGameplayView().adjust();
			}
			break;
			case GGameModel.GAME_STATE_ID_STATISTICS:
			{
				this.getStatisticView().adjust();
			}
			break;
		}
	}

	public void update()
	{
		switch (this.getStateId())
		{
			case GGameModel.GAME_STATE_ID_GAMEPLAY:
			{
				this.getGameplayView().update();
			}
			break;
			case GGameModel.GAME_STATE_ID_STATISTICS:
			{
				this.getStatisticView().update();
			}
			break;
			case GGameModel.GAME_STATE_ID_MENU_PAUSE:
			{
				this.getMenuView().update();
			}
			break;
		}

		this.getFirefliesView().update();	
		this.getTransitionView().update();	
	}
}