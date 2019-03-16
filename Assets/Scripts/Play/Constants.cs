using System.Collections;
using System.Collections.Generic;

public class Constants {

	public static Constants instance;

	private Constants () {}

	public static Constants getInstance () {
		if (instance == null) {
			instance = new Constants ();
		}
		return instance;
	}

	public readonly float[] LEVEL_Y = { -0.97f, 1f, 2.83f, 4.8f };
}
