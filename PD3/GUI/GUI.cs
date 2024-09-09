using ClickableTransparentOverlay;
using ImGuiNET;
using PD3.Classes;
using PD3.GUI.Menus;
using System.Numerics;

/* TODO List
 * Set up user classes... Done
 * Set up file IO... Done
 * Set up serialization... Done
 * Set up data saving... TBD
 * Set up GUI... Done
 * Create menus... TBD
 * Link GUI to menus... TBD
 */

namespace PD3.GUI {
	public enum e_Menus { Main, Register, Login, Profile, Admin }

	public class ProgramGUI : Overlay {
		#region Theme

		private static void SetTheme() {
			var colors = ImGui.GetStyle().Colors;

			colors[(int)ImGuiCol.WindowBg] = new Vector4(0.1f, 0.1f, 0.13f, 1.0f);
			colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);

			// Border
			colors[(int)ImGuiCol.Border] = new Vector4(0.44f, 0.37f, 0.61f, 0.29f);
			colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.0f, 0.0f, 0.0f, 0.24f);

			// Text
			colors[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
			colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);

			// Headers
			colors[(int)ImGuiCol.Header] = new Vector4(0.13f, 0.13f, 0.17f, 1.0f);
			colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.19f, 0.2f, 0.25f, 1.0f);
			colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);

			// Buttons
			colors[(int)ImGuiCol.Button] = new Vector4(0.13f, 0.13f, 0.17f, 1.0f);
			colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.19f, 0.2f, 0.25f, 1.0f);
			colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);
			colors[(int)ImGuiCol.CheckMark] = new Vector4(0.74f, 0.58f, 0.98f, 1.0f);

			// Popups
			colors[(int)ImGuiCol.PopupBg] = new Vector4(0.1f, 0.1f, 0.13f, 0.92f);

			// Slider
			colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.44f, 0.37f, 0.61f, 0.54f);
			colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.74f, 0.58f, 0.98f, 0.54f);

			// Frame BG
			colors[(int)ImGuiCol.FrameBg] = new Vector4(0.13f, 0.13f, 0.17f, 1.0f);
			colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.19f, 0.2f, 0.25f, 1.0f);
			colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);
			// Tabs
			colors[(int)ImGuiCol.Tab] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);
			colors[(int)ImGuiCol.TabHovered] = new Vector4(0.24f, 0.24f, 0.32f, 1.0f);
			colors[(int)ImGuiCol.TabActive] = new Vector4(0.2f, 0.22f, 0.27f, 1.0f);
			colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);
			colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);

			// Title
			colors[(int)ImGuiCol.TitleBg] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);
			colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);
			colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);

			// Scrollbar
			colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.1f, 0.1f, 0.13f, 1.0f);
			colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.16f, 0.16f, 0.21f, 1.0f);
			colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.19f, 0.2f, 0.25f, 1.0f);
			colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.24f, 0.24f, 0.32f, 1.0f);
			// Seperator
			colors[(int)ImGuiCol.Separator] = new Vector4(0.44f, 0.37f, 0.61f, 1.0f);
			colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.74f, 0.58f, 0.98f, 1.0f);
			colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.84f, 0.58f, 1.0f, 1.0f);

			// Resize Grip
			colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.44f, 0.37f, 0.61f, 0.29f);
			colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.74f, 0.58f, 0.98f, 0.29f);
			colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.84f, 0.58f, 1.0f, 0.29f);

			// Docking
			//colors[(int)ImGuiCol.DockingPreview] = new Vector4{ 0.44f, 0.37f, 0.61f, 1.0f };

			var style = ImGui.GetStyle();
			style.TabRounding = 4;
			style.ScrollbarRounding = 9;
			style.WindowRounding = 7;
			style.GrabRounding = 3;
			style.FrameRounding = 3;
			style.PopupRounding = 4;
			style.ChildRounding = 4;
		}

		#endregion 

		public static UserRegistry Registry = new();
		public static e_Menus MenuIndex;
		private List<Menu> MenuList = new();

        protected static Error? error;

        protected override Task PostInitialized() {
            // Set up menus
            MenuList.Add(new Main());
			MenuList.Add(new Register());

            SetTheme();

            return base.PostInitialized();
        }

        protected override void Render() {
            if (error != null) {
                if (error.Render())
                    error = null;
                return;
            }

            ImGui.Begin("Object-Oriented Programming");

			ImGui.SetWindowFontScale(1.3f);

			ImGui.Dummy(new Vector2(ImGui.GetWindowWidth() - 15, 0));

			MenuList[(int)MenuIndex].Render();

			ImGui.End();
		}

		public static void ThrowError(string e_message) {
			error = new Error(e_message);
		}

		public static void FoldoutHeader(string label, ref bool foldout, Action content) {
			ImGui.SetNextItemOpen(foldout);
			foldout = ImGui.CollapsingHeader(label);

			if (foldout) {
				ImGui.Indent();
				content.Invoke();
				ImGui.Unindent();
			}
		}

        public static void CenteredWrappedText(string text, float wrapWidth = 0.0f) {
            // Get the current window's width
            float windowWidth = ImGui.GetWindowSize().X;

            // Set wrap width if not provided
            if (wrapWidth <= 0.0f) {
                wrapWidth = windowWidth;
            }

            // Calculate the text size after wrapping
            float textSizeX = ImGui.CalcTextSize(text, wrapWidth).X;
            float textPosX = (windowWidth - textSizeX) * 0.5f;

            // Center the wrapped text if it's not wider than the window
            if (textPosX > 0.0f) {
                ImGui.SetCursorPosX(textPosX);
            }
            ImGui.TextWrapped(text);
        }
    }
}
