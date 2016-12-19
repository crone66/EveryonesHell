using System;
using System.Linq;
using FileDescriptions;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EveryonesHell.HUD
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogSystem : HudElement
    {
        private const int padding = 10;
        private const int lineSpacing = 30;
        private const int answerSpacing = 35;
        private const float selectionDelay = 0.20f;
        private const int answerCharacterSize = 28;
        private const int textCharacterSize = 36;

        private Dialog currentDialog;
        private Dialog[] answers;

        private Vector2f position;
        private Vector2f size;
        private Font font;
        private DialogCollection dialogs;

        private Text[] dialogLines;
        private Text[][] answerLines;

        private RectangleShape shape;
        private int selectedAnswer;

        private Color selectionColor;
        private Color textColor;
        private float elapsedTime;

        public event EventHandler<DialogChangedArgs> OnDialogChanged;

        /// <summary>
        /// Initzializes a dialog system to handle and draw dialogs
        /// </summary>
        /// <param name="dialogs">Dialog collection which contains all availble dialogs</param>
        /// <param name="position">Dialogbox position</param>
        /// <param name="size">Dialogbox size</param>
        /// <param name="font">Font for dialog output</param>
        /// <param name="textColor">Text color</param>
        /// <param name="selectionColor">Color of selected answer</param>
        public DialogSystem(DialogCollection dialogs, Vector2f position, Vector2f size, Font font, Color textColor, Color selectionColor)
            : base(true, true)
        {
            this.dialogs = dialogs;
            this.position = position;
            this.size = size;
            this.font = font;
            this.textColor = textColor;
            this.selectionColor = selectionColor;

            shape = new RectangleShape(new Vector2f(size.X - 4, size.Y - 2));
            shape.Position = new Vector2f(position.X + 2, position.Y - 2);
            shape.OutlineThickness = 2;
            shape.OutlineColor = Color.Black;
            shape.FillColor = new Color(80, 80, 80, 210);
            elapsedTime = 0f;
        }

        /// <summary>
        /// Updates Dialogsystem
        /// </summary>
        public override void Update(float elapsedSeconds)
        {
            Input(elapsedSeconds);
            elapsedTime -= elapsedSeconds;
            for (int i = 0; i < dialogLines.Length; i++)
            {
                dialogLines[i].Position = new Vector2f(position.X + padding, position.Y + (i * lineSpacing) + padding);
            }

            for (int i = 0; i < answerLines.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < answerLines[i].Length; j++)
                {
                    answerLines[i][j].Position = new Vector2f(position.X + padding, position.Y + ((i + 2) * answerSpacing) + (count * lineSpacing));
                    count++;
                }
            }
        }

        /// <summary>
        /// Moves the answer selection up
        /// </summary>
        public void SelectionUp()
        {
            if (elapsedTime <= 0)
            {
                ChangeSelection(selectedAnswer - 1);
                elapsedTime = selectionDelay;
            }
        }

        /// <summary>
        /// Moves the answer selection down
        /// </summary>
        public void SelectionDown()
        {
            if (elapsedTime <= 0)
            {
                ChangeSelection(selectedAnswer + 1);
                elapsedTime = selectionDelay;
            }
        }
    

        /// <summary>
        /// Handles next dialog step
        /// </summary>
        public void Next()
        {
            if (elapsedTime <= selectionDelay * -1)
            {
                elapsedTime = selectionDelay;
                if (answers != null && answers.Length > 0)
                {
                    Open(answers[selectedAnswer].NextDialogId);
                }
                else if (currentDialog.NextDialogId >= 0)
                {
                    Open(currentDialog.NextDialogId);
                }
                else
                {
                    IsOpen = false;

                    OnDialogChanged?.Invoke(this, new DialogChangedArgs(-1, currentDialog.DialogID));
                }
            }
        }
        /// <summary>
        /// Handels Dialog input
        /// </summary>
        public void Input(float elapsedSeconds)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                SelectionUp();
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                SelectionDown();
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                Next();
            }
        }

        /// <summary>
        /// Draws Dialogsystem
        /// </summary>
        public override void Draw(RenderWindow window)
        {
            window.Draw(shape);

            for (int i = 0; i < dialogLines.Length; i++)
            {
                window.Draw(dialogLines[i]);
            }

            for (int i = 0; i < answerLines.Length; i++)
            {
                for (int j = 0; j < answerLines[i].Length; j++)
                {
                    window.Draw(answerLines[i][j]);
                }
            }
        }
    

        /// <summary>
        /// Opens a dialog window
        /// </summary>
        /// <param name="dialogId">Indicates which dialog should be displayed</param>
        public void Open(int dialogId)
        {
            try
            {
                if (dialogId >= 0)
                {
                    Open(dialogs.Dialogs.First(d => d.DialogID == dialogId));
                }
                else
                {
                    IsOpen = false;

                    OnDialogChanged?.Invoke(this, new DialogChangedArgs(-1 ,currentDialog.DialogID));

                }
            }
            catch(Exception ex)
            {
                GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine("Couldn't load dialog ID: " + dialogId.ToString(), 255, 0, 0);
                GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(ex.Message, 255, 0, 0);
                IsOpen = false;

                OnDialogChanged?.Invoke(this, new DialogChangedArgs(-1, currentDialog.DialogID));
            }
        }

        /// <summary>
        /// Opens a dialog window
        /// </summary>
        /// <param name="dialogId">Indicates which dialog should be displayed</param>
        /// <param name="position">Render position of dialog</param>
        public void Open(int dialogId, Vector2f position)
        {
            Vector2f offset = new Vector2f((GlobalReferences.MainGame.WindowWidth / 2) - shape.Size.X, (GlobalReferences.MainGame.WindowHeight / 2) - shape.Size.Y);
            this.position = position + offset;
            shape.Position = position + offset;
            Open(dialogId);
        }

        /// <summary>
        /// Opens a dialog window
        /// </summary>
        /// <param name="dialog">Dialog to start with</param>
        public void Open(Dialog dialog)
        {
            OnDialogChanged?.Invoke(this, new DialogChangedArgs(dialog.DialogID, currentDialog.DialogID));
            elapsedTime = selectionDelay;
            currentDialog = dialog;
            selectedAnswer = 0;

            Text baseDialogText = new Text(currentDialog.DialogText, font, textCharacterSize);
            baseDialogText.Color = textColor;
            dialogLines = UIHelper.LineWrap(baseDialogText, Convert.ToInt32(size.X) - (padding * 2));
            IsOpen = true;
            if (currentDialog.DialogAnswerIds != null)
            {
                answers = new Dialog[currentDialog.DialogAnswerIds.Length];
                answerLines = new Text[currentDialog.DialogAnswerIds.Length][];
                for (int i = 0; i < currentDialog.DialogAnswerIds.Length; i++)
                {
                    try
                    {
                        answers[i] = dialogs.Dialogs.First(d => d.DialogID == currentDialog.DialogAnswerIds[i]);
                        Text baseText = new Text(answers[i].DialogText, font, answerCharacterSize);
                        baseText.Color = i == 0 ? selectionColor : textColor;
                        answerLines[i] = UIHelper.LineWrap(baseText, Convert.ToInt32(size.X) - (padding * 2));
                    }
                    catch(Exception ex)
                    {
                        GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine("Couldn't load dialog answer ID: " + currentDialog.DialogAnswerIds[i].ToString(), 255, 0 ,0);
                        GlobalReferences.MainGame.ConsoleManager.DebugConsole.WriteLine(ex.Message, 255, 0, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Changes the selected answer
        /// </summary>
        /// <param name="newIndex">Answer index</param>
        private void ChangeSelection(int newIndex)
        {
            ChangeLineColor(selectedAnswer, textColor);
            selectedAnswer = (newIndex < 0 ? answers.Length - 1 : (newIndex >= answers.Length ? 0 : newIndex));
            ChangeLineColor(selectedAnswer, selectionColor);
        }

        /// <summary>
        /// Changes the color of a line
        /// </summary>
        /// <param name="index">Line index</param>
        /// <param name="color">text color</param>
        private void ChangeLineColor(int index, Color color)
        {
            if (answerLines != null && index < answerLines.Length && answerLines[index] != null)
            {
                for (int i = 0; i < answerLines[index].Length; i++)
                {
                    answerLines[index][i].Color = color;
                }
            }
        }
    }
}
