﻿namespace InventorySystem.Shared.Models.Communications
{
    public class SmtpConfig
    {
        public string Host { get; set; } = null!;

        public int Port { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool EnableSsl { get; set; }

        public string FromAddress { get; set; } = null!;

        public string FromName { get; set; } = null!;
    }
}
