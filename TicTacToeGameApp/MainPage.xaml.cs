namespace TicTacToeGameApp
{
    public partial class MainPage : ContentPage
    {
        private bool _isPlayerXTurn = true;
        private bool _isGameOver = false;
        private Button[,] _buttons;

        public MainPage()
        {
            InitializeComponent();
            _buttons = new Button[,]
            {
                { btn00, btn01, btn02 },
                { btn10, btn11, btn12 },
                { btn20, btn21, btn22 }
            };
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            foreach (var button in _buttons)
            {
                button.Text = "";
                button.Clicked += OnButtonClicked;
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (_isGameOver)
                return;
            Button button = (Button)sender;
            if (string.IsNullOrEmpty(button.Text))
            {
                button.Text = _isPlayerXTurn ? "X" : "O";

                if (CheckForWinner())
                {
                    DisplayGameOverMessage("X 贏了!");
                    _isGameOver = true;
                }
                else if (IsBoardFull())
                {
                    DisplayGameOverMessage("平局!");
                    _isGameOver = true;
                }
                else
                {
                    _isPlayerXTurn = !_isPlayerXTurn;

                    if (!_isPlayerXTurn)
                    {
                        ComputerMove();
                        if (CheckForWinner())
                        {
                            DisplayGameOverMessage("O 贏了!");
                            _isGameOver = true;
                        }
                        else if (IsBoardFull())
                        {
                            DisplayGameOverMessage("平局!");
                            _isGameOver = true;
                        }
                    }
                }
            }
        }

        private void ComputerMove()
        {
            Random random = new Random();
            int row, column;
            do
            {
                row = random.Next(0, 3);
                column = random.Next(0, 3);
            } while (!string.IsNullOrEmpty(_buttons[row, column].Text));
            _buttons[row, column].Text = "O";
            _isPlayerXTurn = true;
        }

        private bool CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_buttons[i, 0].Text == _buttons[i, 1].Text && _buttons[i, 1].Text == _buttons[i, 2].Text && !string.IsNullOrEmpty(_buttons[i, 0].Text))
                    return true;
            }
            for (int i = 0; i < 3; i++)
            {
                if (_buttons[0, i].Text == _buttons[1, i].Text && _buttons[1, i].Text == _buttons[2, i].Text && !string.IsNullOrEmpty(_buttons[0, i].Text))
                    return true;
            }
            if (_buttons[0, 0].Text == _buttons[1, 1].Text && _buttons[1, 1].Text == _buttons[2, 2].Text && !string.IsNullOrEmpty(_buttons[0, 0].Text))
                return true;
            if (_buttons[0, 2].Text == _buttons[1, 1].Text && _buttons[1, 1].Text == _buttons[2, 0].Text && !string.IsNullOrEmpty(_buttons[0, 2].Text))
                return true;
            return false;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(_buttons[i, j].Text))
                        return false;
                }
            }
            return true;
        }

        private async void DisplayGameOverMessage(string message)
        {
            string result = await DisplayActionSheet("Game Over", null, null, message, "Play Again");
            if (result == "Play Again")
            {
                ResetGame();
            }
        }

        private void ResetGame()
        {
            _isPlayerXTurn = true;
            _isGameOver = false;
            foreach (var button in _buttons)
            {
                button.Text = "";
            }
        }
    }

}
