namespace Aria_Net.Services {

	/// <summary>
	/// Service for interacting with the 8-Ball API.
	/// </summary>
	public class EightBallService {
		private readonly List<string> _responses;

		public EightBallService() {
			// Initialize with a set of predefined responses
			_responses = new List<string>
			{
			"Yes.",
			"No.",
			"Maybe.",
			"I don't know.",
			"Absolutely.",
			"Definitely not.",
			"Most likely.",
			"Not a chance."
		};
		}

		public Task<string> GetAnswerAsync() {
			var random = new Random();
			int index = random.Next(_responses.Count);
			return Task.FromResult(_responses[index]);
		}

		public Task<string> GetAnswerAsync(string question) {
			// Optionally log or use the question
			// var answer = GetAnswerAsync(); // Use this if you don't need the question

			return GetAnswerAsync();
		}
	}
}