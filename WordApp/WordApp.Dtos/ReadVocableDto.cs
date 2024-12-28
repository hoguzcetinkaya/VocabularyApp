﻿using WordApp.Entities;

namespace WordApp.Dtos
{
    public interface IDto
    {

    }
    public class ReadVocableDto : IVocable, IDto
    {
        public string                   Id                      { get; set; } = string.Empty;
        public DateTime                 CreateTime              { get; set; }
        public DateTime?                UpdateTime              { get; set; }
        public string                   Word                    { get; set; } = string.Empty;
        public List<string>             Meanings                { get; set; } = [];
        public string                   Pronunciation           { get; set; } = string.Empty;
        public WordType                 WordType                { get; set; }
        public WordLevel                WordLevel               { get; set; }
        public WordCountability         WordCountability        { get; set; }
        public List<string>             WordHalleri             { get; set; } = [];
        public bool                     IsActive                { get; set; }
    }
}
