using System;
using System.Collections.Generic;
using Dragonfly.DataAccess.Core;

namespace Dragonfly.Core.Security
{
    public class User : Auditable, ITrackeable
    {
        #region Public Properties
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsOnline { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLocked { get; set; }
        public string LockReason { get; set; }
        public EnabledState Enabled { get; set; }
        public DateTime? EnabledStateDate { get; set; }
        public bool MustChangePassword { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public DateTime? LastLockoutDate { get; set; }
        public int ConnectionTries { get; set; }
        public DateTime? LastConnectionTryDate { get; set; }
        public int AuthenticationMethodId { get; set; }
        public bool RequireOtp { get; set; }
        public OtpSendMode OtpSendMode { get; set; }
        public string OtpKey { get; set; }
        public int OtpValidTime { get; set; }
        public AuthenticationMethod AuthenticationMethod { get; set; }
        public List<Role> Roles { get; set; }
        public List<Session> Sessions { get; set; }
        public List<KeyValuePair<string, DateTime>> PasswordHistory { get; set; }
        public byte[] Photo { get; set; }
        #endregion Public Properties
    }
}
