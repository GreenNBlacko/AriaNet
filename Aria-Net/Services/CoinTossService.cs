namespace Aria_Net.Services {
	public class CoinTossService {
		private static readonly Random _random = new Random();

		/// <summary>
		/// Simulates a coin toss.
		/// </summary>
		/// <returns>A string representing the result of the coin toss. Either "Heads" or "Tails".</returns>
		public string Toss() {
			return _random.Next(2) == 0 ? "Heads" : "Tails";
		}
	}
}