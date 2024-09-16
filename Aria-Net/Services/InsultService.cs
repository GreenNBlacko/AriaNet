using Newtonsoft.Json.Linq;
using RestSharp;

namespace Aria_Net.Services {

	/// <summary>
	/// Service for interacting with the Insult API.
	/// </summary>
	public class InsultService {
		public static HttpClient client = new HttpClient();

		/// <summary>
		/// Gets a random insult.
		/// </summary>
		/// <returns>
		/// A <see cref="JObject"/> containing:
		/// <list type="bullet">
		///     <item>
		///         <description><c>insult</c>: The insult text (string).</description>
		///     </item>
		/// </list>
		/// </returns>
		public async Task<JObject> GetRandomInsultAsync() {
			var response = await client.GetStringAsync("https://insult.mattbas.org/api/insult/");
			return JObject.Parse($"{{insult:\"{response}\"}}");
		}
	}
}