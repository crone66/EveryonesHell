using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileDescriptions;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace EveryonesHell.HUD
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogSystem
    {
        private const int padding = 5;
        private const int lineSpacing = 18;
        private const int answerSpacing = 25;
        private const float selectionDelay = 0.25f;


        private bool isVisable;

        private Dialog currentDialog;
        private Dialog[] answers;

        private Vector2f position;
        private Vector2i size;
        private Sprite background;
        private Font font;
        private DialogCollection dialogs;

        private Text[] dialogLines;
        private Text[][] answerLines;

        private int selectedAnswer;

        private Color selectionColor;
        private Color textColor;
        private float elapsedTime;

        public bool IsVisable
        {
            get
            {
                return isVisable;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="background"></param>
        /// <param name="font"></param>
        public DialogSystem(DialogCollection dialogs, Vector2f position, Vector2i size, Sprite background, Font font, Color textColor, Color selectionColor)
        {
            this.dialogs = dialogs;
            this.position = position;
            this.size = size;
            this.background = background;
            this.font = font;
            this.textColor = textColor;
            this.selectionColor = selectionColor;
            elapsedTime = 0f;
        }

        /// <summary>
        /// Updates Dialogsystem
        /// </summary>
        public void Update(float elapsedSeconds)
        {
            if (isVisable)
            {
                int count = 0;
                for (int i = 0; i < dialogLines.Length; i++)
                {
                    dialogLines[i].Position = new Vector2f(position.X + padding, position.Y + (i * lineSpacing));
                    count++;
                }

                for (int i = 0; i < answerLines.Length; i++)
                {
                    for (int j = 0; j < answerLines[i].Length; j++)
                    {
                        answerLines[i][j].Position = new Vector2f(position.X + padding, position.Y + ((i + 1) * answerSpacing) + (count * lineSpacing));
                        count++;
                    }
                }
            }
        }

        /// <summary>
        /// Handels Dialog input
        /// </summary>
        public void Input(float elapsedSeconds)
        {
            if (isVisable)
            {
                elapsedTime -= elapsedSeconds;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && elapsedTime <= 0)
                {
                    ChangeSelection(selectedAnswer - 1);
                    elapsedTime = selectionDelay;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && elapsedTime <= 0)
                {
                    ChangeSelection(selectedAnswer + 1);
                    elapsedTime = selectionDelay;
                }
                else if(Keyboard.IsKeyPressed(Keyboard.Key.Return) && elapsedTime <= selectionDelay * -1)
                {
                    if(answers != null)
                    {
                        Open(answers[selectedAnswer]);
                    }
                    else if(currentDialog.NextDialogId >= 0)
                    {
                        Open(currentDialog.NextDialogId);
                    }
                    else
                    {
                        isVisable = false;
                    }
                }
            }
        }

        /// <summary>
        /// Draws Dialogsystem
        /// </summary>
        public void Draw(RenderWindow window)
        {
            if(isVisable)
            {
                window.Draw(background);

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialogId"></param>
        public void Open(int dialogId)
        {
            Open(dialogs.Dialogs.First(d => d.DialogID == dialogId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialog"></param>
        public void Open(Dialog dialog)
        {
            currentDialog = dialog;
            Text baseDialogText = new Text(currentDialog.DialogText, font);
            dialogLines = UIHelper.LineWrap(baseDialogText, size.X - (padding * 2));

            if (currentDialog.DialogAnswerIds != null)
            {
                answers = new Dialog[currentDialog.DialogAnswerIds.Length];
                for (int i = 0; i < currentDialog.DialogAnswerIds.Length; i++)
                {
                    answers[i] = dialogs.Dialogs.First(d => d.DialogID == currentDialog.DialogAnswerIds[i]);
                    Text baseText = new Text(answers[i].DialogText, font);
                    answerLines[i] = UIHelper.LineWrap(baseText, size.X - (padding * 2));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newIndex"></param>
        private void ChangeSelection(int newIndex)
        {
            selectedAnswer = (selectedAnswer < 0 ? answers.Length - 1 : (selectedAnswer >= answers.Length ? 0 : newIndex));
            for (int i = 0; i < answerLines[newIndex].Length; i++)
            {
                answerLines[newIndex][i].Color = selectionColor;
            }
        }
    }
}
