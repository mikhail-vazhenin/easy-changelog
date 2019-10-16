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
        private readonly IVersionControl _versionControl;

        public ChangelogService(IVersionControl versionControl)
        {
            _versionControl = versionControl;
        }

        public void SaveChangelog(string workingDir)
        {
            GetChangelog(workingDir).Save();
        }

        public string GetChangelogText(string workingDir)
        {
            return GetChangelog(workingDir).ToString();
        }

        private ChangelogBuilder GetChangelog(string workingDir)
        {
            var lastVersion = _versionControl.GetLastReleaseVersion();
            var commits = _versionControl.GetCommits(lastVersion);

            return new ChangelogBuilder(workingDir, "CHANGELOG.md")
                  .AddVersionHeader(lastVersion, DateTime.Today)
                  .AddBlock("Bug Fixes", commits.Where(commit => "fix".Equals(commit.Type)))
                  .AddBlock("Features", commits.Where(commit => "feat".Equals(commit.Type)))
                  .AddBlock("Breaking Changes", commits.Where(commit => commit.Notes.Any(note => "BREAKING CHANGE".Equals(note.Title))));
        }
    }
}
