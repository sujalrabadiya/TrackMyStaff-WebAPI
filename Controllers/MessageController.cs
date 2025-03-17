using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMyStaffWebApplication.Data;
using TrackMyStaffWebApplication.Models;

namespace TrackMyStaffWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageRepository _messageRepository;

        #region Constructor
        public MessageController(MessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        #endregion

        #region Send Message
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] MessageModel message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSent = _messageRepository.SendMessage(message);
            if (isSent)
                return Ok(new { Message = "Message sent successfully." });

            return StatusCode(500, new { Message = "An error occurred while sending the message." });
        }
        #endregion

        #region Get Conversation
        [HttpGet("conversation/{userId1}-{userId2}")]
        public IActionResult GetConversation(int userId1, int userId2)
        {
            var messages = _messageRepository.GetConversation(userId1, userId2);
            Console.WriteLine(messages);
            return Ok(messages);
        }
        #endregion

        #region Clear Chat
        [HttpDelete("clear-chat/{userId1}-{userId2}")]
        public IActionResult ClearChat(int userId1, int userId2)
        {
            var isCleared = _messageRepository.ClearChat(userId1, userId2);
            if (isCleared)
                return Ok(new { Message = "Chat cleared successfully." });

            return StatusCode(500, new { Message = "An error occurred while clearing the chat." });
        }
        #endregion

        #region Get Conversation List
        [HttpGet("conversation-list/{userId}")]
        public IActionResult GetConversationList(int userId)
        {
            try
            {
                var conversations = _messageRepository.GetConversationList(userId);
                return Ok(conversations);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
