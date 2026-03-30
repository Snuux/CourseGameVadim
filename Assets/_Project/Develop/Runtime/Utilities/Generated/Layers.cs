namespace _Project.Develop.Runtime.Utilities.Generated
{
	using UnityEngine;

	public static class Layers
	{
		public static readonly int Default = LayerMask.NameToLayer("Default");
		public static readonly int TransparentFX = LayerMask.NameToLayer("TransparentFX");
		public static readonly int IgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
		public static readonly int Water = LayerMask.NameToLayer("Water");
		public static readonly int UI = LayerMask.NameToLayer("UI");
		public static readonly int Characters = LayerMask.NameToLayer("Characters");
		public static readonly int Projectiles = LayerMask.NameToLayer("Projectiles");
		public static readonly int Environment = LayerMask.NameToLayer("Environment");

		public static readonly int DefaultMask = 1 << Default;
		public static readonly int TransparentFXMask = 1 << TransparentFX;
		public static readonly int IgnoreRaycastMask = 1 << IgnoreRaycast;
		public static readonly int WaterMask = 1 << Water;
		public static readonly int UIMask = 1 << UI;
		public static readonly int CharactersMask = 1 << Characters;
		public static readonly int ProjectilesMask = 1 << Projectiles;
		public static readonly int EnvironmentMask = 1 << Environment;
	}
}
