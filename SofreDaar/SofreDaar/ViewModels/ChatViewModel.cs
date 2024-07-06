using SofreDaar.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SofreDaar.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private ChatClient _client;
        private string _message;
        private ObservableCollection<string> _messages;

        public ObservableCollection<string> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }
        public ICommand SendMessageCommand { get; set; }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public ChatViewModel(DatabaseContext DbContext, MainViewModel main) : base(DbContext, main)
        {
            try
            {
                ChatServer _server = new ChatServer("127.0.0.1", 8587);
                _server.Start();
            }
            catch (Exception ex)
            {
            }

            Messages = new ObservableCollection<string>();
            SendMessageCommand = new RelayCommand(o => SendMessage());
            _client = new ChatClient();
            _client.MessageReceived += OnMessageReceived;
            _client.Connect("127.0.0.1", 8587);
            Messages.Add("اتصال به سرور برقرار شد");
        }

        private void SendMessage()
        {
            if (!string.IsNullOrEmpty(Message))
            {
                _client.SendMessage(Message, MainVM.LoggedInUser.Name);
                Messages.Add("شما: " + Message);
                Message = string.Empty;
            }
        }

        private void OnMessageReceived(string message)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(message);
            });
        }
    }
}
