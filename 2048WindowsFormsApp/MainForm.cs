using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048WindowsFormsApp
{
    public partial class MainForm : Form
    {
        private const int mapSize = 4;
        private Label[,] labelsMap;
        private static Random random = new Random();
        private int score = 0;
        private int bestScore = 0;
        private string userName;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var startForm = new StartForm();
            startForm.ShowDialog();
            userName = startForm.userNameTextBox.Text;

            InitMap();
            GenerateNumber();
            ShowScore();
            CalculsteBestScore();
        }

        private void CalculsteBestScore()
        {
            var users = UserManager.GetAll();
            if(users.Count == 0)
            {
                return;
            }
            
            bestScore = users[0].Score;
            foreach(var user in users)
            {
                if(user.Score > bestScore)
                {
                    bestScore = user.Score;
                }
            }
            ShowBestScore();
        }

        private void ShowScore()
        {
            scoreLabel.Text = score.ToString();
        }
        private void ShowBestScore()
        {
            if(score > bestScore)
            {  bestScore = score;}
            bestScoreLabel.Text = bestScore.ToString();
        }

        private void InitMap()
        {
            labelsMap = new Label[mapSize,mapSize];

            for(int i = 0; i < mapSize; i++)
            {
                for(int j = 0; j < mapSize; j++)
                {
                    var newLabel = CreateLabel(i,j);
                    Controls.Add(newLabel);
                    labelsMap[i,j] = newLabel;
                }
            }
        }
        private void GenerateNumber()
        {
            var random = new Random();
            for(int i = 0; i < mapSize*mapSize;i++)
            {
                var randomNumberLabel = random.Next(mapSize * mapSize);
                int indexRow = randomNumberLabel / mapSize;
                int indexColum = randomNumberLabel % mapSize;
                if (labelsMap[indexRow, indexColum].Text == string.Empty)
                {
                    var randomNumber = random.Next(1, 101);
                    if (randomNumber <= 75)
                    {
                        labelsMap[indexRow, indexColum].Text = "2";
                    }
                    else
                    {
                        labelsMap[indexRow, indexColum].Text = "4";
                    }
                    break;
                }
            }
        }
        private Label CreateLabel(int indexRow, int indexColum)
        {
            var label = new Label();
            label.BackColor = SystemColors.ControlDark;
            label.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            int x = 10 + indexColum * 76;
            int y = 70 + indexRow * 76;
            label.Location = new Point(x, y);
            label.Size = new Size(70, 70);
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Right && e.KeyCode != Keys.Left && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            { return; }

            if(e.KeyCode == Keys.Right)
            {
                MoveRight();
            }
            if (e.KeyCode == Keys.Left)
            {
                MoveLeft();
            }
            if (e.KeyCode == Keys.Up)
            {
                MoveUp();
            }
            if (e.KeyCode == Keys.Down)
            {
                MoveDown();
            }

            GenerateNumber();
            ShowScore();
            ShowBestScore();

            if(Win())
            {
                UserManager.Add(new User() { Name = userName, Score = score });
                MessageBox.Show("Победа!");
                return;
            }
            if(EndGame())
            {
                UserManager.Add(new User() { Name = userName, Score = score });
                MessageBox.Show(" вы проиграли(");
                return;
            }

        }

        private void MoveDown()
        {
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = mapSize - 1; i >= 0; i--)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    score += number * 2;
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    labelsMap[k, j].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = mapSize - 1; i >= 0; i--)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = i - 1; k >= 0; k--)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[k, j].Text;
                                labelsMap[k, j].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveUp()
        {
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = i + 1; k < mapSize; k++)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    score += number * 2;
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    labelsMap[k, j].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int j = 0; j < mapSize; j++)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = i + 1; k < mapSize; k++)
                        {
                            if (labelsMap[k, j].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[k, j].Text;
                                labelsMap[k, j].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveLeft()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = j + 1; k < mapSize; k++)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    score += number * 2;
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    labelsMap[i, k].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = j + 1; k < mapSize; k++)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[i, k].Text;
                                labelsMap[i, k].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void MoveRight()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = mapSize - 1; j >= 0; j--)
                {
                    if (labelsMap[i, j].Text != string.Empty)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                {
                                    var number = int.Parse(labelsMap[i, j].Text);
                                    score += number * 2;
                                    labelsMap[i, j].Text = (number * 2).ToString();
                                    labelsMap[i, k].Text = string.Empty;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = mapSize - 1; j >= 0; j--)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (labelsMap[i, k].Text != string.Empty)
                            {
                                labelsMap[i, j].Text = labelsMap[i, k].Text;
                                labelsMap[i, k].Text = string.Empty;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private bool EndGame()
        {
            for (int i = 0; i < mapSize;i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == "")
                    { return false; }
                }
            }

            for (int i = 0; i < mapSize -1; i++)
            {
                for (int j = 0; j < mapSize - 1; j++)
                {
                    if (labelsMap[i, j].Text == labelsMap[i, j + 1].Text || labelsMap[i, j].Text == labelsMap[i + 1, j].Text)
                    { return false; }
                }
            }
            return true;
        }
        private bool Win()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == "2048")
                    { return true; }
                }
            }
            return false;
        }

        private void restartToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UserManager.Add(new User() { Name = userName, Score = score });
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserManager.Add(new User() { Name = userName, Score = score });
            Application.Exit();
        }

        private void rulesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var rulesForm = new RulesForm();
            rulesForm.ShowDialog();
        }

        private void resultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var resultForm = new ResultForm();
            resultForm.ShowDialog();
        }
    }
}
