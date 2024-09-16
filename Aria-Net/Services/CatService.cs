using Newtonsoft.Json.Linq;
using RestSharp;

namespace Aria_Net.Services {

	/// <summary>
	/// Service for interacting with the Cat API.
	/// </summary>
	public class CatService {
		private readonly RestClient _client;

		public CatService() {
			_client = new RestClient("https://api.thecatapi.com/v1/");
		}

		/// <summary>
		/// Gets a random cat image.
		/// </summary>
		/// <returns>
		/// A <see cref="JObject"/> array containing:
		/// <list type="bullet">
		///     <item>
		///         <description><c>id</c>: The unique identifier of the image (string).</description>
		///     </item>
		///     <item>
		///         <description><c>url</c>: A URL of the cat image (string).</description>
		///     </item>
		///     <item>
		///         <description><c>width</c>: The width of the image in pixels (integer).</description>
		///     </item>
		///     <item>
		///         <description><c>height</c>: The height of the image in pixels (integer).</description>
		///     </item>
		/// </list>
		/// </returns>
		public async Task<JObject> GetRandomCatImageAsync() {
			var request = new RestRequest("images/search", Method.Get);
			var response = await _client.ExecuteAsync(request);
			return JArray.Parse(response.Content)[0] as JObject;
		}
	}
}