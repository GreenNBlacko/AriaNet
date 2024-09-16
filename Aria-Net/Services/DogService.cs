using Newtonsoft.Json.Linq;
using RestSharp;

namespace Aria_Net.Services {

	/// <summary>
	/// Service for interacting with the Dog API.
	/// </summary>
	public class DogService {
		private readonly RestClient _client;

		public DogService() {
			_client = new RestClient("https://dog.ceo/api/");
		}

		/// <summary>
		/// Gets a random dog image.
		/// </summary>
		/// <returns>
		/// A <see cref="JObject"/> containing:
		/// <list type="bullet">
		///     <item>
		///         <description><c>message</c>: A URL of the random dog image (string).</description>
		///     </item>
		///     <item>
		///         <description><c>status</c>: The status of the response (string).</description>
		///     </item>
		/// </list>
		/// </returns>
		public async Task<JObject> GetRandomDogImageAsync() {
			var request = new RestRequest("breeds/image/random", Method.Get);
			var response = await _client.ExecuteAsync(request);
			return JObject.Parse(response.Content);
		}
	}
}