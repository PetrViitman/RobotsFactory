using UnityEngine;

public class GGameplayModel : GStatesPoweredModel
{

	public const int GAMEPLAY_STATE_ID_INVALID = GStatesPoweredModel.STATE_ID_INVALID;
	public const int GAMEPLAY_STATE_ID_ASSEMBLE = 0;
	public const int GAMEPLAY_STATE_ID_HINT_REWIND = 1;
	public const int GAMEPLAY_STATE_ID_HINT_REWIND_TRANSITION = 2;
	public const int GAMEPLAY_STATE_ID_HINT = 3;
	public const int GAMEPLAY_STATE_ID_POUSED = 4;
	public const int GAMEPLAY_STATE_ID_COMPLETED = 5;
	public const int GAMEPLAY_STATE_ID_RESTART_REQUIRED = 6;

	public GGameplayModel()
		: base()
	{

	}
}