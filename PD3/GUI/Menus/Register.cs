using ImGuiNET;
using System.Numerics;
using System.Drawing;
using PD3.Classes;

namespace PD3.GUI.Menus {
    public class Register : Menu {
        protected string firstName = string.Empty;
        protected string lastName = string.Empty;
        protected string password = string.Empty;
        protected int year = 0;
        protected int month = 0;
        protected int day = 0;

        public override void Render() {
            ImGui.SeparatorText("Register");
            ImGui.Spacing();
            ImGui.SeparatorText("User info");
            ImGui.InputText("First name", ref firstName, 30);
            if (firstName == null || firstName.Trim() == string.Empty)
                ImGui.TextColored(new Vector4(Color.Red.R, Color.Red.G, Color.Red.B, Color.Red.A), "First name field cannot be empty!");

            ImGui.InputText("Last name", ref lastName, 30);
            if (lastName == null || lastName.Trim() == string.Empty)
                ImGui.TextColored(new Vector4(Color.Red.R, Color.Red.G, Color.Red.B, Color.Red.A), "Last name field cannot be empty!" +
                    " cannot be empty!");

            ImGui.InputText("Password", ref password, 32, ImGuiInputTextFlags.Password);
            if (lastName == null || lastName.Trim() == string.Empty)
                ImGui.TextColored(new Vector4(Color.Red.R, Color.Red.G, Color.Red.B, Color.Red.A), "Password field cannot be empty!");

            ImGui.SeparatorText("Birth date");

            ImGui.InputInt("Year", ref year);
            ImGui.InputInt("Month", ref month);
            ImGui.InputInt("Day", ref day);
                
            try {
                var date = new Date(year, month, day);

                try {
                    User user = new(fi)
                }
            } catch (Exception e) {
                ImGui.TextColored(new Vector4(Color.Red.R, Color.Red.G, Color.Red.B, Color.Red.A), e.Message);
            }
        }
    }
}
