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
        private const int suggestionCount = 5;
        private const float KeyDelay = 0.15f;

        public DebuggingConsole DebugConsole;
        private Text baseText;
        private RectangleShape outputBackground;
        private RectangleShape inputBackground;
        private RectangleShape autoCompletionBackgorund;
        private RectangleShape suggestionHighlighter;
        private Game game;

        private int selectedIndex = -1;
        private float elapsedTime;

        /// <summary>
        /// Constructor of DebugConsoleManager. Initzialize DebuggingConsole and prepares shapes for rendering
        /// </summary>
        /// <param name="game">Game class with references to entities and other command related stuff</param>
        /// <param name="font">Font to draw console input and output</param>
        public DebugConsoleManager(Game game, Font font)
        {
            this.game = game;
            CommandDescriptor[] commands = new CommandDescriptor[15];
            int index = 0;
            commands[index++] = new CommandDescriptor("exit", "", false, CommandHandler_Exit);
            commands[index++] = new CommandDescriptor("quit", "", false, CommandHandler_Quit);
            commands[index++] = new CommandDescriptor("help", "", false, CommandHandler_Help);
            commands[index++] = new CommandDescriptor("clear", "", false, CommandHandler_Clear);
            commands[index++] = new CommandDescriptor("list", "", false, CommandHandler_Help);
            commands[index++] = new CommandDescriptor("noclip", "", false, CommandHandler_Noclip);
            commands[index++] = new CommandDescriptor("changelevel", "", false, CommandHandler_Changelevel);
            commands[index++] = new CommandDescriptor("ent_setproperty", "", false, CommandHandler_SetProp);
            commands[index++] = new CommandDescriptor("ent_getproperty", "", false, CommandHandler_GetProp);
            commands[index++] = new CommandDescriptor("ent_getposition", "", false, CommandHandler_GetPosition);
            commands[index++] = new CommandDescriptor("ent_getgridposition", "", false, CommandHandler_GetGridPosition);
            commands[index++] = new CommandDescriptor("ent_teleport", "", false, CommandHandler_Teleport);
            commands[index++] = new CommandDescriptor("ent_spawn", "", false, CommandHandler_Spawn);
            commands[index++] = new CommandDescriptor("ent_kill", "", false, CommandHandler_Kill);
            commands[index++] = new CommandDescriptor("ent_god", "", false, CommandHandler_God);

            DebugConsole = new DebuggingConsole(commands, new DebugConsole.Color(255, 255, 255));

            baseText = new Text("", font, 14);
            baseText.Color = SFML.Graphics.Color.White;
            outputBackground = new RectangleShape(new SFML.System.Vector2f(width, outputHeight));
            outputBackground.FillColor = new SFML.Graphics.Color(0, 0, 0, 128);
            outputBackground.OutlineColor = new SFML.Graphics.Color(0, 0, 0, 200);

            inputBackground = new RectangleShape(new SFML.System.Vector2f(width, inputHeight));
            inputBackground.FillColor = new SFML.Graphics.Color(0, 0, 0, 128);
            inputBackground.OutlineColor = new SFML.Graphics.Color(0, 0, 0, 200);
            inputBackground.Position = new SFML.System.Vector2f(0, outputHeight + 5);

            autoCompletionBackgorund = new RectangleShape(new SFML.System.Vector2f(0, 0));
            autoCompletionBackgorund.FillColor = new SFML.Graphics.Color(0, 0, 0, 128);
            autoCompletionBackgorund.OutlineColor = new SFML.Graphics.Color(0, 0, 0, 200);
            autoCompletionBackgorund.Position = new SFML.System.Vector2f(0, outputHeight + inputHeight + 5);

            suggestionHighlighter = new RectangleShape(new SFML.System.Vector2f(0, 0));
            suggestionHighlighter.FillColor = new SFML.Graphics.Color(100, 100, 100, 128);
            suggestionHighlighter.OutlineColor = new SFML.Graphics.Color(100, 100, 100, 200);
            suggestionHighlighter.Position = new SFML.System.Vector2f(0, 0);
        }

        /// <summary>
        /// Updates the debugging console and handles arrowkey input
        /// </summary>
        /// <param name="elapsedSeconds">elapsed seconds since last update</param>
        public void Update(float elapsedSeconds)
        {
            if (DebugConsole.IsOpen)
            {
                DebugConsole.Update(elapsedSeconds, Keyboard.IsKeyPressed(Keyboard.Key.Left), Keyboard.IsKeyPressed(Keyboard.Key.Right));
                if (elapsedTime <= 0)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                    {
                        selectedIndex++;
                        if (selectedIndex >= suggestionCount)
                            selectedIndex = 0;

                        elapsedTime = KeyDelay;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                    {
                        selectedIndex--;
                        if (selectedIndex < 0)
                            selectedIndex = suggestionCount - 1;

                        elapsedTime = KeyDelay;
                    }
                }
                else
                {
                    elapsedTime -= elapsedSeconds;
                }
            }
        }

        /// <summary>
        /// Draws debugging console including input, output and suggestions
        /// </summary>
        /// <param name="window">window to render</param>
        public void Draw(RenderWindow window)
        {
            if (DebugConsole.IsOpen)
            {
                window.Draw(outputBackground);
                window.Draw(inputBackground);

                DrawOutput(window);

                DrawCurrentCommand(window);
                DrawSuggestions(window);
            }
        }

        /// <summary>
        /// Draws command suggestions and background
        /// </summary>
        /// <param name="window">window to render</param>
        private void DrawSuggestions(RenderWindow window)
        {
            if (DebugConsole.RenderingInfo.AutoComplete.Length > 0 && DebugConsole.RenderingInfo.CommandLine.Length > 0)
            {
                List<Text> suggestions = new List<Text>();
                float width = 0;
                for (int i = 0; i < DebugConsole.RenderingInfo.AutoComplete.Length && i < suggestionCount; i++)
                {
                    Text suggest = new Text(baseText);
                    suggest.DisplayedString = DebugConsole.RenderingInfo.AutoComplete[i];
                    suggest.Color = SFML.Graphics.Color.White;
                    suggest.Position = new SFML.System.Vector2f(autoCompletionBackgorund.Position.X, autoCompletionBackgorund.Position.Y + (i * lineSpacing));
                    suggestions.Add(suggest);
                    if (suggest.GetGlobalBounds().Width > width)
                        width = suggest.GetGlobalBounds().Width;
                }

                autoCompletionBackgorund.Size = new SFML.System.Vector2f(width, suggestions.Count * lineSpacing);
                window.Draw(autoCompletionBackgorund);

                for (int i = 0; i < suggestions.Count; i++)
                {
                    if (selectedIndex >= 0 && selectedIndex == i)
                    {
                        suggestions[i].Color = new SFML.Graphics.Color(255, 150, 0);
                        suggestionHighlighter.Position = new SFML.System.Vector2f(0, autoCompletionBackgorund.Position.Y + (selectedIndex * lineSpacing));
                        suggestionHighlighter.Size = new SFML.System.Vector2f(suggestions[i].GetGlobalBounds().Width, lineSpacing);
                        window.Draw(suggestionHighlighter);
                    }
                    window.Draw(suggestions[i]);
                }
            }
        }

        /// <summary>
        /// Draw console output lines and output box
        /// </summary>
        /// <param name="window">window to render</param>
        private void DrawOutput(RenderWindow window)
        {
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
        }

        /// <summary>
        /// Draws the current command and inputbox
        /// </summary>
        /// <param name="window">window to render</param>
        private void DrawCurrentCommand(RenderWindow window)
        {
            Text currentCommand = new Text(baseText);
            currentCommand.DisplayedString = DebugConsole.RenderingInfo.CommandLine;
            currentCommand.Color = SFML.Graphics.Color.White;
            currentCommand.Position = new SFML.System.Vector2f(10, outputHeight + (inputHeight - lineSpacing));
            window.Draw(currentCommand);
        }

        /// <summary>
        /// Event to catch all key strokes
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        public void TextEntered(object sender, TextEventArgs e)
        {
            if (DebugConsole.IsOpen)
            {
                for (int i = 0; i < e.Unicode.Length; i++)
                {
                    if (selectedIndex != -1 && e.Unicode[0] == 13 && selectedIndex < DebugConsole.RenderingInfo.AutoComplete.Length)
                    {
                        DebugConsole.CurrentCommand = DebugConsole.RenderingInfo.AutoComplete[selectedIndex];
                        selectedIndex = -1;
                        return;
                    }
                    DebugConsole.Update(0f, e.Unicode[i], false, false);
                }
            }
        }

        /// <summary>
        /// Formats console output and creates new lines if the given line is to long
        /// </summary>
        /// <param name="baseLine">unformated line</param>
        /// <returns>returns an array of lines</returns>
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

        /// <summary>
        /// Removes the last word of a given line or if it's just one word, it removes the last 10 characters.
        /// </summary>
        /// <param name="text">text to remove from</param>
        /// <param name="word">word to remove</param>
        /// <returns>returns changed text</returns>
        private string RemoveLastWord(string text, string word)
        {
            if (text.Length > word.Length)
                return text.Substring(0, text.Length - (word.Length + 1));
            else
                return text.Substring(0, text.Length - 10);
        }

        /// <summary>
        /// The Function searchs for the last word in a given string
        /// </summary>
        /// <param name="text">text to search in</param>
        /// <returns>returns last word</returns>
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

        /// <summary>
        /// Handels game quit command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_Quit(object sender, ExecuteCommandArgs e)
        {
            GlobalReferences.State = GameState.Exit;
        }

        /// <summary>
        /// Handels console exit command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_Exit(object sender, ExecuteCommandArgs e)
        {
            DebugConsole.Close();
        }

        /// <summary>
        /// Handels console help command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_Help(object sender, ExecuteCommandArgs e)
        {
            DebugConsole.WriteLine("> " + e.Command, 255, 255, 255);
            foreach (KeyValuePair<string, CommandDescriptor> command in DebugConsole.Commands)
            {
                DebugConsole.WriteLine(command.Key + ": " + command.Value.Description, 255, 255, 255);
            }
        }

        /// <summary>
        /// Handels console clear command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_Clear(object sender, ExecuteCommandArgs e)
        {
            DebugConsole.Clear();
        }

        /// <summary>
        /// Handels ent_getgridposition command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_GetGridPosition(object sender, ExecuteCommandArgs e)
        {
            DebugConsole.WriteLine("> " + e.Command.Command, 255, 255, 255);
            DebugConsole.WriteLine("X: " + game.CurrentScene.X.ToString() + ", Y:" + game.CurrentScene.Y.ToString(), 255, 255, 255);
        }

        /// <summary>
        /// Handels ent_getposition command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_GetPosition(object sender, ExecuteCommandArgs e)
        {
            DebugConsole.WriteLine("> " + e.Command.Command, 255, 255, 255);
            DebugConsole.WriteLine("X: " + game.CurrentScene.X.ToString() + ", Y:" + game.CurrentScene.Y.ToString(), 255, 255, 255);
        }

        /// <summary>
        /// Handels ent_teleport command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_Teleport(object sender, ExecuteCommandArgs e)
        {

        }

        /// <summary>
        /// Handels ent_spawn command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_Spawn(object sender, ExecuteCommandArgs e)
        {

        }

        /// <summary>
        /// Handels ent_kill command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_Kill(object sender, ExecuteCommandArgs e)
        {

        }

        /// <summary>
        /// Handels god command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_God(object sender, ExecuteCommandArgs e)
        {

        }

        /// <summary>
        /// Handels noclip command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_Noclip(object sender, ExecuteCommandArgs e)
        {

        }

        /// <summary>
        /// Handels changelevel command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_Changelevel(object sender, ExecuteCommandArgs e)
        {

        }

        /// <summary>
        /// Handels ent_setproperty command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_SetProp(object sender, ExecuteCommandArgs e)
        {

        }

        /// <summary>
        /// Handels ent_getproperty command
        /// </summary>
        /// <param name="sender">command</param>
        /// <param name="e">command args</param>
        private void CommandHandler_GetProp(object sender, ExecuteCommandArgs e)
        {

        }
    }
}
