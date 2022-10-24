using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    public partial class MainWindow : Window
    {
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        DispatcherTimer timer = new DispatcherTimer();
        int secondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            secondsElapsed++;
            timeTextBlock.Text = (secondsElapsed / 10F).ToString("0.0s");
            if(matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " You Won!";
            }
        }

        private void SetUpGame()
        {
            Random random = new Random();
            List<string> animalEmoji = new List<string>()
            {
                "🎄", "🎄",
                "🎉","🎉",
                "❄️","❄️",
                "🌟","🌟",
                "☃️","☃️",
                "🎅🏾","🎅🏾",
                "🍬","🍬",
                "🧸","🧸",
            };

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                try
                {
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
                catch (Exception)
                {
                    break;
                }
            }

            timer.Start();
            secondsElapsed = 0;
            matchesFound = 0;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text.Equals(lastTextBlockClicked.Text))
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}
