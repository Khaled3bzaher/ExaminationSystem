﻿using System.Text.Json;

namespace Shared.ErrorModels
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
