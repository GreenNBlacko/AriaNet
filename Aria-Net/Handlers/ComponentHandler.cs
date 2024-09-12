using Aria_Net.Components;
using Aria_Net.Components.Buttons;
using Aria_Net.Components.Modals;
using Aria_Net.Components.SelectMenus;
using Aria_Net.IO;
using System.Data;
using System.Reflection;

namespace Aria_Net.Handlers {
	public class ComponentHandler {

		private DiscordClient _client;

		public ComponentHandler(DiscordClient client) {
			_client = client;
		}

		public void RegisterComponents() {
			var logger = new Logger();

			logger.Log("Registering Components", Discord.LogSeverity.Info).Wait();

			var assembly = Assembly.GetExecutingAssembly();

			var componentTypes = assembly.GetTypes()
				.Where(t => !t.IsAbstract && InheritsFromGenericBase(t, typeof(BaseComponent<,>)));

			foreach (var componentType in componentTypes) {
				if (typeof(BaseButton).IsAssignableFrom(componentType)) {
					var instance = Activator.CreateInstance(componentType) as BaseButton;
					if (instance != null) {
						instance.Register(_client);
						logger.Log("Button registered: " + instance.ID, Discord.LogSeverity.Info).Wait();
					}
				} else if (typeof(BaseSelectMenu).IsAssignableFrom(componentType)) {
					var instance = Activator.CreateInstance(componentType) as BaseSelectMenu;
					if (instance != null) {
						instance.Register(_client);
						logger.Log("Select menu registered: " + instance.ID, Discord.LogSeverity.Info).Wait();
					}
				} else if (typeof(BaseModal).IsAssignableFrom(componentType)) {
					var instance = Activator.CreateInstance(componentType) as BaseModal;
					if (instance != null) {
						instance.Register(_client);
						logger.Log("Modal registered: " + instance.ID, Discord.LogSeverity.Info).Wait();
					}
				}
			}
		}

		private bool InheritsFromGenericBase(Type type, Type genericBase) {
			while (type != null && type != typeof(object)) {
				var currentType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
				if (currentType == genericBase)
					return true;

				type = type.BaseType;
			}
			return false;
		}
	}
}
