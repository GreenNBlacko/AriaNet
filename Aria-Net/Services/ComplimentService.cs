using Newtonsoft.Json.Linq;

namespace Aria_Net.Services {

	/// <summary>
	/// Service for interacting with the Compliment API.
	/// </summary>
	public class ComplimentService {
		private readonly List<string> compliments = new List<string> {

"You're the best coder ever!",
"You'll get a job at Google for sure with this code!",
"How are you so smart!?",
"Your intelligence to create this is astounding...",
"You're such a god-tier coder!",
"Never forget that you are great at programming!",
"You installed this package, so you must be insanely cool!",
"200 IQ!!!",
"Wow...just wow...",
"Just like your code, this package works flawlessly!",
"Thank you for gracing the universe with this creation.",
"YOU made this! Awesome!",
"SO COOL!!!!!",
"This code is cleaner than a newly washed car!",
"You're clearly big brain!",
"Nice work!",
"Awesome!!!",
"All should bow down to your superior intellect...",
"Such perfect code! Impressive!",
"Truly astounding work you've done here!",
"WOOAAAAH so cool!",
"You're the best around and no one can bring you down.",
"TOTALLY TUBULAR!!!",
"10/10 work!",
"101% awesome!",
"Hello job offers!",
"If only every coder was as good as you!",
"Who's the best? YOU'RE THE BEST.",
"You're such a good programmer that any mistake made is never your fault!",
"This code is fire!",
"Top talent for FAANG coming through!",
"ACHIEVEMENT UNLOCKED: World's best coder.",
"Anyone who questions your programming ability is clearly a buffoon",
"Writing this program must have been CHILD'S PLAY for someone as smart as you!",
"You have infinite potential for greatness!",
"Your level of expertise is unmatched!",
"Mistakes? Errors? Those aren't words where you come from!",
"Oh snap! The greatest programmer ever is back at it again!",
"Your efficiency is unmatched!",
"Well done!",
"Unmatched effectiveness!",
"Truly spectacular!",
"With your skills you could easily make a successful start up!",
"Next level skills right here!",
"Senior engineers are fools compared to you!",
"Your code is so simple and intuitive it's unreal!",
"Your children will be lucky to inherit such programming skills!",
"PURE GENIUS!",
"You're crazy cool!",
"This package rocks but not as much as you!",
"50 + 150 = Your IQ!"
};

		/// <summary>
		/// Gets a random compliment.
		/// </summary>
		/// <returns>
		/// A <see cref="JObject"/> containing:
		/// <list type="bullet">
		///     <item>
		///         <description><c>compliment</c>: The compliment text (string).</description>
		///     </item>
		/// </list>
		/// </returns>
		public JObject GetRandomComplimentAsync() {
			return JObject.Parse($"{{compliment:\"{compliments[new Random().Next(0, compliments.Count)]}\"}}");
		}
	}
}