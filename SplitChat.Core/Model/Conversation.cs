using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitChat.Core.Model
{
    /// <summary>
    /// Conversation model
    /// </summary>
    public class Conversation
    {
        /// <summary>
        /// Conversation GUID
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Messages in the conversation
        /// </summary>
        public ObservableCollection<Message> Messages { get; }

        /// <summary>
        /// Path to directory in which conversation data is stored
        /// </summary>
        public string DataDir { get; }

        /// <summary>
        /// Conversation data file
        /// </summary>
        private HtmlDocument dataFile;

        /// <summary>
        /// Class constructor
        /// </summary>
        public Conversation()
        {
            // Initialize object
            Id = Guid.NewGuid();
            Messages = new ObservableCollection<Message>();
            DataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SplitChat", "data", "conversations", Id.ToString()
            );

            // Create conversation data dir
            if (!Directory.Exists(DataDir))
            {
                CreateDataDir();
            }

            // Print conversation ID as system message
            AddMessage(new Message()
            {
                Type = Message.MessageTypes.System,
                Sender = "System",
                Text = string.Format("Conversation ID: {0}", Id)
            });
        }

        /// <summary>
        /// Creates the files and directories required to support the conversation
        /// </summary>
        private void CreateDataDir()
        {
            // Copy template conversation structure from ./app/skel/conversations/default
            Core.Utility.Filesystem.Copy(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SplitChat", "data", "conversations", "template"
            ), DataDir);

            // Initialize conversation data file
            dataFile = new HtmlDocument();
            dataFile.Load(Path.Combine(DataDir, "conversation.html"));
        }

        /// <summary>
        /// Sets the title in the conversation data file
        /// </summary>
        /// <param name="title"></param>
        private void SetConversationTitle(string title)
        {
            dataFile.DocumentNode.SelectSingleNode("//title").InnerHtml = title;
            dataFile.Save(Path.Combine(DataDir, "conversation.html"));
        }

        /// <summary>
        /// Saves any pending changes to the conversation data file
        /// </summary>
        private void SaveDataFile()
        {
            dataFile.Save(Path.Combine(DataDir, "conversation.html"));
        }
 
        /// <summary>
        /// Adds a message to the conversation
        /// </summary>
        /// <param name="m">Message to add to the conversation</param>
        public void AddMessage(Message m)
        {
            // Add message to conversation
            Messages.Add(m);
            string messageHtml = m.ToHtmlString();

            // Update conversation data file
            HtmlNode newNode = HtmlNode.CreateNode(messageHtml);
            dataFile.GetElementbyId("messages").AppendChild(newNode);
            SaveDataFile();
        }
    }
}
