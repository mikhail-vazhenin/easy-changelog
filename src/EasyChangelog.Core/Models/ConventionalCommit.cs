using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChangelog.Core.Models
{
    public class ConventionalCommit
    {
        public string Scope { get; set; }

        public string Type { get; set; }

        public string Subject { get; set; }

        public string Url { get; set; }

        public string Sha { get; set; }

        public string ShortSha { get => Sha.Substring(0, 7); }
        public List<ConventionalCommitNote> Notes { get; set; } = new List<ConventionalCommitNote>();

    }

    public class ConventionalCommitNote
    {
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
