using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DailyFortuneGenerator
{
    public partial class MainForm : Form
    {
        private FortuneEngine fortuneEngine;
        private TextBox nameTextBox;
        private ComboBox categoryComboBox;
        private ComboBox moodComboBox;
        private Button generateButton;
        private RichTextBox fortuneDisplayBox;
        private CheckBox enableSoundCheckBox;
        private TrackBar luckyNumberTrackBar;
        private Label luckyNumberLabel;
        private Button saveFortuneButton;
        private Button historyButton;
        private Label titleLabel;

        public MainForm()
        {
            InitializeComponent();
            fortuneEngine = new FortuneEngine();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Daily Fortune Generator";
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "🔮 Daily Fortune Generator 🔮";
            titleLabel.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(75, 0, 130);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Location = new Point(50, 20);
            titleLabel.Size = new Size(500, 40);

            // Name input
            var nameLabel = new Label();
            nameLabel.Text = "Enter your name:";
            nameLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            nameLabel.Location = new Point(50, 80);
            nameLabel.Size = new Size(150, 25);

            nameTextBox = new TextBox();
            nameTextBox.Font = new Font("Segoe UI", 10);
            nameTextBox.Location = new Point(50, 105);
            nameTextBox.Size = new Size(200, 25);
            nameTextBox.PlaceholderText = "Your name here...";

            // Category selection
            var categoryLabel = new Label();
            categoryLabel.Text = "Fortune Category:";
            categoryLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            categoryLabel.Location = new Point(300, 80);
            categoryLabel.Size = new Size(150, 25);

            categoryComboBox = new ComboBox();
            categoryComboBox.Font = new Font("Segoe UI", 10);
            categoryComboBox.Location = new Point(300, 105);
            categoryComboBox.Size = new Size(200, 25);
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.Items.AddRange(new string[] { "Love", "Career", "Health", "Finance", "General", "Adventure" });
            categoryComboBox.SelectedIndex = 4; // Default to General

            // Mood selection
            var moodLabel = new Label();
            moodLabel.Text = "Current Mood:";
            moodLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            moodLabel.Location = new Point(50, 150);
            moodLabel.Size = new Size(150, 25);

            moodComboBox = new ComboBox();
            moodComboBox.Font = new Font("Segoe UI", 10);
            moodComboBox.Location = new Point(50, 175);
            moodComboBox.Size = new Size(200, 25);
            moodComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            moodComboBox.Items.AddRange(new string[] { "Happy", "Sad", "Excited", "Anxious", "Calm", "Adventurous" });
            moodComboBox.SelectedIndex = 0; // Default to Happy

            // Lucky number selector
            var luckyLabel = new Label();
            luckyLabel.Text = "Lucky Number Range:";
            luckyLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            luckyLabel.Location = new Point(300, 150);
            luckyLabel.Size = new Size(150, 25);

            luckyNumberTrackBar = new TrackBar();
            luckyNumberTrackBar.Location = new Point(300, 175);
            luckyNumberTrackBar.Size = new Size(150, 45);
            luckyNumberTrackBar.Minimum = 1;
            luckyNumberTrackBar.Maximum = 100;
            luckyNumberTrackBar.Value = 50;
            luckyNumberTrackBar.TickFrequency = 10;
            luckyNumberTrackBar.ValueChanged += LuckyNumberTrackBar_ValueChanged;

            luckyNumberLabel = new Label();
            luckyNumberLabel.Text = "1-50";
            luckyNumberLabel.Font = new Font("Segoe UI", 9);
            luckyNumberLabel.Location = new Point(460, 185);
            luckyNumberLabel.Size = new Size(50, 25);

            // Sound checkbox
            enableSoundCheckBox = new CheckBox();
            enableSoundCheckBox.Text = "Enable sound effects";
            enableSoundCheckBox.Font = new Font("Segoe UI", 9);
            enableSoundCheckBox.Location = new Point(50, 230);
            enableSoundCheckBox.Size = new Size(200, 25);
            enableSoundCheckBox.Checked = true;

            // Generate button
            generateButton = new Button();
            generateButton.Text = "✨ Generate My Fortune ✨";
            generateButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            generateButton.BackColor = Color.FromArgb(138, 43, 226);
            generateButton.ForeColor = Color.White;
            generateButton.Location = new Point(200, 270);
            generateButton.Size = new Size(200, 50);
            generateButton.FlatStyle = FlatStyle.Flat;
            generateButton.FlatAppearance.BorderSize = 0;
            generateButton.Click += GenerateButton_Click;

            // Fortune display
            var fortuneLabel = new Label();
            fortuneLabel.Text = "Your Fortune:";
            fortuneLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            fortuneLabel.Location = new Point(50, 340);
            fortuneLabel.Size = new Size(150, 25);

            fortuneDisplayBox = new RichTextBox();
            fortuneDisplayBox.Font = new Font("Segoe UI", 11);
            fortuneDisplayBox.Location = new Point(50, 370);
            fortuneDisplayBox.Size = new Size(500, 200);
            fortuneDisplayBox.ReadOnly = true;
            fortuneDisplayBox.BackColor = Color.FromArgb(255, 255, 240);
            fortuneDisplayBox.BorderStyle = BorderStyle.FixedSingle;

            // Action buttons
            saveFortuneButton = new Button();
            saveFortuneButton.Text = "💾 Save Fortune";
            saveFortuneButton.Font = new Font("Segoe UI", 10);
            saveFortuneButton.Location = new Point(50, 590);
            saveFortuneButton.Size = new Size(120, 35);
            saveFortuneButton.Click += SaveFortuneButton_Click;
            saveFortuneButton.Enabled = false;

            historyButton = new Button();
            historyButton.Text = "📜 View History";
            historyButton.Font = new Font("Segoe UI", 10);
            historyButton.Location = new Point(180, 590);
            historyButton.Size = new Size(120, 35);
            historyButton.Click += HistoryButton_Click;

            // Add all controls to form
            this.Controls.AddRange(new Control[] {
                titleLabel, nameLabel, nameTextBox, categoryLabel, categoryComboBox,
                moodLabel, moodComboBox, luckyLabel, luckyNumberTrackBar, luckyNumberLabel,
                enableSoundCheckBox, generateButton, fortuneLabel, fortuneDisplayBox,
                saveFortuneButton, historyButton
            });

            this.ResumeLayout(false);
        }

        private void LuckyNumberTrackBar_ValueChanged(object sender, EventArgs e)
        {
            luckyNumberLabel.Text = $"1-{luckyNumberTrackBar.Value}";
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Please enter your name first!", "Missing Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            var userInput = new UserInput
            {
                Name = nameTextBox.Text.Trim(),
                Category = categoryComboBox.SelectedItem.ToString(),
                Mood = moodComboBox.SelectedItem.ToString(),
                LuckyNumberRange = luckyNumberTrackBar.Value,
                EnableSound = enableSoundCheckBox.Checked
            };

            var fortune = fortuneEngine.GenerateFortune(userInput);
            DisplayFortune(fortune);
            saveFortuneButton.Enabled = true;

            if (enableSoundCheckBox.Checked)
            {
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        private void DisplayFortune(Fortune fortune)
        {
            fortuneDisplayBox.Clear();
            fortuneDisplayBox.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
            fortuneDisplayBox.SelectionColor = Color.FromArgb(75, 0, 130);
            fortuneDisplayBox.AppendText($"Fortune for {fortune.UserName}\n");
            fortuneDisplayBox.AppendText($"Generated on: {fortune.Timestamp:MMM dd, yyyy 'at' h:mm tt}\n\n");

            fortuneDisplayBox.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
            fortuneDisplayBox.SelectionColor = Color.Black;
            fortuneDisplayBox.AppendText($"📝 {fortune.Message}\n\n");

            fortuneDisplayBox.SelectionFont = new Font("Segoe UI", 10, FontStyle.Bold);
            fortuneDisplayBox.SelectionColor = Color.FromArgb(0, 100, 0);
            fortuneDisplayBox.AppendText($"🍀 Lucky Number: {fortune.LuckyNumber}\n");
            fortuneDisplayBox.AppendText($"🎨 Lucky Color: {fortune.LuckyColor}\n");
            fortuneDisplayBox.AppendText($"⭐ Confidence Level: {fortune.ConfidenceLevel}%");
        }

        private void SaveFortuneButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fortuneDisplayBox.Text))
                return;

            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveDialog.DefaultExt = "txt";
                    saveDialog.FileName = $"Fortune_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(saveDialog.FileName, fortuneDisplayBox.Text);
                        MessageBox.Show("Fortune saved successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving fortune: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HistoryButton_Click(object sender, EventArgs e)
        {
            var historyForm = new HistoryForm(fortuneEngine.GetFortuneHistory());
            historyForm.ShowDialog();
        }
    }
}
