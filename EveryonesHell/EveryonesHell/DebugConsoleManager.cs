using System;
using System.Collections.Generic;
using DebugConsole;
using SFML.Window;
using SFML.Graphics;
using System.Text;

namespace EveryonesHell
{
    public class DebugConsoleManager
    {
        private const int lineSpacing = 16;
        private const int width = 600;
        private const int outputHeight = 380;
        private const int inputHeight = 25;

        public DebuggingConsole DebugConsole;
        private Scene currentScene;
        private Text baseText;
        private RectangleShape outputBackground;
        private RectangleShape inputBackground;

        public DebugConsoleManager(Scene scene, Font font)
        {
            currentScene = scene;
            CommandDescriptor[] commands = new CommandDescriptor[14];
            int index = 0;
            commands[index++] = new CommandDescriptor("help", "", false, CommandHandler_Help);
            commands[index++] = new CommandDescriptor("clear", "", false, CommandHandler_Clear);
            commands[index++] = new CommandDescriptor("list", "", false, CommandHandler_Help);
            commands[index++] = new CommandDescriptor("getposition", "", false, CommandHandler_GetPosition);
            commands[index++] = new CommandDescriptor("getgridposition", "", false, CommandHandler_GetGridPosition);
            commands[index++] = new CommandDescriptor("teleport", "", false, CommandHandler_Teleport);
            commands[index++] = new CommandDescriptor("spawn", "", false, CommandHandler_Spawn);
            commands[index++] = new CommandDescriptor("kill", "", false, CommandHandler_Kill);
            commands[index++] = new CommandDescriptor("god", "", false, CommandHandler_God);
            commands[index++] = new CommandDescriptor("noclip", "", false, CommandHandler_Noclip);
            commands[index++] = new CommandDescriptor("changelevel", "", false, CommandHandler_Changelevel);
            commands[index++] = new CommandDescriptor("setproperty", "", false, CommandHandler_SetProp);
            commands[index++] = new CommandDescriptor("getproperty", "", false, CommandHandler_GetProp);
            commands[index++] = new CommandDescriptor("exit", "", false, CommandHandler_Exit);

            DebugConsole = new DebuggingConsole(commands);

            baseText = new Text("", font, 14);
            baseText.Color = SFML.Graphics.Color.White;
            outputBackground = new RectangleShape(new SFML.System.Vector2f(width, outputHeight));
            outputBackground.FillColor = new SFML.Graphics.Color(0, 0, 0, 128);
            outputBackground.OutlineColor = new SFML.Graphics.Color(0, 0, 0, 200);

            inputBackground = new RectangleShape(new SFML.System.Vector2f(width, inputHeight));
            inputBackground.FillColor = new SFML.Graphics.Color(0, 0, 0, 128);
            inputBackground.OutlineColor = new SFML.Graphics.Color(0, 0, 0, 200);
            inputBackground.Position = new SFML.System.Vector2f(0, outputHeight + 5);
        }

        public void Update(float elapsedMilliseconds)
        {
            if (DebugConsole.IsOpen)
            {
                DebugConsole.Update(elapsedMilliseconds, Keyboard.IsKeyPressed(Keyboard.Key.Left), Keyboard.IsKeyPressed(Keyboard.Key.Right));
            }
        }

        public void Draw(RenderWindow window)
        {
            if (DebugConsole.IsOpen)
            {
                window.Draw(outputBackground);
                window.Draw(inputBackground);

                List<Text> linesToDraw = new List<Text>();
                int avalibleLines = (int)Math.Floor((double)outputHeight / (double)lineSpacing);
                for (int i = DebugConsole.RenderingInfo.Lines.Length - 1; i >= 0 && avalibleLines > 0; i--)
                {
                    Text currentLine = new Text(baseText);
                    currentLine.DisplayedString = DebugConsole.RenderingInfo.Lines[i];
                    DebugConsole.Color color = DebugConsole.RenderingInfo.LineColors[i];
                    currentLine.Color = new SFML.Graphics.Color(color.R, color.G, color.B, color.A);

                    Text[] lines = LineWrap(currentLine);
                    for (int j = lines.Length - 1; j >= 0 && avalibleLines > 0; j--)
                    {
                        linesToDraw.Add(lines[j]);
                        avalibleLines--;
                    }
                }

                for (int i = 0; i < linesToDraw.Count; i++)
                {
                    linesToDraw[i].Position = new SFML.System.Vector2f(0, lineSpacing * ((linesToDraw.Count - 1) - i));
                    window.Draw(linesToDraw[i]);
                }
                
                Text currentCommand = new Text(baseText);
                currentCommand.DisplayedString = DebugConsole.RenderingInfo.CommandLine;
                currentCommand.Color = SFML.Graphics.Color.White;
                currentCommand.Position = new SFML.System.Vector2f(10, outputHeight + (inputHeight - lineSpacing));
                window.Draw(currentCommand);
            }
        }

        public void TextEntered(object sender, TextEventArgs e)
        {
            if (DebugConsole.IsOpen)
            {
                for (int i = 0; i < e.Unicode.Length; i++)
                {
                    DebugConsole.Update(0f, e.Unicode[i], false, false);
                }
            }
        }

        private Text[] LineWrap(Text baseLine)
        {
            string text = baseLine.DisplayedString;

            if(baseLine.GetGlobalBounds().Width <= width)
                return new Text[1] { baseLine };

            Text currentLine = new Text(baseLine);
            List<Text> lines = new List<Text>();

            StringBuilder sb = new StringBuilder(); 
            string currentText = text;
            while (currentLine.GetGlobalBounds().Width > width)
            {
                string lastWord = GetLastWord(currentText);
                if (lastWord == currentText)
                {
                    lastWord = currentText.Substring(currentText.Length - 10);
                    currentText = currentText.Substring(0, currentText.Length - 10);
                }
                else
                    currentText = RemoveLastWord(currentText, lastWord);

                if (lastWord.Length > 0)
                {
                    sb.Insert(0, " ");
                    sb.Insert(0, lastWord);
                }

                currentLine.DisplayedString = currentText;
                if(currentLine.GetGlobalBounds().Width <= width)
                {
                    lines.Add(currentLine);
                    if (sb.Length > 0)
                    {
                        currentLine = new Text(baseLine);
                        currentLine.DisplayedString = sb.ToString().Trim();
                        currentText = sb.ToString().Trim();
                        sb.Clear();
                        if (currentLine.GetGlobalBounds().Width <= width)
                        {
                            lines.Add(currentLine);
                            break;
                        }
                    }
                }
            }

            return lines.ToArray();
        }

        private string RemoveLastWord(string text, string word)
        {
            if (text.Length > word.Length)
                return text.Substring(0, text.Length - (word.Length + 1));
            else
                return text.Substring(0, text.Length - 10);
        }

        private string GetLastWord(string text)
        {
            string[] words = text.Split(' ');
            if (words != null && words.Length > 0)
            {
                if (words[words.Length - 1].Length > 0)
                    return words[words.Length - 1];
                else
                    return " ";
            }

            return null;
        }

        private void CommandHandler_Exit(object sender, ExecuteCommandArgs e)
        {
            DebugConsole.Close();
        }

        private void CommandHandler_Help(object sender, ExecuteCommandArgs e)
        {
            DebugConsole.WriteLine("> " + e.Command, 255, 255, 255);
            foreach (KeyValuePair<string, CommandDescriptor> command in DebugConsole.Commands)
            {
                DebugConsole.WriteLine(command.Key + ": " + command.Value.Description, 255, 255, 255);
            }
        }

        private void CommandHandler_Clear(object sender, ExecuteCommandArgs e)
        {
            DebugConsole.Clear();
        }

        private void CommandHandler_GetGridPosition(object sender, ExecuteCommandArgs e)
        {
        }

        private void CommandHandler_GetPosition(object sender, ExecuteCommandArgs e)
        {
        }

        private void CommandHandler_Teleport(object sender, ExecuteCommandArgs args)
        {

        }

        private void CommandHandler_Spawn(object sender, ExecuteCommandArgs args)
        {

        }

        private void CommandHandler_Kill(object sender, ExecuteCommandArgs args)
        {

        }
        private void CommandHandler_God(object sender, ExecuteCommandArgs args)
        {

        }
        private void CommandHandler_Noclip(object sender, ExecuteCommandArgs args)
        {

        }
        private void CommandHandler_Changelevel(object sender, ExecuteCommandArgs args)
        {

        }

        private void CommandHandler_SetProp(object sender, ExecuteCommandArgs e)
        {

        }

        private void CommandHandler_GetProp(object sender, ExecuteCommandArgs e)
        {

        }
    }
}
