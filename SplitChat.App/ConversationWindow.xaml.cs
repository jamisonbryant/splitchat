using SplitChat.Core.Model;
using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SplitChat.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ConversationWindow : Window
    {
        /// <summary>
        /// Conversation model for this chat window
        /// </summary>
        private Conversation conversation;

        /// <summary>
        /// Class constructor
        /// </summary>
        public ConversationWindow()
        {
            InitializeComponent();

            // Initialize object
            conversation = new Conversation();

            // Register event listener for new messages
            conversation.Messages.CollectionChanged += 
                new NotifyCollectionChangedEventHandler(UpdateConversationWindow);

            // Point conversation browser to conversation file
            string dataDirUri = new System.Uri(conversation.DataDir).AbsoluteUri;
            ConversationBrowser.Address = dataDirUri + "/conversation.html";

            // Focus on chat text box
            MessageTextBox.Focus();
        }

        /// <summary>
        /// Displays a new message in the conversation window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateConversationWindow(object sender, NotifyCollectionChangedEventArgs e)
        {
            ConversationBrowser.GetBrowser().Reload(); 
        }

        /// <summary>
        /// Takes an action when a key is pressed in the message text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Enter key was pressed
            if (e.Key == Key.Enter)
            {
                // Create Message object
                Message m = new Message();

                // Check if message was a command
                // If not, it must have been a text-type (system- and invisible-type
                // messages cannot be generated via direct user input).
                if (Regex.IsMatch(MessageTextBox.Text, @"\s*\/"))
                {
                    m.Type = Message.MessageTypes.Command;
                }
                else
                {
                    m.Type = Message.MessageTypes.Text;
                    m.Sender = Environment.UserName;
                    m.Text = MessageTextBox.Text;
                }

                // Add message to conversation
                conversation.AddMessage(m);

                // Clear text box
                MessageTextBox.Text = "";
            }
            else
            {
                //SendTypingNotification();
            }
        }
    }
}
