namespace Aria_Net.Services {
	public class DiceRollService {
		private static readonly Random _random = new Random();

		/// <summary>
		/// Rolls a die with a specified number of sides.
		/// </summary>
		/// <param name="sides">The number of sides on the die. Must be greater than 0.</param>
		/// <returns>An integer between 1 and the number of sides (inclusive) representing the result of the roll.</returns>
		/// <exception cref="ArgumentException">Thrown if the number of sides is less than 1.</exception>
		public int Roll(int sides) {
			if (sides < 1) {
				throw new ArgumentException("Number of sides must be greater than 0.", nameof(sides));
			}

			return _random.Next(1, sides + 1);
		}
	}
}