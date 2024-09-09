using ClickableTransparentOverlay;
using ImGuiNET;
using System.Numerics;

namespace PD3.GUI.Menus {
	public class Error {

		protected string e_message;

		public Error(string message) {
			e_message = message;
		}

		public bool Render() {
			ImGui.Begin("Error!");

            var style = ImGui.GetStyle();

            ImGui.SetWindowFontScale(1.3f);
			ImGui.SetWindowSize(new Vector2(250, ImGui.CalcTextSize(text:e_message, start:0, wrapWidth:250).Y + ImGui.CalcTextSize("OK").Y * 1.5f + style.ItemSpacing.Y * 12));


			ProgramGUI.CenteredWrappedText(e_message);

			ImGui.Separator();

			if(ImGui.Button("OK", new Vector2(ImGui.GetWindowWidth() - style.ItemSpacing.X * 2, ImGui.CalcTextSize("L").Y * 1.5f))) {
				return true;
            }

			ImGui.End();

			return false;
		}
	}
}
