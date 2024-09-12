﻿using Discord.WebSocket;

namespace Aria_Net.Commands.Options
{
    public class CommandOption
    {
        private SocketSlashCommandDataOption _option;

        public string Name => _option.Name;

        public CommandOptions Options => new(_option.Options);

		public CommandOption(SocketSlashCommandDataOption option)
        {
            _option = option;
        }

#nullable enable
        public T? GetValue<T>() {
			if (_option.Value != null) {
				return _option.Value is T ? (T)_option.Value : default;
			}

			return default;
		}
#nullable restore

        public T GetValue<T>(T defaultValue) {
			if (_option.Value != null) {
				return _option.Value is T ? (T)_option.Value : defaultValue;
			}

			return defaultValue;
		}
    }
}