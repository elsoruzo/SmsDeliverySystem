﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SmssApi
{
    public partial class SmsStatus
    {
        public int Id { get; set; }
        public Guid SmsId { get; set; }
        public string To { get; set; }
        public bool IsCompleted { get; set; }
		public virtual SmsMessage SmsMessage { get; set; }

	}
}