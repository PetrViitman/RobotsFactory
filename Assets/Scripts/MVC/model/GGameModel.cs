using UnityEngine;

public class GGameModel : GStatesPoweredModel
{
	public const int GAME_STATE_ID_GAMEPLAY = 0;
	public const int GAME_STATE_ID_MENU_PAUSE = 1;
	public const int GAME_STATE_ID_STATISTICS = 2;

	private static int roundIndex_int = 0;

	private int score_int = 0;
	private bool isTransitionInProgress_bl = false;

	public GGameModel()
		: base()
	{
		this.setStateId(GGameModel.GAME_STATE_ID_MENU_PAUSE);
	}

	public static void setRoundIndex(int aRoundIndex_int)
	{
		GGameModel.roundIndex_int = aRoundIndex_int;
	}

	public static void incrementRoundIndex()
	{
		GGameModel.roundIndex_int++;

		if(GGameModel.roundIndex_int > GProgressDescriptor.ROUNDS_NUMBER - 1)
		{
			GGameModel.roundIndex_int = GProgressDescriptor.ROUNDS_NUMBER - 1;
		}
	}

	public static int getRoundIndex()
	{
		return GGameModel.roundIndex_int;
	}


	public void incrementScore()
	{
		this.score_int += 5;
	}

	public void setScore(int aScore_int)
	{
		this.score_int = aScore_int;
	}

	public int getScore()
	{
		return this.score_int;
	}

	public bool isTransitionInProgress()
	{
		return this.isTransitionInProgress_bl;
	}

	public void setIsTransitionInProgress(bool aIsTransitionInProgress_bl)
	{
		this.isTransitionInProgress_bl = aIsTransitionInProgress_bl;
	}
}