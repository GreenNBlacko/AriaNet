using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PD3.GUI.Menus {
    public class Main : Menu {
        public override void Render() {
            var style = ImGui.GetStyle();

            ImGui.SeparatorText("Profile creator");
            ImGui.Spacing();

            if(ImGui.Button("Register", new Vector2(ImGui.GetWindowWidth() / 2 - style.ItemSpacing.X, ImGui.CalcTextSize("L").Y * 1.5f))) {
                ProgramGUI.MenuIndex = e_Menus.Register;
            }
            ImGui.SameLine();
            if (ImGui.Button("Login", new Vector2(ImGui.GetWindowWidth() / 2 - style.ItemSpacing.X * 2, ImGui.CalcTextSize("L").Y * 1.5f))) {
                ProgramGUI.MenuIndex = e_Menus.Login;
            }

            ImGui.Spacing();

            if (ImGui.Button("Quit", new Vector2(ImGui.GetWindowWidth() - style.ItemSpacing.X * 2, ImGui.CalcTextSize("L").Y * 1.5f))) {
                Environment.Exit(0);
            }
        }
    }
}
