using HtmlAgilityPack;
using SubAnalyzer.Models;
using SubAnalyzer.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SubAnalyzer.Scraping
{
    class ScrapingCore
    {
        public static bool Test()
        {
            try
            {
                string url = "https://www.reddit.com/r/funny/";

                var i = 0;
                var count = 0;
                var after = "";

                var watch = new Stopwatch();
                watch.Start();
                for(var n = 0; n < 25; n++)
                {
                    after = Test2(url, count, after);
                    count += 25;
                    url = String.Format("https://www.reddit.com/r/funny?count={0}&after=t3_{1}", count, after);
                }
                watch.Stop();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public static string Test2(string url, int count, string after)
        {
            try
            {
                var webGet = new HtmlWeb();
                var document = webGet.Load(url);

                var postList = document.DocumentNode.SelectNodes("//div[@id='siteTable']/div[contains(@class, 'thing')]");

                var skipId = "";

                foreach (HtmlNode post in postList)
                {
                    var topMatter = post.SelectSingleNode(".//div[contains(@class, 'entry')]").SelectSingleNode(".//div[contains(@class, 'top-matter')]");

                    var unVoted = post.SelectSingleNode(".//div[contains(@class, 'unvoted')]");

                    var score = unVoted.SelectSingleNode(".//div[contains(@class, 'unvoted')]").InnerText;

                    var tagLine = topMatter.SelectSingleNode(".//p[contains(@class, 'tagline')]");

                    var titleContent = topMatter.SelectSingleNode(".//p[@class='title']").SelectSingleNode(".//a[contains(@class, 'title')]");

                    var postTitle = titleContent.InnerText;

                    var titleLink = titleContent.Attributes["href"].Value;

                    var userName = tagLine.SelectSingleNode(".//a[contains(@class, 'author')]").InnerText;

                    var postId = post.Id.Split('_')[2];

                    DateTime postTime = Convert.ToDateTime(tagLine.SelectSingleNode(".//time").Attributes["datetime"].Value);


                    var newPost = new PostMain()
                    {
                        Title = postTitle.Replace("\"", "'").Replace("'", "''"),
                        UserName = userName,
                        TitleLink = titleLink,
                        PostId = postId,
                        PostDate = postTime
                    };

                    DatabaseService.UpdateDatabase(newPost);

                    skipId = postId;
                }

                return skipId;
            }
            catch(Exception e)
            {
                return "Nope";
            }
           
        }
    }
}
