using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChangelog.Models
{
    public class ConventionalCommit
    {
        public string Scope { get; set; }

        public string Type { get; set; }

        public string Subject { get; set; }

        public List<ConventionalCommitNote> Notes { get; set; } = new List<ConventionalCommitNote>();

    }

    public class ConventionalCommitNote
    {
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
