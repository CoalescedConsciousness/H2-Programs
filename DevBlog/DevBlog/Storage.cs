using DevBlog;
using System;
using System.IO;
using System.Xml.Serialization
using Common;

namespace Storage;

/// <summary>
/// Public record used to maintain database of Posts, utilizes interface to standardize
/// </summary>
public record PostStorage : IStorage
{
    internal const string PostDatabase = @"posts.txt";
    public static List<Post> PostDB = new List<Post>();

    public void Save()
    {
        foreach (Post post in PostDB)
        {
            if (!PostDB.Any(x => x.ID == post.ID))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Post));
                StreamWriter sw = new StreamWriter(PostDatabase);
                serializer.Serialize(sw, post);
                sw.Close();
            }
                
        }
    }

    public void Load()
    {
        if (File.Exists(PostDatabase))
        {
            foreach (Post post in PostDB)
            {
                XmlSerializer read = new XmlSerializer(typeof(Post));
                StreamReader file = new StreamReader(PostDatabase);
                Post result = (Post)read.Deserialize(file);
                file.Close();
            }
        }
    }

    public void Update(Post post, string field, string newData)
    {
        Post target = PostDB.Find(x => x.ID == post.ID);
            
        object p = field.ToLower() switch
        {
            "body" => target.Body = newData,
            "title" => target.Title = newData,
            "active" => (newData.ToLower() == "true") ? target.Active = true : target.Active = false,
            _ => throw new FormatException(),
        };

        target.EditDate = DateTime.Now;

    }
}

/// <summary>
/// Used to maintain database of available Authors.
/// </summary>
public record AuthorStorage : IStorage
{
    internal static string AuthorDatabase = @"authors.txt";
    public static List<Author> AuthorDB = new List<Author>();

    public void Save()
    {
        foreach (Author author in AuthorDB)
        {
            if (!AuthorDB.Any(x => x.ID == author.ID))
            {
                AuthorDB.Add(author);
            }
            else
            {
                throw new ExistsException(author.Name);
            }
        }
    }

    public void Load()
    {
        throw new System.NotImplementedException();
    }

    public void Update(Author author, string field, string newData)
    {
        Author target = AuthorDB.Find(x => x.ID == author.ID);
        object p = field.ToLower() switch
        {
            "name" => target.Name = newData,
            "email" => target.Email = newData,
            "active" => (field.ToLower() == "true") ? target.Active = true : target.Active = false,
            _ => throw new FormatException(),
        };
    }
}
