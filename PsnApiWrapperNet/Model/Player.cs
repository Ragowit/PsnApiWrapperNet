using System.Collections.Generic;

namespace PsnApiWrapperNet.Model
{
    public class Player
    {
        public string aboutMe { get; set; }
        public List<Avatar> avatars { get; set; }
        public Error error { get; set; }
        public bool isMe { get; set; }
        public bool isOfficiallyVerified { get; set; }
        public bool isPlus { get; set; }
        public List<string> languages { get; set; }
        public string onlineId { get; set; }
    }
}
