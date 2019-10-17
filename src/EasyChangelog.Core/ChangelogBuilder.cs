using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EasyChangelog.Core.Models;

namespace EasyChangelog.Core
{
    public class ChangelogBuilder
    {
        private string changeLogFilename;
        private FileInfo _changelogFile;
        StringBuilder _markdownBuilder = new StringBuilder();

        public ChangelogBuilder(string changelogFilename)
        {
            this.changeLogFilename = changelogFilename;

            _changelogFile = new FileInfo(changelogFilename);
        }

        public ChangelogBuilder AddVersionHeader(Version version, DateTime? versionDate)
        {
            var date = versionDate ?? DateTime.Today;

            _markdownBuilder.AppendLine($"<a name=\"{version}\"></a>");
            _markdownBuilder.AppendLine($"## {version} ({date.Year}-{date.Month}-{date.Day})");
            _markdownBuilder.AppendLine();
            return this;
        }

        public ChangelogBuilder AddBlock(string header, ICollection<ConventionalCommit> commits)
        {
            if (commits.Count > 0)
            {
                _markdownBuilder.Append("### ").AppendLine(header);
                _markdownBuilder.AppendLine();

                foreach (var commit in commits)
                {
                    _markdownBuilder.AppendLine($"* {commit.Subject} ([{commit.ShortSha}]({commit.Url})");
                }
            }

            return this;
        }

        public override string ToString()
        {
            return _markdownBuilder.ToString();
        }
    }
}
