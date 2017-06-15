using SplitChat.Core.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitChat.Core.Model
{
    /// <summary>
    /// Message model
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Message types
        /// </summary>
        public enum MessageTypes
        {
            Invisible,  // Messages that are processed by not displayed
            Text,       // Simple message from one user to another
            Command,    // Message containing a command for the system
            System      // General informational message from the system
        }

        /// <summary>
        /// Conversation GUID
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Message type of current message
        /// </summary>
        public MessageTypes Type { get; set; }

        /// <summary>
        /// Username of message sender
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// When message was received
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Message text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public Message()
        {
            // Initialize object
            Id = Guid.NewGuid();
            Type = MessageTypes.Invisible;      // All messages are invisible by default
            Sender = "<Anonymous>";             // All messages are from <Anonymous> by default
            Timestamp = DateTime.Now;
            Text = "";
        }

        /// <summary>
        /// Converts the message to an HTML string
        /// </summary>
        /// <returns></returns>
        public string ToHtmlString()
        {
            string html = "";

            switch (Type)
            {
                case MessageTypes.Invisible:
                    html = @"
                        <!-- ==================== BEGIN INVISIBLE MESSAGE ==================== -->
                        <p class=""invisible message"" id=""{0}"">
                            <span class=""timestamp"">[{1}] </span>
                            <span class=""sender"">{2}: </span>
                            <span class=""text"">{3}</span>
                        </p>
                        <!-- ===================== END INVISIBLE MESSAGE ===================== -->
                    ";

                    html = string.Format(html, Id, Timestamp.ToLocalTime().ToString(), Sender, Text);
                    break;

                case MessageTypes.Text:
                    html = @"
                        <p class=""sent text message"" id=""{0}"">
                            <span class=""timestamp"">[{1}] </span>
                            <span class=""sender"">{2}: </span>
                            <span class=""text"">{3}</span>
                        </p>
                    ";

                    html = string.Format(html, Id, Timestamp.ToLocalTime().ToString(), Sender, Text);
                    break;

                case MessageTypes.Command:
                    // Command messages aren't meant to be displayed, and thus don't have an HTML
                    // string equivalent. Thus, we just break here.
                    break;

                case MessageTypes.System:
                    html = @"
                        <p class=""system message"" id=""{0}"">
                            <span class=""timestamp"">[{1}] </span>
                            <span class=""sender"">{2}: </span>
                            <span class=""text"">{3}</span>
                        </p>
                    ";

                    html = string.Format(html, Id, Timestamp.ToLocalTime().ToString(), Sender, Text);
                    break;

                default:
                    throw new InvalidMessageTypeException(this.ToString());
            }

            return html.Trim();
        }
    }
}
