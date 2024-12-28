﻿using WordApp.Data;
using WordApp.Entities;

namespace WordApp.Dtos
{
    public class UpdateVocableDto : IUpdateOrDeleteDto
    {
        public string                   Id                  { get; set; } = string.Empty;
        public string                   Word                { get; set; } = string.Empty;
        public List<string>             Meanings            { get; set; } = [];
        public string                   Pronunciation       { get; set; } = string.Empty;
        public WordType                 WordType            { get; set; }
        public WordLevel                WordLevel           { get; set; }
        public WordCountability         WordCountability    { get; set; }
        public List<string>             WordHalleri             { get; set; } = [];

    }
}