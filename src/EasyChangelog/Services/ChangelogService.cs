using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EasyChangelog.Core;
using EasyChangelog.Core.Interfaces;
using EasyChangelog.Core.Models;
using EasyChangelog.Services.Interfaces;
using EasyChangelog.Tools;

namespace EasyChangelog.Services
{
    public class ChangelogService : IChangelogService
    {
        const string changelogName = "CHANGELOG.md";

        private readonly IVersionControl _versionControl;

        public ChangelogService(IVersionControl versionControl)
        {
            _versionControl = versionControl;
        }

        public string GetChangelog(string workingDir, Version fromVersion, Version nextVersion, bool saveToFile = false)
        {
            var changelogFullName = GetChangelogFullname(workingDir);
            var changelogText = BuildChangelog(changelogFullName, fromVersion, nextVersion).ToString();

            if (saveToFile)
            {
                SaveChangelogToFile(changelogText, changelogFullName);
            }

            return changelogText;
        }

        public void CommitChanges(string workingDir, Version nextVersion, string token)
        {
            var changelogFullName = GetChangelogFullname(workingDir);

            var sha = _versionControl.PushChangelog(changelogFullName, nextVersion, token);
        }

        private ChangelogBuilder BuildChangelog(string chnagelogFileName, Version fromVersion, Version nextVersion)
        {
            var commits = _versionControl.GetCommits(fromVersion);

            return new ChangelogBuilder(chnagelogFileName)
                  .AddVersionHeader(nextVersion, DateTime.Today)
                  .AddBlock("Bug Fixes", commits.Where(commit => "fix".Equals(commit.Type)).ToArray())
                  .AddBlock("Features", commits.Where(commit => "feat".Equals(commit.Type)).ToArray())
                  .AddBlock("Breaking Changes", commits.Where(commit => commit.Notes.Any(note => "BREAKING CHANGE".Equals(note.Title))).ToArray());
        }

        private void SaveChangelogToFile(string changelogText, string changelogFullName)
        {
            if (File.Exists(changelogFullName))
            {
                var contents = File.ReadAllText(changelogFullName);

                var firstReleaseHeadlineIdx = contents.IndexOf("##");

                if (firstReleaseHeadlineIdx >= 0)
                {
                    contents = contents.Substring(firstReleaseHeadlineIdx);
                }

                changelogText += contents;
            }

            File.WriteAllText(changelogFullName, changelogText);
        }

        private string GetChangelogFullname(string workingDir)
        {
            return Path.Combine(workingDir, changelogName);
        }
    }
}
