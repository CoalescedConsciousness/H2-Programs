using DevBlog;
using Common;

namespace DevBlog.Repository
{
    public static class PostURL
    {
        /// <summary>
        /// Helper-method used to update record links.
        /// </summary>
        /// <param name="target"></param>
        private static void UpdateRecordLinks(Post target)
        {
            string query =
                $"UPDATE Post " +
                $"SET [Links] = '{target.Links}' " +
                $"WHERE ID = {target.ID} ";
            Database.QueryDatabase(query);
        }

        /// <summary>
        /// Adds a specified URL to the post method.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="url"></param>
        public static void AddURL(Post target, string url)
        {
            bool addUrl = true;
            while (addUrl)
            {
                if (!string.IsNullOrEmpty(url))
                {
                    if (target.Links == "")
                    {
                        target.Links += url;
                    }
                    else if (target.Links != "")
                    {
                        target.Links += $";{url}";
                    }
                    Console.WriteLine("Do you wish to add another URL? [y/n]");
                    string input = Console.ReadLine();

                    if (input.ToLower() != "y")
                    { addUrl = false; }
                }
            }

            UpdateRecordLinks(target);
        }

        /// <summary>
        /// Removes URL form specified Post
        /// </summary>
        /// <param name="target"></param>
        /// <param name="uInput"></param>
        public static void RemoveURL(Post target, int uInput)
        {
            if (!target.Links.Contains(";") && uInput == 1)
            {
                target.Links = "";
            }
            else
            {
                if (uInput != target.Links.Split(";").Length)
                {
                    target.Links.Replace($"{target.Links.Split(";")[uInput]};", "");
                }
                else
                {
                    target.Links.Replace($";{target.Links.Split(";")[uInput]}", "");
                }
            }

            UpdateRecordLinks(target);
        }

        /// <summary>
        /// Method used to open URL
        /// </summary>
        /// <param name="id"></param>
        public static void OpenURL(int id)
        {
            Post p = Post.GetPost(id);

            if (p.Links.Split(";").Length > 1)
            {
                Console.WriteLine("Please select the URL you wish to open:\n");
                for (int i = 0; i < p.Links.Split(";").Length; i++)
                {
                    Console.WriteLine($"{i+1}: {p.Links.Split(";")[i]}");
                }
                int input = int.Parse(Console.ReadLine());

                p.Links.Split(";")[input-1].OpenGivenUrl();
            }
            else if (p.Links.Split(";").Length == 1) { p.Links.OpenGivenUrl(); }
        }
    }
}
