using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubAnalyzer.Models
{
    public class PostMain
    {
        public PostMain(){}

        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string TitleLink { get; set; }
        public string PostId { get; set; }
        public DateTime PostDate { get; set; }
        public int? Score { get; set; }
    }
}
