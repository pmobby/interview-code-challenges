﻿namespace OneBeyondApi.Model
{
    public class Borrower
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Fine Fine { get; set; }
    }
}
