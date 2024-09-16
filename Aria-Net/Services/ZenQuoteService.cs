using Aria_Net.IO;

namespace Aria_Net.Services {
	public class ZenQuoteService {
		private static readonly HttpClient client = new HttpClient();

		public async Task<string> GetQuoteAsync() {
			try {
				var response = await client.GetStringAsync("https://zenquotes.io/api/random");
				return response;
			} catch (Exception ex) {
				await new Logger().Log(new Discord.LogMessage(Discord.LogSeverity.Error, "ZenQuoteService", ex.Message, ex));
				return $"Error fetching quote: {ex.Message}";
			}
		}
	}
}