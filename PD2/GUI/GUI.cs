using ClickableTransparentOverlay;
using ImGuiNET;
using PD2.Classes;
using System.Drawing;
using System.Numerics;

namespace PD2.GUI {
	public class ProgramGUI : Overlay {
		protected override Task PostInitialized() {
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

			return base.PostInitialized();
		}

		protected string firstName = string.Empty;
		protected string lastName = string.Empty;
		protected int year = 0;
		protected int month = 0;
		protected int day = 0;

		protected bool f_name = true;
		protected bool f_date = true;

		protected override void Render() {
			ImGui.Begin("Object-Oriented Programming");

			ImGui.SetWindowFontScale(1.3f);

			ImGui.Dummy(new Vector2(ImGui.GetWindowWidth() - 15, 0));

			FoldoutHeader("Name", ref f_name, delegate {
				ImGui.InputText("First Name", ref firstName, 30);
				if (firstName == null || firstName.Trim() == string.Empty)
					ImGui.TextColored(new Vector4(Color.Red.R, Color.Red.G, Color.Red.B, Color.Red.A), "First name cannot be null!");

				ImGui.InputText("Last Name", ref lastName, 30);
				if (lastName == null || lastName.Trim() == string.Empty)
					ImGui.TextColored(new Vector4(Color.Red.R, Color.Red.G, Color.Red.B, Color.Red.A), "Last name cannot be null!");
			});

			FoldoutHeader("Birth Date", ref f_date, delegate {
				ImGui.InputInt("Year", ref year);
				ImGui.InputInt("Month", ref month);
				ImGui.InputInt("Day", ref day);
				try {
					new Date(year, month, day);
				} catch (Exception e) {
					ImGui.TextColored(new Vector4(Color.Red.R, Color.Red.G, Color.Red.B, Color.Red.A), e.Message);
				}
			});

			try {
				Person p = new Person(firstName, lastName, year, month, day);
				ImGui.Text(p.GetPersonInfo());
				ImGui.Text(string.Format("Days until next birthday: {0}", p.BirthDate.DaysUntilBirthday));
			} catch { }


			ImGui.End();
		}

		private void FoldoutHeader(string label, ref bool foldout, Action content) {
			ImGui.SetNextItemOpen(foldout);
			foldout = ImGui.CollapsingHeader(label);

			if (foldout) {
				ImGui.Indent();
				content.Invoke();
				ImGui.Unindent();
			}
		}
	}
}
