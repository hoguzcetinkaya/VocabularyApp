using WordApp.Data;
using WordApp.Mongo;

namespace WordApp.Entities
{
    public interface IVocable : IUniqueIdentityEntity
    {
        string                  Word                { get; set; }
        List<string>            WordHalleri         { get; set; }
        List<string>            Meanings            { get; set; }
        string                  Pronunciation       { get; set; }
        WordType                WordType            { get; set; }
        WordLevel               WordLevel           { get; set; }
        WordCountability        WordCountability    { get; set; }
        bool                    IsActive            { get; set; }

    }
    enum WordTense
    {
        Present,
        Past,
        Future,
        PresentContinuous,
        PastContinuous,
        FutureContinuous,
        PresentPerfect,
        PastPerfect,
        FuturePerfect,
        PresentPerfectContinuous,
        PastPerfectContinuous,
        FuturePerfectContinuous
    }
    
    enum WordMood
    {
        Indicative,
        Subjunctive,
        Imperative,
        Conditional,
        Infinitive,
        Participle
    }
    enum WordVoice
    {
        Active,
        Passive
    }
    enum WordPerson
    {
        First,
        Second,
        Third
    }
    enum WordNumber
    {
        Singular,
        Plural
    }
    public enum WordType
    {
        Noun,
        Verb,
        Adjective,
        Adverb,
        Pronoun,
        Preposition,
        Conjunction,
        Interjection
    }
    public enum WordCountability
    {
        Countable,
        UnCountable
    }
    public enum WordLevel
    {
        A1,
        A2,
        B1,
        B2,
        C1,
    }

    [MongoEntity("Vocables")]
    public class Vocable : IVocable
    {
        public string                   Id                  { get; set; } = string.Empty;
        public DateTime                 CreateTime          { get; set; }
        public DateTime?                UpdateTime          { get; set; }
        public string                   Word                { get; set; } = string.Empty;
        public List<string>             Meanings            { get; set; } = [];
        public List<string>             WordHalleri         { get; set; } = [];
        public string                   Pronunciation       { get; set; } = string.Empty;
        public WordType                 WordType            { get; set; }
        public WordLevel                WordLevel           { get; set; }
        public WordCountability         WordCountability    { get; set; }
        public bool                     IsActive            { get; set; }
    }

    public static class VocableExtensions
    {
        public static TEntity SetCreateParams<TEntity>(this TEntity entity, IUniqueIdentityEntity? parent = null) where TEntity:IUniqueIdentityEntity
        {
            entity.Id = string.Concat(parent is null ? string.Empty : $"{parent.Id}:", Guid.NewGuid().ToString());
            entity.CreateTime = DateTime.Now;
            return entity;
        }
    }
}
