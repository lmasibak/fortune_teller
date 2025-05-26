using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DailyFortuneGenerator
{
    public partial class HistoryForm : Form
    {
        private List<Fortune> fortunes;
        private ListBox fortuneListBox;
        private RichTextBox detailsTextBox;
        private Button clearHistoryButton;
        private Button exportButton;

        public HistoryForm(List<Fortune> fortuneHistory)
        {
            fortunes = fortuneHistory;
            InitializeComponent();
            LoadFortuneHistory();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Fortune History";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Title
            var titleLabel = new Label();
            titleLabel.Text = "📜 Your Fortune History";
            titleLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(75, 0, 130);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(300, 30);

            // Fortune list
            var listLabel = new Label();
            listLabel.Text = "Previous Fortunes:";
            listLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            listLabel.Location = new Point(20, 60);
            listLabel.Size = new Size(150, 25);

            fortuneListBox = new ListBox();
            fortuneListBox.Font = new Font("Segoe UI", 9);
            fortuneListBox.Location = new Point(20, 85);
            fortuneListBox.Size = new Size(350, 400);
            fortuneListBox.SelectedIndexChanged += FortuneListBox_SelectedIndexChanged;

            // Details panel
            var detailsLabel = new Label();
            detailsLabel.Text = "Fortune Details:";
            detailsLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            detailsLabel.Location = new Point(390, 60);
            detailsLabel.Size = new Size(150, 25);

            detailsTextBox = new RichTextBox();
            detailsTextBox.Font = new Font("Segoe UI", 10);
            detailsTextBox.Location = new Point(390, 85);
            detailsTextBox.Size = new Size(370, 400);
            detailsTextBox.ReadOnly = true;
            detailsTextBox.BackColor = Color.FromArgb(255, 255, 240);

            // Buttons
            clearHistoryButton = new Button();
            clearHistoryButton.Text = "🗑️ Clear History";
            clearHistoryButton.Font = new Font("Segoe UI", 10);
            clearHistoryButton.Location = new Point(20, 500);
            clearHistoryButton.Size = new Size(120, 35);
            clearHistoryButton.Click += ClearHistoryButton_Click;

            exportButton = new Button();
            exportButton.Text = "📤 Export History";
            exportButton.Font = new Font("Segoe UI", 10);
            exportButton.Location = new Point(150, 500);
            exportButton.Size = new Size(120, 35);
            exportButton.Click += ExportButton_Click;

            var closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Font = new Font("Segoe UI", 10);
            closeButton.Location = new Point(640, 500);
            closeButton.Size = new Size(120, 35);
            closeButton.Click += (s, e) => this.Close();

            // Add controls
            this.Controls.AddRange(new Control[] {
                titleLabel, listLabel, fortuneListBox, detailsLabel, detailsTextBox,
                clearHistoryButton, exportButton, closeButton
            });

            this.ResumeLayout(false);
        }

        private void LoadFortuneHistory()
        {
            fortuneListBox.Items.Clear();
            foreach (var fortune in fortunes)
            {
                var displayText = $"{fortune.Timestamp:MMM dd, yyyy h:mm tt} - {fortune.Category} ({fortune.UserName})";
                fortuneListBox.Items.Add(displayText);
            }

            if (fortuneListBox.Items.Count == 0)
            {
                fortuneListBox.Items.Add("No fortunes generated yet.");
                clearHistoryButton.Enabled = false;
                exportButton.Enabled = false;
            }
        }

        private void FortuneListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fortuneListBox.SelectedIndex >= 0 && fortuneListBox.SelectedIndex < fortunes.Count)
            {
                var selectedFortune = fortunes[fortuneListBox.SelectedIndex];
                DisplayFortuneDetails(selectedFortune);
            }
        }

        private void DisplayFortuneDetails(Fortune fortune)
        {
            detailsTextBox.Clear();
            detailsTextBox.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
            detailsTextBox.SelectionColor = Color.FromArgb(75, 0, 130);
            detailsTextBox.AppendText($"Fortune for {fortune.UserName}\n");
            detailsTextBox.AppendText($"{fortune.Timestamp:MMMM dd, yyyy 'at' h:mm tt}\n\n");

            detailsTextBox.SelectionFont = new Font("Segoe UI", 10, FontStyle.Bold);
            detailsTextBox.SelectionColor = Color.Black;
            detailsTextBox.AppendText($"Category: {fortune.Category}\n");
            detailsTextBox.AppendText($"Mood: {fortune.Mood}\n\n");

            detailsTextBox.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
            detailsTextBox.AppendText($"📝 Message:\n{fortune.Message}\n\n");

            detailsTextBox.SelectionFont = new Font("Segoe UI", 10, FontStyle.Bold);
            detailsTextBox.SelectionColor = Color.FromArgb(0, 100, 0);
            detailsTextBox.AppendText($"🍀 Lucky Number: {fortune.LuckyNumber}\n");
            detailsTextBox.AppendText($"🎨 Lucky Color: {fortune.LuckyColor}\n");
            detailsTextBox.AppendText($"⭐ Confidence Level: {fortune.ConfidenceLevel}%");
        }

        private void ClearHistoryButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to clear all fortune history?", 
                "Confirm Clear History", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                fortunes.Clear();
                LoadFortuneHistory();
                detailsTextBox.Clear();
                MessageBox.Show("Fortune history cleared successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (fortunes.Count == 0)
            {
                MessageBox.Show("No fortunes to export!", "No Data", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveDialog.DefaultExt = "txt";
                    saveDialog.FileName = $"FortuneHistory_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var exportText = "DAILY FORTUNE GENERATOR - HISTORY EXPORT\n";
                        exportText += $"Exported on: {DateTime.Now:MMMM dd, yyyy 'at' h:mm tt}\n";
                        exportText += new string('=', 60) + "\n\n";

                        foreach (var fortune in fortunes)
                        {
                            exportText += fortune.ToString() + "\n\n";
                        }

                        System.IO.File.WriteAllText(saveDialog.FileName, exportText);
                        MessageBox.Show("Fortune history exported successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting history: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
