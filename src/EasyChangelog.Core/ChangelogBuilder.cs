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
        private string workingDir;
        private string changeLogFilename;
        private FileInfo _changelogFile;
        StringBuilder _markdownBuilder = new StringBuilder();

        public ChangelogBuilder(string workingDir, string changelogFilename)
        {
            this.workingDir = workingDir;
            this.changeLogFilename = changelogFilename;

            _changelogFile = new FileInfo(Path.Combine(workingDir, "CHANGELOG.md"));
        }

        public ChangelogBuilder AddVersionHeader(Version version, DateTime? versionDate)
        {
            var date = versionDate ?? DateTime.Today;

            _markdownBuilder.AppendLine($"<a name=\"{version}\"></a>");
            _markdownBuilder.AppendLine($"## {version} ({date.Year}-{date.Month}-{date.Day})");
            _markdownBuilder.AppendLine();
            return this;
        }

        public ChangelogBuilder AddBlock(string header, IEnumerable<ConventionalCommit> commits)
        {
            if (commits.Any())
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

        public void Save()
        {
            if (_changelogFile.Exists)
            {
                var contents = File.ReadAllText(_changelogFile.FullName);

                var firstReleaseHeadlineIdx = contents.IndexOf("##");

                if (firstReleaseHeadlineIdx >= 0)
                {
                    contents = contents.Substring(firstReleaseHeadlineIdx);
                }

                _markdownBuilder.AppendLine(contents);
            }

            var newContent = this.ToString();

            File.WriteAllText(Path.Combine(workingDir, "CHANGELOG.md"), newContent);
        }

        public override string ToString()
        {
            return _markdownBuilder.ToString();
        }
    }
}
